using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MiniSupermarket.API.Data;
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

        // 1. READ: Danh sách hóa đơn
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var orders = await _context.Orders
                .Include(o => o.OrderDetails)
                .AsNoTracking()
                .OrderByDescending(o => o.OrderId)
                .ToListAsync();
            return Ok(orders);
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
            return Ok(order);
        }

        // 3. CREATE: Thanh toán tại quầy POS (tạo hóa đơn + chi tiết + trừ tồn kho + tích điểm)
        [HttpPost("checkout")]
        public async Task<IActionResult> Checkout([FromBody] CheckoutRequest request)
        {
            if (request.Items == null || request.Items.Count == 0)
                return BadRequest(new { message = "Giỏ hàng đang trống!" });

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

                decimal lineTotal = item.UnitPrice * item.Quantity;
                order.OrderDetails.Add(new OrderDetail
                {
                    ProductId = product.ProductId,
                    Quantity = item.Quantity,
                    UnitPrice = item.UnitPrice,
                    LineTotal = lineTotal
                });

                // Trừ tồn kho
                product.StockQuantity = Math.Max(0, product.StockQuantity - item.Quantity);
                total += lineTotal;
            }

            order.TotalAmount = total;

            // Tích điểm khách hàng thân thiết nếu có số điện thoại
            Customer? customer = null;
            if (!string.IsNullOrWhiteSpace(request.CustomerPhone))
            {
                customer = await _context.Customers.FirstOrDefaultAsync(c => c.PhoneNumber == request.CustomerPhone);
                if (customer != null)
                {
                    customer.RewardPoints += (int)(total / 1000); // 1 điểm cho mỗi 1.000đ
                    order.CustomerId = customer.CustomerId;
                }
            }

            _context.Orders.Add(order);
            await _context.SaveChangesAsync();

            return Ok(new
            {
                success = true,
                orderId = order.OrderId,
                orderCode = order.OrderCode,
                totalAmount = order.TotalAmount,
                customerName = customer?.CustomerName,
                rewardPoints = customer?.RewardPoints
            });
        }
    }

    public class CheckoutRequest
    {
        public string? CashierUsername { get; set; }
        public string? CustomerPhone { get; set; }
        public List<CheckoutItem> Items { get; set; } = new();
    }

    public class CheckoutItem
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
    }
}
