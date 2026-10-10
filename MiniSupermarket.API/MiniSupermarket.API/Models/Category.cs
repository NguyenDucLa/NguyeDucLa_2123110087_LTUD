using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace MiniSupermarket.API.Models
{
    [Table("Categories")] // Ánh xạ thành bảng Categories trong SQL Server
    public class Category
    {
        [Key] // Khóa chính
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // Tự động tăng (Identity)
        public int CategoryId { get; set; }

        [Required(ErrorMessage = "Tên nhóm hàng không được để trống!")]
        [StringLength(100, ErrorMessage = "Tên nhóm hàng không vượt quá 100 ký tự")]
        public string CategoryName { get; set; } = string.Empty;

        [StringLength(255)]
        public string? Description { get; set; }

        // Quan hệ 1 - Nhiều: Một danh mục có thể chứa nhiều sản phẩm
        [JsonIgnore] // Tránh lỗi lặp vòng vô tận khi chuyển đổi sang JSON
        public virtual ICollection<Product>? Products { get; set; }
    }
}