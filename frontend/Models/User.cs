using System.ComponentModel.DataAnnotations;

namespace FactorySystemWebpage.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }

        [Required]
        [StringLength(50)]
        public string Username { get; set; } = string.Empty;

        [Required]
        [StringLength(255)]
        public string PasswordHash { get; set; } = string.Empty;

        [Required]
        [StringLength(20)]
        public string Role { get; set; } = "Employee"; // Admin or Employee

        public int? EmployeeId { get; set; } // Link to Employee table

        [StringLength(100)]
        public string FullName { get; set; } = string.Empty;

        public bool IsActive { get; set; } = true;

        public DateTime CreatedDate { get; set; } = DateTime.Now;

        public DateTime? LastLogin { get; set; }

        // Navigation property
        public Employee? Employee { get; set; }
    }
}
