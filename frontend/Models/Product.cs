using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FactorySystemWebpage.Models
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }

        [Required]
        [StringLength(100)]
        public string ProductCode { get; set; } = string.Empty; // Unique code for traceability

        [Required]
        [StringLength(100)]
        public string ProductName { get; set; } = string.Empty;

        [Required]
        [StringLength(50)]
        public string ProductType { get; set; } = string.Empty; // Crackers, Peanuts, Snacks, Biscuits

        [Required]
        public DateTime ProductionDate { get; set; }

        [Required]
        public DateTime ExpirationDate { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Quantity { get; set; }

        [StringLength(50)]
        public string Unit { get; set; } = string.Empty; // units, boxes, kg

        public int? ProductionRoomId { get; set; }

        public DateTime? ShipDate { get; set; }

        [StringLength(500)]
        public string Notes { get; set; } = string.Empty;

        // Navigation properties
        [ForeignKey("ProductionRoomId")]
        public ProductionRoom? ProductionRoom { get; set; }

        public ICollection<ProductMaterial> ProductMaterials { get; set; } = new List<ProductMaterial>();
        public ICollection<Inventory> Inventories { get; set; } = new List<Inventory>();
        public ICollection<CustomerProduct> CustomerProducts { get; set; } = new List<CustomerProduct>();
    }
}
