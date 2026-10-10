using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace MiniSupermarket.API.Models
{
    [Table("Orders")] // Hóa đơn bán hàng tại quầy POS
    public class Order
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int OrderId { get; set; }

        [Required]
        [StringLength(30)]
        public string OrderCode { get; set; } = string.Empty; // Mã hóa đơn (ví dụ HD20261010-0001)

        public DateTime OrderDate { get; set; } = DateTime.Now;

        // Khách hàng thân thiết (có thể null - khách vãng lai)
        public int? CustomerId { get; set; }
        [ForeignKey("CustomerId")]
        public virtual Customer? Customer { get; set; }

        [StringLength(50)]
        public string CashierUsername { get; set; } = string.Empty; // Nhân viên thu ngân

        [Column(TypeName = "decimal(18,2)")]
        public decimal TotalAmount { get; set; }

        [StringLength(30)]
        public string Status { get; set; } = "Paid";

        public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();
    }
}
