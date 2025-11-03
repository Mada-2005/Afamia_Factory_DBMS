using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FactorySystemWebpage.Models
{
    public class Inventory
    {
        [Key]
        public int InventoryId { get; set; }

        [Required]
        [StringLength(100)]
        public string Location { get; set; } = string.Empty; // Warehouse A-3, etc.

        [StringLength(50)]
        public string InventoryType { get; set; } = string.Empty; // RawMaterial, FinishedProduct

        public int? RawMaterialId { get; set; }

        public int? ProductId { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Quantity { get; set; }

        [StringLength(50)]
        public string Unit { get; set; } = string.Empty;

        [Required]
        public DateTime EntryDate { get; set; }

        public DateTime? ExpirationDate { get; set; }

        [StringLength(500)]
        public string Notes { get; set; } = string.Empty;

        // Navigation properties
        [ForeignKey("RawMaterialId")]
        public RawMaterial? RawMaterial { get; set; }

        [ForeignKey("ProductId")]
        public Product? Product { get; set; }
    }
}
