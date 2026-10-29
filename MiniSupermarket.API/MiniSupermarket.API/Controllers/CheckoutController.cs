using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using MiniSupermarket.API.Models;

namespace MiniSupermarket.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CheckoutController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public CheckoutController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        // =====================================================================
        // POST: api/checkout
        // Thanh toán POS: tạo Orders + OrderDetails, trừ kho, tích điểm
        // Toàn bộ bọc trong SqlTransaction (chuẩn ACID)
        // =====================================================================
        [HttpPost]
        public IActionResult ProcessCheckout([FromBody] CheckoutRequestDto request)
        {
            if (request.Items == null || request.Items.Count == 0)
                return BadRequest(new { message = "Giỏ hàng rỗng, không thể tiến hành thanh toán!" });

            if (string.IsNullOrWhiteSpace(request.CashierUsername))
                return BadRequest(new { message = "Thiếu thông tin tài khoản thu ngân lập hóa đơn!" });

            string connectionString = _configuration.GetConnectionString("DefaultConnection")!;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Bắt đầu phiên giao dịch
                SqlTransaction transaction = connection.BeginTransaction();

                try
                {
                    decimal finalAmount = request.Items.Sum(i => i.Quantity * i.UnitPrice);
                    int totalItems = request.Items.Sum(i => i.Quantity);
                    int rewardPoints = (int)(finalAmount / 10000); // 1 điểm / 10.000đ

                    // Xác định khách hàng: theo CustomerId hoặc số điện thoại
                    int? customerId = request.CustomerId > 0 ? request.CustomerId : null;
                    if (customerId == null && !string.IsNullOrWhiteSpace(request.CustomerPhone))
                    {
                        using (SqlCommand cmdFind = new SqlCommand(
                            "SELECT CustomerId FROM Customers WHERE PhoneNumber = @Phone", connection, transaction))
                        {
                            cmdFind.Parameters.AddWithValue("@Phone", request.CustomerPhone.Trim());
                            object found = cmdFind.ExecuteScalar();
                            if (found != null && found != DBNull.Value)
                                customerId = Convert.ToInt32(found);
                        }
                    }

                    string orderCode = $"HD-{DateTime.Now:yyyyMMdd}-{Guid.NewGuid().ToString().Substring(0, 4).ToUpper()}";

                    // BƯỚC 1: Tạo hóa đơn, lấy OrderId vừa sinh bằng OUTPUT INSERTED.OrderId
                    const string insertOrderSql = @"
                        INSERT INTO Orders (OrderCode, OrderDate, CashierUsername, CustomerId, TotalAmount, Status)
                        OUTPUT INSERTED.OrderId
                        VALUES (@OrderCode, GETDATE(), @CashierUsername, @CustomerId, @TotalAmount, @Status)";

                    int orderId;
                    using (SqlCommand cmdOrder = new SqlCommand(insertOrderSql, connection, transaction))
                    {
                        cmdOrder.Parameters.AddWithValue("@OrderCode", orderCode);
                        cmdOrder.Parameters.AddWithValue("@CashierUsername", request.CashierUsername.Trim());
                        cmdOrder.Parameters.AddWithValue("@CustomerId", (object?)customerId ?? DBNull.Value);
                        cmdOrder.Parameters.AddWithValue("@TotalAmount", finalAmount);
                        cmdOrder.Parameters.AddWithValue("@Status", "Paid");
                        orderId = Convert.ToInt32(cmdOrder.ExecuteScalar());
                    }

                    // BƯỚC 2 & 3: Từng món — kiểm tra tồn kho, lưu chi tiết, trừ kho
                    foreach (var item in request.Items)
                    {
                        int currentStock;
                        string productName;

                        using (SqlCommand cmdCheck = new SqlCommand(
                            "SELECT StockQuantity, ProductName FROM Products WHERE ProductId = @ProductId", connection, transaction))
                        {
                            cmdCheck.Parameters.AddWithValue("@ProductId", item.ProductId);
                            using (SqlDataReader reader = cmdCheck.ExecuteReader())
                            {
                                if (!reader.Read())
                                    throw new InvalidOperationException($"Không tìm thấy sản phẩm có ID: {item.ProductId}");

                                currentStock = Convert.ToInt32(reader["StockQuantity"]);
                                productName = reader["ProductName"].ToString() ?? string.Empty;
                            }
                        }

                        // Điểm kiểm soát: mua vượt tồn kho -> ném lỗi để Rollback
                        if (currentStock < item.Quantity)
                        {
                            throw new InvalidOperationException(
                                $"Sản phẩm '{productName}' (ID: {item.ProductId}) không đủ tồn kho! " +
                                $"(Tồn kho thực tế: {currentStock}, Số lượng yêu cầu mua: {item.Quantity})");
                        }

                        const string insertDetailSql = @"
                            INSERT INTO OrderDetails (OrderId, ProductId, Quantity, UnitPrice, LineTotal)
                            VALUES (@OrderId, @ProductId, @Quantity, @UnitPrice, @LineTotal)";

                        using (SqlCommand cmdDetail = new SqlCommand(insertDetailSql, connection, transaction))
                        {
                            cmdDetail.Parameters.AddWithValue("@OrderId", orderId);
                            cmdDetail.Parameters.AddWithValue("@ProductId", item.ProductId);
                            cmdDetail.Parameters.AddWithValue("@Quantity", item.Quantity);
                            cmdDetail.Parameters.AddWithValue("@UnitPrice", item.UnitPrice);
                            cmdDetail.Parameters.AddWithValue("@LineTotal", item.Quantity * item.UnitPrice);
                            cmdDetail.ExecuteNonQuery();
                        }

                        using (SqlCommand cmdStock = new SqlCommand(
                            "UPDATE Products SET StockQuantity = StockQuantity - @Qty WHERE ProductId = @ProductId", connection, transaction))
                        {
                            cmdStock.Parameters.AddWithValue("@Qty", item.Quantity);
                            cmdStock.Parameters.AddWithValue("@ProductId", item.ProductId);
                            cmdStock.ExecuteNonQuery();
                        }
                    }

                    // BƯỚC 4: Cộng điểm tích lũy cho khách hàng
                    if (rewardPoints > 0 && customerId.HasValue)
                    {
                        using (SqlCommand cmdCustomer = new SqlCommand(
                            "UPDATE Customers SET RewardPoints = RewardPoints + @Points WHERE CustomerId = @CustomerId", connection, transaction))
                        {
                            cmdCustomer.Parameters.AddWithValue("@Points", rewardPoints);
                            cmdCustomer.Parameters.AddWithValue("@CustomerId", customerId.Value);
                            cmdCustomer.ExecuteNonQuery();
                        }
                    }

                    // BƯỚC 5: Commit — ghi vĩnh viễn
                    transaction.Commit();

                    return Ok(new
                    {
                        success = true,
                        message = "Thanh toán hóa đơn POS thành công!",
                        orderId,
                        orderCode,
                        totalItems,
                        finalAmount,
                        rewardPointsAdded = rewardPoints
                    });
                }
                catch (Exception ex)
                {
                    // BƯỚC 6: Rollback — hoàn tác toàn bộ
                    try
                    {
                        transaction.Rollback();
                    }
                    catch (Exception rollbackEx)
                    {
                        return StatusCode(500, new
                        {
                            success = false,
                            message = "Lỗi nghiêm trọng khi thực thi Rollback: " + rollbackEx.Message
                        });
                    }

                    return BadRequest(new
                    {
                        success = false,
                        message = "Giao dịch thanh toán bị hủy và đã được Rollback an toàn!",
                        errorDetail = ex.Message
                    });
                }
            }
        }

        // =====================================================================
        // DELETE: api/checkout/cancel/{orderId}
        // Hủy hóa đơn: hoàn trả tồn kho + khấu trừ điểm thưởng, bọc Transaction
        // =====================================================================
        [HttpDelete("cancel/{orderId}")]
        public IActionResult CancelOrder(int orderId)
        {
            string connectionString = _configuration.GetConnectionString("DefaultConnection")!;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlTransaction transaction = connection.BeginTransaction();

                try
                {
                    // 1. Lấy thông tin hóa đơn
                    int? customerId = null;
                    decimal totalAmount;

                    using (SqlCommand cmdGet = new SqlCommand(
                        "SELECT CustomerId, TotalAmount FROM Orders WHERE OrderId = @OrderId", connection, transaction))
                    {
                        cmdGet.Parameters.AddWithValue("@OrderId", orderId);
                        using (SqlDataReader reader = cmdGet.ExecuteReader())
                        {
                            if (!reader.Read())
                                throw new InvalidOperationException($"Không tìm thấy hóa đơn ID = {orderId} để hủy!");

                            customerId = reader["CustomerId"] == DBNull.Value ? null : Convert.ToInt32(reader["CustomerId"]);
                            totalAmount = Convert.ToDecimal(reader["TotalAmount"]);
                        }
                    }

                    int pointsToDeduct = (int)(totalAmount / 10000);

                    // 2. Lấy danh sách sản phẩm để hoàn kho
                    var itemsToRestore = new List<(int ProductId, int Quantity)>();
                    using (SqlCommand cmdDetails = new SqlCommand(
                        "SELECT ProductId, Quantity FROM OrderDetails WHERE OrderId = @OrderId", connection, transaction))
                    {
                        cmdDetails.Parameters.AddWithValue("@OrderId", orderId);
                        using (SqlDataReader reader = cmdDetails.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                itemsToRestore.Add((
                                    Convert.ToInt32(reader["ProductId"]),
                                    Convert.ToInt32(reader["Quantity"])
                                ));
                            }
                        }
                    }

                    // 3. Hoàn trả tồn kho
                    foreach (var item in itemsToRestore)
                    {
                        using (SqlCommand cmdRestore = new SqlCommand(
                            "UPDATE Products SET StockQuantity = StockQuantity + @Qty WHERE ProductId = @ProductId", connection, transaction))
                        {
                            cmdRestore.Parameters.AddWithValue("@Qty", item.Quantity);
                            cmdRestore.Parameters.AddWithValue("@ProductId", item.ProductId);
                            cmdRestore.ExecuteNonQuery();
                        }
                    }

                    // 4. Khấu trừ điểm thưởng
                    if (pointsToDeduct > 0 && customerId.HasValue)
                    {
                        using (SqlCommand cmdDeduct = new SqlCommand(@"
                            UPDATE Customers SET RewardPoints = CASE
                                WHEN RewardPoints >= @Pts THEN RewardPoints - @Pts
                                ELSE 0
                            END
                            WHERE CustomerId = @CustomerId", connection, transaction))
                        {
                            cmdDeduct.Parameters.AddWithValue("@Pts", pointsToDeduct);
                            cmdDeduct.Parameters.AddWithValue("@CustomerId", customerId.Value);
                            cmdDeduct.ExecuteNonQuery();
                        }
                    }

                    // 5. Xóa chi tiết trước (tránh khóa ngoại), rồi xóa hóa đơn
                    using (SqlCommand cmdDelDetails = new SqlCommand(
                        "DELETE FROM OrderDetails WHERE OrderId = @OrderId", connection, transaction))
                    {
                        cmdDelDetails.Parameters.AddWithValue("@OrderId", orderId);
                        cmdDelDetails.ExecuteNonQuery();
                    }

                    using (SqlCommand cmdDelOrder = new SqlCommand(
                        "DELETE FROM Orders WHERE OrderId = @OrderId", connection, transaction))
                    {
                        cmdDelOrder.Parameters.AddWithValue("@OrderId", orderId);
                        cmdDelOrder.ExecuteNonQuery();
                    }

                    transaction.Commit();

                    return Ok(new
                    {
                        success = true,
                        message = $"Đã hủy thành công hóa đơn #{orderId} và hoàn trả toàn bộ tồn kho vào CSDL!"
                    });
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    return BadRequest(new
                    {
                        success = false,
                        message = "Hủy hóa đơn thất bại, giao dịch đã được Rollback!",
                        errorDetail = ex.Message
                    });
                }
            }
        }
    }
}
