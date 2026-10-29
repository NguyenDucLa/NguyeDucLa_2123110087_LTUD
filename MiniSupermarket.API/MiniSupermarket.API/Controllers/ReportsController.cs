using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using MiniSupermarket.API.Data;
using MiniSupermarket.API.Models;
using System.Data;

namespace MiniSupermarket.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")] // Chỉ Admin mới xem được báo cáo doanh thu
    public class ReportsController : ControllerBase
    {
        private readonly SupermarketDbContext _context;
        private readonly IConfiguration _configuration;

        public ReportsController(SupermarketDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        // Báo cáo nhanh theo ngày: tổng hóa đơn, tổng doanh thu, mặt hàng bán chạy nhất
        [HttpGet("quick")]
        public async Task<IActionResult> Quick([FromQuery] string? date)
        {
            DateTime day = DateTime.Today;
            if (!string.IsNullOrWhiteSpace(date) && DateTime.TryParse(date, out var parsed))
            {
                day = parsed.Date;
            }
            var nextDay = day.AddDays(1);

            var orders = await _context.Orders
                .Where(o => o.OrderDate >= day && o.OrderDate < nextDay)
                .Include(o => o.OrderDetails)
                .AsNoTracking()
                .ToListAsync();

            int totalOrders = orders.Count;
            decimal totalRevenue = orders.Sum(o => o.TotalAmount);

            var bestSeller = orders
                .SelectMany(o => o.OrderDetails ?? new List<OrderDetail>())
                .GroupBy(d => d.ProductId)
                .Select(g => new { ProductId = g.Key, Quantity = g.Sum(x => x.Quantity) })
                .OrderByDescending(x => x.Quantity)
                .FirstOrDefault();

            string bestSellerName = "Chưa có dữ liệu";
            int bestSellerQuantity = 0;
            if (bestSeller != null)
            {
                var product = await _context.Products.AsNoTracking()
                    .FirstOrDefaultAsync(p => p.ProductId == bestSeller.ProductId);
                bestSellerName = product?.ProductName ?? $"SP #{bestSeller.ProductId}";
                bestSellerQuantity = bestSeller.Quantity;
            }

            return Ok(new
            {
                date = day.ToString("yyyy-MM-dd"),
                totalOrders,
                totalRevenue,
                bestSellerName,
                bestSellerQuantity
            });
        }

        // =====================================================================
        // BÁO CÁO DOANH THU THEO THU NGÂN — gọi Stored Procedure sp_GetRevenueByCashier
        // GET: api/reports/revenue-by-cashier
        // =====================================================================
        [HttpGet("revenue-by-cashier")]
        public IActionResult GetRevenueByCashier()
        {
            var reportList = new List<CashierRevenueDto>();
            string connectionString = _configuration.GetConnectionString("DefaultConnection")!;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand("sp_GetRevenueByCashier", connection))
                {
                    // BẮT BUỘC: chỉ định đây là Stored Procedure, không phải câu SQL thường
                    command.CommandType = CommandType.StoredProcedure;

                    try
                    {
                        connection.Open();

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                reportList.Add(new CashierRevenueDto
                                {
                                    CashierUsername = reader["CashierUsername"].ToString() ?? string.Empty,
                                    TotalOrders = Convert.ToInt32(reader["TotalOrders"]),
                                    TotalRevenue = Convert.ToDecimal(reader["TotalRevenue"])
                                });
                            }
                        }
                    }
                    catch (SqlException ex)
                    {
                        return StatusCode(500, new
                        {
                            success = false,
                            message = "Lỗi thực thi Stored Procedure trên SQL Server: " + ex.Message
                        });
                    }
                }
            }

            return Ok(new
            {
                success = true,
                generatedAt = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                totalCashiers = reportList.Count,
                grandTotalRevenue = reportList.Sum(x => x.TotalRevenue),
                data = reportList
            });
        }
    }
}
