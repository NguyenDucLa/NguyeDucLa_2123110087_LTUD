using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using MiniSupermarket.API.Data;
using MiniSupermarket.API.DTOs;
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
        // DASHBOARD BÁO CÁO: KPI trong ngày + top sản phẩm + doanh thu theo
        // thu ngân + doanh thu theo từng ngày (để vẽ biểu đồ)
        // GET: api/reports/dashboard?date=yyyy-MM-dd&days=14
        // =====================================================================
        [HttpGet("dashboard")]
        public async Task<IActionResult> Dashboard([FromQuery] string? date, [FromQuery] int days = 14)
        {
            if (days < 1) days = 14;
            if (days > 90) days = 90;

            DateTime day = DateTime.Today;
            if (!string.IsNullOrWhiteSpace(date) && DateTime.TryParse(date, out var parsed))
            {
                day = parsed.Date;
            }
            DateTime nextDay = day.AddDays(1);

            // Hóa đơn của riêng ngày đang chọn
            var dayOrders = await _context.Orders
                .AsNoTracking()
                .Where(o => o.OrderDate >= day && o.OrderDate < nextDay)
                .Include(o => o.OrderDetails).ThenInclude(d => d.Product)
                .ToListAsync();

            var details = dayOrders
                .SelectMany(o => o.OrderDetails ?? new List<OrderDetail>())
                .ToList();

            decimal totalRevenue = dayOrders.Sum(o => o.TotalAmount);
            int totalOrders = dayOrders.Count;

            var topProducts = details
                .GroupBy(d => d.ProductId)
                .Select(g => new TopProductDto
                {
                    ProductId = g.Key,
                    ProductName = g.Select(x => x.Product?.ProductName)
                                   .FirstOrDefault(n => n != null) ?? $"SP #{g.Key}",
                    Quantity = g.Sum(x => x.Quantity),
                    Revenue = g.Sum(x => x.LineTotal)
                })
                .OrderByDescending(x => x.Quantity)
                .ThenByDescending(x => x.Revenue)
                .Take(8)
                .ToList();

            var cashiers = dayOrders
                .GroupBy(o => o.CashierUsername)
                .Select(g => new CashierRevenueDto
                {
                    CashierUsername = g.Key,
                    TotalOrders = g.Count(),
                    TotalRevenue = g.Sum(x => x.TotalAmount)
                })
                .OrderByDescending(x => x.TotalRevenue)
                .ToList();

            // Doanh thu từng ngày trong khoảng `days` ngày tính đến ngày đang chọn
            DateTime from = day.AddDays(-(days - 1));
            var rangeOrders = await _context.Orders
                .AsNoTracking()
                .Where(o => o.OrderDate >= from && o.OrderDate < nextDay)
                .Select(o => new { o.OrderDate, o.TotalAmount })
                .ToListAsync();

            var daily = new List<DailyRevenueDto>();
            for (int i = 0; i < days; i++)
            {
                DateTime d = from.AddDays(i);
                var inDay = rangeOrders.Where(x => x.OrderDate.Date == d).ToList();
                daily.Add(new DailyRevenueDto
                {
                    Date = d.ToString("yyyy-MM-dd"),
                    Orders = inDay.Count,
                    Revenue = inDay.Sum(x => x.TotalAmount)
                });
            }

            var first = topProducts.FirstOrDefault();

            return Ok(new DashboardDto
            {
                Date = day.ToString("yyyy-MM-dd"),
                TotalOrders = totalOrders,
                TotalRevenue = totalRevenue,
                AveragePerOrder = totalOrders == 0 ? 0 : Math.Round(totalRevenue / totalOrders, 0),
                TotalItemsSold = details.Sum(d => d.Quantity),
                BestSellerName = first?.ProductName ?? "Chưa có dữ liệu",
                BestSellerQuantity = first?.Quantity ?? 0,
                TopProducts = topProducts,
                Cashiers = cashiers,
                Daily = daily
            });
        }

        // =====================================================================
        // BÁO CÁO DOANH THU THEO THU NGÂN — gọi Stored Procedure sp_GetRevenueByCashier
        // GET: api/reports/revenue-by-cashier
        // =====================================================================
        [HttpGet("revenue-by-cashier")]
        public async Task<IActionResult> GetRevenueByCashier()
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
                        await connection.OpenAsync();

                        using (SqlDataReader reader = await command.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
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

        // =====================================================================
        // DASHBOARD TỔNG QUAN — 4 chỉ số KPI, mỗi chỉ số lấy bằng ADO.NET
        // ExecuteScalar (nhanh, không cần load entity)
        // GET: api/reports/dashboard-summary
        // =====================================================================
        [HttpGet("dashboard-summary")]
        public async Task<IActionResult> DashboardSummary()
        {
            string connectionString = _configuration.GetConnectionString("DefaultConnection")!;

            decimal revenueToday = 0m;
            int ordersToday = 0, newCustomers = 0, lowStock = 0;

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    await connection.OpenAsync();

                    // 1. Doanh thu hôm nay
                    using (SqlCommand cmd = new SqlCommand(
                        "SELECT ISNULL(SUM(TotalAmount), 0) FROM Orders WHERE CAST(OrderDate AS DATE) = CAST(GETDATE() AS DATE)",
                        connection))
                    {
                        object? value = await cmd.ExecuteScalarAsync();
                        if (value != null && value != DBNull.Value) revenueToday = Convert.ToDecimal(value);
                    }

                    // 2. Tổng số hóa đơn đã xuất hôm nay
                    using (SqlCommand cmd = new SqlCommand(
                        "SELECT COUNT(*) FROM Orders WHERE CAST(OrderDate AS DATE) = CAST(GETDATE() AS DATE)",
                        connection))
                    {
                        object? value = await cmd.ExecuteScalarAsync();
                        if (value != null && value != DBNull.Value) ordersToday = Convert.ToInt32(value);
                    }

                    // 3. Số khách hàng mới đăng ký hôm nay
                    using (SqlCommand cmd = new SqlCommand(
                        "SELECT COUNT(*) FROM Customers WHERE CAST(CreatedDate AS DATE) = CAST(GETDATE() AS DATE)",
                        connection))
                    {
                        object? value = await cmd.ExecuteScalarAsync();
                        if (value != null && value != DBNull.Value) newCustomers = Convert.ToInt32(value);
                    }

                    // 4. Số mặt hàng sắp hết (tồn kho <= 10)
                    using (SqlCommand cmd = new SqlCommand(
                        "SELECT COUNT(*) FROM Products WHERE StockQuantity <= 10",
                        connection))
                    {
                        object? value = await cmd.ExecuteScalarAsync();
                        if (value != null && value != DBNull.Value) lowStock = Convert.ToInt32(value);
                    }
                }
            }
            catch (SqlException ex)
            {
                return StatusCode(500, new { success = false, message = "Lỗi truy vấn tổng quan: " + ex.Message });
            }

            return Ok(new
            {
                success = true,
                generatedAt = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                revenueToday,
                ordersToday,
                newCustomers,
                lowStock
            });
        }

        // =====================================================================
        // BIỂU ĐỒ DOANH THU THEO NGÀY — gọi Stored Procedure sp_GetRevenueByDay
        // (bên trong SP dùng GROUP BY CAST(OrderDate AS DATE))
        // GET: api/reports/revenue-by-day?from=yyyy-MM-dd&to=yyyy-MM-dd
        // =====================================================================
        [HttpGet("revenue-by-day")]
        public async Task<IActionResult> RevenueByDay([FromQuery] string? from, [FromQuery] string? to)
        {
            DateTime fromDate = DateTime.Today.AddDays(-13);
            DateTime toDate = DateTime.Today;

            if (!string.IsNullOrWhiteSpace(from) && DateTime.TryParse(from, out var f)) fromDate = f.Date;
            if (!string.IsNullOrWhiteSpace(to) && DateTime.TryParse(to, out var t)) toDate = t.Date;

            if (toDate < fromDate)
            {
                (fromDate, toDate) = (toDate, fromDate);
            }

            var list = new List<DailyRevenueDto>();
            string connectionString = _configuration.GetConnectionString("DefaultConnection")!;

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                using (SqlCommand command = new SqlCommand("sp_GetRevenueByDay", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@FromDate", fromDate);
                    command.Parameters.AddWithValue("@ToDate", toDate);

                    await connection.OpenAsync();

                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            list.Add(new DailyRevenueDto
                            {
                                Date = Convert.ToDateTime(reader["OrderDay"]).ToString("yyyy-MM-dd"),
                                Orders = Convert.ToInt32(reader["TotalOrders"]),
                                Revenue = Convert.ToDecimal(reader["TotalRevenue"])
                            });
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                return StatusCode(500, new
                {
                    success = false,
                    message = "Lỗi thực thi Stored Procedure sp_GetRevenueByDay: " + ex.Message
                });
            }

            return Ok(new
            {
                success = true,
                fromDate = fromDate.ToString("yyyy-MM-dd"),
                toDate = toDate.ToString("yyyy-MM-dd"),
                totalRevenue = list.Sum(x => x.Revenue),
                data = list
            });
        }

        // =====================================================================
        // TOP SẢN PHẨM BÁN CHẠY NHẤT — LINQ GroupBy → OrderByDescending → Take
        // GET: api/reports/top-products?date=yyyy-MM-dd&top=5
        // =====================================================================
        [HttpGet("top-products")]
        public async Task<IActionResult> TopProducts([FromQuery] string? date, [FromQuery] int top = 5)
        {
            if (top < 1) top = 5;
            if (top > 20) top = 20;

            DateTime day = DateTime.Today;
            if (!string.IsNullOrWhiteSpace(date) && DateTime.TryParse(date, out var parsed)) day = parsed.Date;
            DateTime nextDay = day.AddDays(1);

            // Gom nhóm theo sản phẩm, sắp xếp giảm dần theo số lượng bán ra
            var grouped = await _context.OrderDetails
                .AsNoTracking()
                .Where(d => d.Order!.OrderDate >= day && d.Order.OrderDate < nextDay)
                .GroupBy(d => d.ProductId)
                .Select(g => new
                {
                    ProductId = g.Key,
                    Quantity = g.Sum(x => x.Quantity),
                    Revenue = g.Sum(x => x.LineTotal)
                })
                .OrderByDescending(x => x.Quantity)
                .Take(top)
                .ToListAsync();

            var ids = grouped.Select(g => g.ProductId).ToList();
            var names = await _context.Products
                .AsNoTracking()
                .Where(p => ids.Contains(p.ProductId))
                .ToDictionaryAsync(p => p.ProductId, p => p.ProductName);

            var result = grouped.Select(g => new TopProductDto
            {
                ProductId = g.ProductId,
                ProductName = names.TryGetValue(g.ProductId, out var n) ? n : $"SP #{g.ProductId}",
                Quantity = g.Quantity,
                Revenue = g.Revenue
            }).ToList();

            return Ok(new
            {
                success = true,
                date = day.ToString("yyyy-MM-dd"),
                top,
                data = result
            });
        }
    }
}
