using System.ComponentModel.DataAnnotations;

namespace FactorySystemWebpage.Models
{
    public class Employee
    {
        [Key]
        public int EmployeeId { get; set; }

        [Required]
        [StringLength(100)]
        public string FullName { get; set; } = string.Empty;

        [Required]
        [StringLength(14)]
        public string NationalId { get; set; } = string.Empty;

        [Phone]
        [StringLength(20)]
        public string Phone { get; set; } = string.Empty;

        [EmailAddress]
        [StringLength(100)]
        public string Email { get; set; } = string.Empty;

        [Required]
        [StringLength(50)]
        public string Role { get; set; } = string.Empty; // Production Worker, Supervisor, etc.

        [StringLength(200)]
        public string Address { get; set; } = string.Empty;

        public DateTime HireDate { get; set; }

        public bool IsActive { get; set; } = true;

        // Navigation properties
        public ICollection<MonthlyPay> MonthlyPays { get; set; } = new List<MonthlyPay>();
        public ICollection<DailyWorkSchedule> DailyWorkSchedules { get; set; } = new List<DailyWorkSchedule>();
        public ICollection<EmployeeProductionLine> EmployeeProductionLines { get; set; } = new List<EmployeeProductionLine>();
    }
}
