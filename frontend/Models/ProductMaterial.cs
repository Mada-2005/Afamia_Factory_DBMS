using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FactorySystemWebpage.Models
{
    // Junction table for Product-RawMaterial many-to-many relationship
    public class ProductMaterial
    {
        [Key]
        public int ProductMaterialId { get; set; }

        [Required]
        public int ProductId { get; set; }

        [Required]
        public int MaterialId { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal QuantityUsed { get; set; }

        [StringLength(50)]
        public string Unit { get; set; } = string.Empty;

        // Navigation properties
        [ForeignKey("ProductId")]
        public Product Product { get; set; } = null!;

        [ForeignKey("MaterialId")]
        public RawMaterial RawMaterial { get; set; } = null!;
    }
}
