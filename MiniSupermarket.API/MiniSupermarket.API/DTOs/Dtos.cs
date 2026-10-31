using MiniSupermarket.API.Models;
using System.ComponentModel.DataAnnotations;

namespace MiniSupermarket.API.DTOs
{
    // ==================== CATEGORY ====================
    public class CategoryDto
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; } = string.Empty;
        public string? Description { get; set; }
    }

    public class CategoryRequestDto
    {
        [Required(ErrorMessage = "Tên nhóm hàng không được để trống!")]
        [StringLength(100, ErrorMessage = "Tên nhóm hàng không vượt quá 100 ký tự")]
        public string CategoryName { get; set; } = string.Empty;

        [StringLength(255)]
        public string? Description { get; set; }
    }

    // ==================== PRODUCT ====================
    public class ProductDto
    {
        public int ProductId { get; set; }
        public string Barcode { get; set; } = string.Empty;
        public string ProductName { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int StockQuantity { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; } = string.Empty;
    }

    public class ProductRequestDto
    {
        [Required(ErrorMessage = "Mã vạch sản phẩm không được trống")]
        [StringLength(50)]
        public string Barcode { get; set; } = string.Empty;

        [Required(ErrorMessage = "Tên sản phẩm không được để trống")]
        [StringLength(150)]
        public string ProductName { get; set; } = string.Empty;

        [Range(typeof(decimal), "0.01", "1000000000000", ErrorMessage = "Giá bán phải lớn hơn 0")]
        public decimal Price { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Số lượng tồn kho không được âm")]
        public int StockQuantity { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Vui lòng chọn nhóm hàng hợp lệ")]
        public int CategoryId { get; set; }
    }

    // ==================== CUSTOMER ====================
    public class CustomerDto
    {
        public int CustomerId { get; set; }
        public string CustomerName { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string? Address { get; set; }
        public int RewardPoints { get; set; }
        public string MembershipRank { get; set; } = string.Empty;
    }

    public class CustomerRequestDto
    {
        [Required(ErrorMessage = "Tên khách hàng không được để trống!")]
        [StringLength(100)]
        public string CustomerName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Số điện thoại không được để trống!")]
        [StringLength(15)]
        public string PhoneNumber { get; set; } = string.Empty;

        [StringLength(255)]
        public string? Address { get; set; }

        [Range(0, int.MaxValue)]
        public int RewardPoints { get; set; }

        [StringLength(50)]
        public string MembershipRank { get; set; } = "Chuẩn";
    }

    // ==================== USER ====================
    public class UserDto
    {
        public int UserId { get; set; }
        public string Username { get; set; } = string.Empty;
        public string? FullName { get; set; }
        public string? Email { get; set; }
        public string Role { get; set; } = string.Empty;
        public bool IsActive { get; set; }
    }

    // ==================== ORDER ====================
    public class OrderDetailDto
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal LineTotal { get; set; }
    }

    public class OrderDto
    {
        public int OrderId { get; set; }
        public string OrderCode { get; set; } = string.Empty;
        public DateTime OrderDate { get; set; }
        public int? CustomerId { get; set; }
        public string CashierUsername { get; set; } = string.Empty;
        public decimal TotalAmount { get; set; }
        public string Status { get; set; } = string.Empty;
        public List<OrderDetailDto> OrderDetails { get; set; } = new();
    }

    // ==================== PHÂN TRANG ====================
    public class PagedResult<T>
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
        public int TotalItems { get; set; }
        public int TotalPages { get; set; }
        public List<T> Data { get; set; } = new();
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

    public class DashboardDto
    {
        public string Date { get; set; } = string.Empty;
        public int TotalOrders { get; set; }
        public decimal TotalRevenue { get; set; }
        public decimal AveragePerOrder { get; set; }
        public int TotalItemsSold { get; set; }
        public string BestSellerName { get; set; } = string.Empty;
        public int BestSellerQuantity { get; set; }
        public List<TopProductDto> TopProducts { get; set; } = new();
        public List<CashierRevenueDto> Cashiers { get; set; } = new();
        public List<DailyRevenueDto> Daily { get; set; } = new();
    }
}
