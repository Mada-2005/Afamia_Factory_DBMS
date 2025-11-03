using System.ComponentModel.DataAnnotations;

namespace FactorySystemWebpage.Models
{
    public class Customer
    {
        [Key]
        public int CustomerId { get; set; }

        [Required]
        [StringLength(100)]
        public string CustomerName { get; set; } = string.Empty;

        [Required]
        [StringLength(50)]
        public string CustomerType { get; set; } = string.Empty; // Trader, Supermarket, Distributor

        [StringLength(100)]
        public string ContactPerson { get; set; } = string.Empty;

        [Phone]
        [StringLength(20)]
        public string Phone { get; set; } = string.Empty;

        [EmailAddress]
        [StringLength(100)]
        public string Email { get; set; } = string.Empty;

        [Required]
        [StringLength(200)]
        public string DeliveryLocation { get; set; } = string.Empty;

        [StringLength(200)]
        public string Address { get; set; } = string.Empty;

        public bool IsActive { get; set; } = true;

        // Navigation property
        public ICollection<CustomerProduct> CustomerProducts { get; set; } = new List<CustomerProduct>();
    }
}
