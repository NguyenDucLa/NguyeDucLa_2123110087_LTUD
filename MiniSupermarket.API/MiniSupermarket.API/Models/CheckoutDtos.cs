namespace MiniSupermarket.API.Models
{
    // Một dòng sản phẩm trong giỏ hàng gửi từ quầy POS
    public class CartItemCheckoutDto
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
    }

    // Yêu cầu thanh toán toàn bộ giỏ hàng
    public class CheckoutRequestDto
    {
        public string CashierUsername { get; set; } = string.Empty;

        // Có thể truyền CustomerId hoặc CustomerPhone (ưu tiên CustomerId nếu > 0)
        public int CustomerId { get; set; }
        public string? CustomerPhone { get; set; }

        public List<CartItemCheckoutDto> Items { get; set; } = new();
    }
}
