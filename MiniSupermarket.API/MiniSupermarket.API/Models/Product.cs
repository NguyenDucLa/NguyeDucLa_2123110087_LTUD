using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MiniSupermarket.API.Models
{
    [Table("Products")] // Ánh xạ thành bảng Products trong SQL Server
    public class Product
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProductId { get; set; }

        [Required(ErrorMessage = "Mã vạch sản phẩm không được trống")]
        [StringLength(50)]
        public string Barcode { get; set; } = string.Empty;

        [Required(ErrorMessage = "Tên sản phẩm không được để trống")]
        [StringLength(150)]
        public string ProductName { get; set; } = string.Empty;

        [Column(TypeName = "decimal(18,2)")]
        [Range(typeof(decimal), "0.01", "1000000000000", ErrorMessage = "Giá bán phải lớn hơn 0")]
        public decimal Price { get; set; } // Giá bán

        [Range(0, int.MaxValue, ErrorMessage = "Số lượng tồn kho không được âm")]
        public int StockQuantity { get; set; } // Số lượng tồn kho

        // Khóa ngoại (Foreign Key) liên kết đến bảng Categories
        public int CategoryId { get; set; }
        [ForeignKey("CategoryId")]
        public virtual Category? Category { get; set; }
    }
}