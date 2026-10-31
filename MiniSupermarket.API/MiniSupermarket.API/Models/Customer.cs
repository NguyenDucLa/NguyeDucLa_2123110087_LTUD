using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MiniSupermarket.API.Models
{
    [Table("Customers")] // Ánh xạ thành bảng Customers trong SQL Server
    public class Customer
    {
        [Key] // Khóa chính
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // Tự động tăng IDENTITY(1,1)
        public int CustomerId { get; set; }

        [Required(ErrorMessage = "Tên khách hàng không được để trống!")]
        [StringLength(100, ErrorMessage = "Tên khách hàng không vượt quá 100 ký tự")]
        public string CustomerName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Số điện thoại không được để trống!")]
        [StringLength(15, ErrorMessage = "Số điện thoại không vượt quá 15 ký tự")]
        [Column(TypeName = "varchar(15)")]
        public string PhoneNumber { get; set; } = string.Empty;

        // Địa chỉ giao hàng đầy đủ (số nhà, tên đường, phường/xã, quận/huyện, tỉnh/thành phố)
        [StringLength(255)]
        public string? Address { get; set; }

        public int RewardPoints { get; set; } = 0; // Điểm tích lũy, mặc định 0

        [StringLength(50)]
        public string MembershipRank { get; set; } = "Chuẩn"; // Hạng thẻ, mặc định "Chuẩn"

        // Ngày đăng ký thành viên (dùng cho thống kê khách hàng mới)
        [Column(TypeName = "datetime2")]
        public DateTime CreatedDate { get; set; }
    }
}
