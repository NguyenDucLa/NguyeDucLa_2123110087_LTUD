using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MiniSupermarket.API.Data;
using MiniSupermarket.API.Models;

namespace MiniSupermarket.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")] // Chỉ Admin mới xem được báo cáo doanh thu
    public class ReportsController : ControllerBase
    {
        private readonly SupermarketDbContext _context;

        public ReportsController(SupermarketDbContext context)
        {
            _context = context;
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
    }
}
