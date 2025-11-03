using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FactorySystemWebpage.Models
{
    // Junction table for Employee-ProductionLine many-to-many relationship
    public class EmployeeProductionLine
    {
        [Key]
        public int EmployeeLineId { get; set; }

        [Required]
        public int EmployeeId { get; set; }

        [Required]
        public int LineId { get; set; }

        [Required]
        [StringLength(100)]
        public string Section { get; set; } = string.Empty; // Mixing, Forming, Baking, Packaging

        public DateTime AssignmentDate { get; set; }

        public bool IsActive { get; set; } = true;

        [StringLength(500)]
        public string Notes { get; set; } = string.Empty;

        // Navigation properties
        [ForeignKey("EmployeeId")]
        public Employee Employee { get; set; } = null!;

        [ForeignKey("LineId")]
        public ProductionPipelineLine ProductionLine { get; set; } = null!;
    }
}
