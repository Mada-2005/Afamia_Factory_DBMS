using System.ComponentModel.DataAnnotations;

namespace FactorySystemWebpage.Models
{
    public class Vendor
    {
        [Key]
        public int VendorId { get; set; }

        [Required]
        [StringLength(100)]
        public string CompanyName { get; set; } = string.Empty;

        [StringLength(100)]
        public string ContactPerson { get; set; } = string.Empty;

        [Phone]
        [StringLength(20)]
        public string Phone { get; set; } = string.Empty;

        [EmailAddress]
        [StringLength(100)]
        public string Email { get; set; } = string.Empty;

        [StringLength(200)]
        public string Address { get; set; } = string.Empty;

        // Navigation property
        public ICollection<RawMaterial> RawMaterials { get; set; } = new List<RawMaterial>();
    }
}
