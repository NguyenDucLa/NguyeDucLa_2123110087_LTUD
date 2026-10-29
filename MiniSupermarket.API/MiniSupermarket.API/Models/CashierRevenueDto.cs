namespace MiniSupermarket.API.Models
{
    // DTO hứng kết quả thống kê doanh thu từ Stored Procedure sp_GetRevenueByCashier
    public class CashierRevenueDto
    {
        public string CashierUsername { get; set; } = string.Empty;
        public int TotalOrders { get; set; }
        public decimal TotalRevenue { get; set; }
    }
}
