namespace MiniSupermarket.WinForms
{
    // DTO dùng chung cho các Form con (nhận dữ liệu JSON từ Web API)

    public class ProductDto
    {
        public int ProductId { get; set; }
        public string Barcode { get; set; } = string.Empty;
        public string ProductName { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int StockQuantity { get; set; }
        public int CategoryId { get; set; }

        // Tên nhóm hàng do API trả về (đã Eager Loading)
        public string CategoryName { get; set; } = string.Empty;
    }

    public class UserDto
    {
        public int UserId { get; set; }
        public string Username { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        public string? Email { get; set; }
        public string Role { get; set; } = string.Empty;
        public bool IsActive { get; set; }
    }

    public class CartItemDto
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }
        public decimal TotalPrice => UnitPrice * Quantity;
    }

    public class QuickReportDto
    {
        public string Date { get; set; } = string.Empty;
        public int TotalOrders { get; set; }
        public decimal TotalRevenue { get; set; }
        public string BestSellerName { get; set; } = string.Empty;
        public int BestSellerQuantity { get; set; }
    }

    // Kết quả phân trang do API trả về (GET api/products/paged?page=&pageSize=)
    public class PagedResult<T>
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
        public int TotalItems { get; set; }
        public int TotalPages { get; set; }
        public List<T> Data { get; set; } = new List<T>();
    }

    // ==================== DASHBOARD BÁO CÁO ====================
    public class TopProductDto
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public decimal Revenue { get; set; }
    }

    public class DailyRevenueDto
    {
        public string Date { get; set; } = string.Empty;
        public int Orders { get; set; }
        public decimal Revenue { get; set; }
    }

    public class CashierRowDto
    {
        public string CashierUsername { get; set; } = string.Empty;
        public int TotalOrders { get; set; }
        public decimal TotalRevenue { get; set; }
    }

    public class DashboardDto
    {
        public string Date { get; set; } = string.Empty;
        public int TotalOrders { get; set; }
        public decimal TotalRevenue { get; set; }
        public decimal AveragePerOrder { get; set; }
        public int TotalItemsSold { get; set; }
        public string BestSellerName { get; set; } = string.Empty;
        public int BestSellerQuantity { get; set; }
        public List<TopProductDto> TopProducts { get; set; } = new List<TopProductDto>();
        public List<CashierRowDto> Cashiers { get; set; } = new List<CashierRowDto>();
        public List<DailyRevenueDto> Daily { get; set; } = new List<DailyRevenueDto>();
    }

    // 4 chỉ số KPI (GET api/reports/dashboard-summary)
    public class DashboardSummaryDto
    {
        public decimal RevenueToday { get; set; }
        public int OrdersToday { get; set; }
        public int NewCustomers { get; set; }
        public int LowStock { get; set; }
    }

    // (GET api/reports/revenue-by-day?from=&to=)
    public class RevenueByDayResult
    {
        public string FromDate { get; set; } = string.Empty;
        public string ToDate { get; set; } = string.Empty;
        public decimal TotalRevenue { get; set; }
        public List<DailyRevenueDto> Data { get; set; } = new List<DailyRevenueDto>();
    }

    // (GET api/reports/top-products?date=&top=)
    public class TopProductsResult
    {
        public string Date { get; set; } = string.Empty;
        public int Top { get; set; }
        public List<TopProductDto> Data { get; set; } = new List<TopProductDto>();
    }

    // Kết quả trả về của POST api/orders/checkout
    public class CheckoutResultDto
    {
        public bool Success { get; set; }
        public int OrderId { get; set; }
        public string OrderCode { get; set; } = string.Empty;
        public decimal TotalAmount { get; set; }
        public decimal Discount { get; set; }
        public decimal FinalAmount { get; set; }
        public int RedeemPoints { get; set; }
        public int EarnedPoints { get; set; }
        public string? CustomerName { get; set; }
        public int? RewardPoints { get; set; }
    }

    // Thông báo lỗi dạng { "message": "..." } từ API
    public class ApiMessageDto
    {
        public string? Message { get; set; }
    }
}
