using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FactorySystemWebpage.Models
{
    // Junction table for Customer-Product many-to-many relationship
    public class CustomerProduct
    {
        [Key]
        public int CustomerProductId { get; set; }

        [Required]
        public int CustomerId { get; set; }

        [Required]
        public int ProductId { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal QuantityOrdered { get; set; }

        [Required]
        public DateTime OrderDate { get; set; }

        public DateTime? ShipDate { get; set; }

        [StringLength(50)]
        public string Status { get; set; } = "Pending"; // Pending, Shipped, Delivered

        [StringLength(500)]
        public string Notes { get; set; } = string.Empty;

        // Navigation properties
        [ForeignKey("CustomerId")]
        public Customer Customer { get; set; } = null!;

        [ForeignKey("ProductId")]
        public Product Product { get; set; } = null!;
    }
}
