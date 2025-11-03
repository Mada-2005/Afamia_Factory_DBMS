using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FactorySystemWebpage.Models
{
    public class MonthlyPay
    {
        [Key]
        public int PayId { get; set; }

        [Required]
        public int EmployeeId { get; set; }

        [Required]
        public int Month { get; set; } // 1-12

        [Required]
        public int Year { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal BaseSalary { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Bonus { get; set; } = 0;

        [Column(TypeName = "decimal(18,2)")]
        public decimal Deductions { get; set; } = 0;

        [Column(TypeName = "decimal(18,2)")]
        public decimal AdditionalIncome { get; set; } = 0;

        [Column(TypeName = "decimal(18,2)")]
        public decimal TotalPay { get; set; }

        public int WorkedHours { get; set; }

        public DateTime PaymentDate { get; set; }

        [StringLength(500)]
        public string Notes { get; set; } = string.Empty;

        // Navigation property
        [ForeignKey("EmployeeId")]
        public Employee Employee { get; set; } = null!;
    }
}
