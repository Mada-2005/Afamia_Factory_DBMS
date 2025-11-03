using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FactorySystemWebpage.Models
{
    public class RawMaterial
    {
        [Key]
        public int MaterialId { get; set; }

        [Required]
        [StringLength(100)]
        public string MaterialName { get; set; } = string.Empty;

        [Required]
        [StringLength(50)]
        public string BatchNumber { get; set; } = string.Empty;

        [Required]
        public DateTime ProductionDate { get; set; }

        [Required]
        public DateTime ExpirationDate { get; set; }

        [Required]
        public DateTime ReceivedDate { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Quantity { get; set; }

        [StringLength(50)]
        public string Unit { get; set; } = string.Empty; // kg, liters, etc.

        // Foreign Key
        public int VendorId { get; set; }

        // Navigation properties
        [ForeignKey("VendorId")]
        public Vendor Vendor { get; set; } = null!;

        public ICollection<Inventory> Inventories { get; set; } = new List<Inventory>();
        public ICollection<ProductMaterial> ProductMaterials { get; set; } = new List<ProductMaterial>();
    }
}
