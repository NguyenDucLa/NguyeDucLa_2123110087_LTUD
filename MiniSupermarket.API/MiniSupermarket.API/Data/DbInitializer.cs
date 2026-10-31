using Microsoft.EntityFrameworkCore;
using MiniSupermarket.API.Models;

namespace MiniSupermarket.API.Data
{
    /// <summary>
    /// Nạp hóa đơn demo trải đều 30 ngày gần nhất để màn hình "Báo cáo doanh thu"
    /// và thống kê nhanh có dữ liệu thật.
    /// (Dữ liệu mồi HasData chỉ nạp được ngày cố định, còn doanh thu cần ngày động theo hôm nay.)
    /// </summary>
    public static class DbInitializer
    {
        private const string DemoPrefix = "HD-DEMO-";

        public static async Task SeedDemoOrdersAsync(SupermarketDbContext context)
        {
            try
            {
                // Những ngày ĐÃ có hóa đơn demo -> bỏ qua, chỉ bù các ngày còn thiếu
                var existingDays = (await context.Orders
                        .Where(o => o.OrderCode.StartsWith(DemoPrefix))
                        .Select(o => o.OrderDate)
                        .ToListAsync())
                    .Select(d => d.Date)
                    .ToHashSet();

                var products = await context.Products.AsNoTracking().ToListAsync();
                var customers = await context.Customers.AsNoTracking().Select(c => c.CustomerId).ToListAsync();
                var cashiers = await context.Users.AsNoTracking()
                    .Where(u => u.Role == "Cashier")
                    .OrderBy(u => u.Username)
                    .Select(u => u.Username)
                    .ToListAsync();

                if (products.Count == 0 || cashiers.Count == 0) return;

                var rnd = new Random(20261029); // Cố định seed -> dữ liệu ổn định giữa các lần chạy
                var orders = new List<Order>();

                for (int dayOffset = 29; dayOffset >= 0; dayOffset--)
                {
                    DateTime day = DateTime.Today.AddDays(-dayOffset).Date;

                    // Ngày này đã có hóa đơn demo rồi thì giữ nguyên
                    if (existingDays.Contains(day)) continue;

                    int ordersPerDay = rnd.Next(2, 5); // 2 - 4 hóa đơn mỗi ngày

                    for (int k = 0; k < ordersPerDay; k++)
                    {
                        DateTime orderDate = day.AddHours(8 + rnd.Next(0, 11)).AddMinutes(rnd.Next(0, 60));

                        var details = new List<OrderDetail>();
                        var usedProducts = new HashSet<int>();
                        decimal total = 0m;

                        int lines = rnd.Next(1, 4); // 1 - 3 mặt hàng mỗi hóa đơn
                        for (int d = 0; d < lines; d++)
                        {
                            var p = products[rnd.Next(products.Count)];
                            if (!usedProducts.Add(p.ProductId)) continue;

                            int qty = rnd.Next(1, 4);
                            decimal lineTotal = p.Price * qty;

                            details.Add(new OrderDetail
                            {
                                ProductId = p.ProductId,
                                Quantity = qty,
                                UnitPrice = p.Price,
                                LineTotal = lineTotal
                            });
                            total += lineTotal;
                        }

                        if (details.Count == 0) continue;

                        orders.Add(new Order
                        {
                            OrderCode = $"{DemoPrefix}{orderDate:yyyyMMdd}-{orders.Count + 1:D3}",
                            OrderDate = orderDate,
                            CashierUsername = cashiers[rnd.Next(cashiers.Count)],
                            CustomerId = rnd.Next(0, 3) == 0 ? null : customers[rnd.Next(customers.Count)],
                            TotalAmount = total,
                            Status = "Paid",
                            OrderDetails = details
                        });
                    }
                }

                context.Orders.AddRange(orders);
                await context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine("[DbInitializer] Bỏ qua nạp hóa đơn demo: " + ex.Message);
            }
        }

        /// <summary>
        /// Bổ sung ngày đăng ký cho các khách hàng mẫu (HasData không đặt được ngày động).
        /// Rải đều trong ~20 ngày gần nhất để thống kê "khách hàng mới" có số liệu.
        /// </summary>
        public static async Task BackfillCustomerCreatedDatesAsync(SupermarketDbContext context)
        {
            try
            {
                var customers = await context.Customers
                    .Where(c => c.CreatedDate < new DateTime(2000, 1, 1))
                    .OrderBy(c => c.CustomerId)
                    .ToListAsync();

                if (customers.Count == 0) return;

                var rnd = new Random(20261031);
                for (int i = 0; i < customers.Count; i++)
                {
                    int daysAgo = i / 2;                     // 2 khách / ngày
                    customers[i].CreatedDate = DateTime.Today
                        .AddDays(-Math.Min(daysAgo, 20))
                        .AddHours(8 + rnd.Next(0, 10))
                        .AddMinutes(rnd.Next(0, 60));
                }

                await context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine("[DbInitializer] Bỏ qua cập nhật ngày đăng ký khách hàng: " + ex.Message);
            }
        }
    }
}
