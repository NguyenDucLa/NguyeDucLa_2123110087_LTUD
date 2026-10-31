using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MiniSupermarket.API.Data;
using MiniSupermarket.API.DTOs;
using MiniSupermarket.API.Models;

namespace MiniSupermarket.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class OrdersController : ControllerBase
    {
        private readonly SupermarketDbContext _context;

        public OrdersController(SupermarketDbContext context)
        {
            _context = context;
        }

        // Ánh xạ Entity -> DTO
        private static OrderDto ToDto(Order o) => new()
        {
            OrderId = o.OrderId,
            OrderCode = o.OrderCode,
            OrderDate = o.OrderDate,
            CustomerId = o.CustomerId,
            CashierUsername = o.CashierUsername,
            TotalAmount = o.TotalAmount,
            Status = o.Status,
            OrderDetails = (o.OrderDetails ?? new List<OrderDetail>()).Select(d => new OrderDetailDto
            {
                ProductId = d.ProductId,
                ProductName = d.Product?.ProductName ?? string.Empty,
                Quantity = d.Quantity,
                UnitPrice = d.UnitPrice,
                LineTotal = d.LineTotal
            }).ToList()
        };

        // 1. READ: Danh sách hóa đơn
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var orders = await _context.Orders
                .Include(o => o.OrderDetails).ThenInclude(d => d.Product)
                .AsNoTracking()
                .OrderByDescending(o => o.OrderId)
                .ToListAsync();
            return Ok(orders.Select(ToDto).ToList());
        }

        // 2. READ: Chi tiết hóa đơn
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var order = await _context.Orders
                .Include(o => o.OrderDetails).ThenInclude(d => d.Product)
                .AsNoTracking()
                .FirstOrDefaultAsync(o => o.OrderId == id);

            if (order == null) return NotFound(new { message = "Không tìm thấy hóa đơn!" });
            return Ok(ToDto(order));
        }

        // 3. CREATE: Thanh toán tại quầy POS
        //    Tạo hóa đơn + chi tiết + trừ tồn kho + ĐỔI ĐIỂM THƯỞNG TRỪ TIỀN + tích điểm mới.
        //    Toàn bộ nằm trong MỘT transaction: lỗi ở bước nào thì Rollback bước đó.
        [HttpPost("checkout")]
        public async Task<IActionResult> Checkout([FromBody] CheckoutRequest request)
        {
            if (request.Items == null || request.Items.Count == 0)
                return BadRequest(new { message = "Giỏ hàng đang trống, không thể thanh toán!" });

            // ---- Xác định khách hàng (nếu có) để áp dụng điểm thưởng ----
            Customer? customer = null;
            if (request.CustomerId.HasValue && request.CustomerId.Value > 0)
            {
                customer = await _context.Customers.FindAsync(request.CustomerId.Value);
            }
            else if (!string.IsNullOrWhiteSpace(request.CustomerPhone))
            {
                string phone = request.CustomerPhone.Trim();
                customer = await _context.Customers.FirstOrDefaultAsync(c => c.PhoneNumber == phone);
            }

            int redeemPoints = Math.Max(0, request.RedeemPoints);
            if (redeemPoints > 0)
            {
                if (customer == null)
                    return BadRequest(new { message = "Chưa xác định được khách hàng nên không thể dùng điểm thưởng!" });

                if (customer.RewardPoints < redeemPoints)
                    return BadRequest(new { message = $"Khách hàng chỉ có {customer.RewardPoints} điểm, không đủ {redeemPoints} điểm để trừ!" });
            }

            // ================= BẮT ĐẦU TRANSACTION =================
            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                var order = new Order
                {
                    OrderCode = $"HD{DateTime.Now:yyyyMMddHHmmss}",
                    OrderDate = DateTime.Now,
                    CashierUsername = request.CashierUsername ?? string.Empty,
                    Status = "Paid",
                    OrderDetails = new List<OrderDetail>()
                };

                decimal total = 0;
                foreach (var item in request.Items)
                {
                    var product = await _context.Products.FindAsync(item.ProductId);
                    if (product == null) continue;

                    if (product.StockQuantity < item.Quantity)
                        return BadRequest(new { message = $"Sản phẩm \"{product.ProductName}\" chỉ còn {product.StockQuantity} trong kho!" });

                    decimal lineTotal = item.UnitPrice * item.Quantity;
                    order.OrderDetails.Add(new OrderDetail
                    {
                        ProductId = product.ProductId,
                        Quantity = item.Quantity,
                        UnitPrice = item.UnitPrice,
                        LineTotal = lineTotal
                    });

                    // Trừ tồn kho
                    product.StockQuantity -= item.Quantity;
                    total += lineTotal;
                }

                // ---- Đổi điểm thưởng trừ tiền: 1 điểm = 1.000 đ ----
                decimal discount = redeemPoints * 1000m;
                if (discount > total) discount = total;             // không cho âm tiền
                decimal finalAmount = total - discount;

                order.TotalAmount = finalAmount;
                if (customer != null) order.CustomerId = customer.CustomerId;

                _context.Orders.Add(order);

                // ---- Cập nhật điểm trong CÙNG transaction ----
                int earnedPoints = 0;
                if (customer != null)
                {
                    earnedPoints = (int)(finalAmount / 1000);       // 1 điểm / 1.000 đ
                    customer.RewardPoints = customer.RewardPoints - redeemPoints + earnedPoints;
                }

                await _context.SaveChangesAsync();
                await transaction.CommitAsync();                    // Lưu tất cả

                return Ok(new
                {
                    success = true,
                    orderId = order.OrderId,
                    orderCode = order.OrderCode,
                    totalAmount = total,
                    discount,
                    finalAmount = order.TotalAmount,
                    redeemPoints,
                    earnedPoints,
                    customerName = customer?.CustomerName,
                    rewardPoints = customer?.RewardPoints
                });
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();                  // Hoàn tác toàn bộ
                return BadRequest(new { message = "Thanh toán thất bại, đã hoàn tác: " + ex.Message });
            }
        }
    }

    public class CheckoutRequest
    {
        public string? CashierUsername { get; set; }
        public string? CustomerPhone { get; set; }
        public int? CustomerId { get; set; }
        public int RedeemPoints { get; set; }      // Số điểm khách muốn dùng để trừ tiền
        public List<CheckoutItem> Items { get; set; } = new();
    }

    public class CheckoutItem
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
    }
}
