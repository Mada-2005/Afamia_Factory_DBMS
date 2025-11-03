using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FactorySystemWebpage.Models
{
    public class ProductionPipelineLine
    {
        [Key]
        public int LineId { get; set; }

        [Required]
        [StringLength(100)]
        public string LineName { get; set; } = string.Empty; // Line 1, Line 2, etc.

        [Required]
        public int ProductionRoomId { get; set; }

        [StringLength(500)]
        public string Sections { get; set; } = string.Empty; // Mixing, Forming, Baking, Packaging

        public int Capacity { get; set; } // Units per hour

        public bool IsActive { get; set; } = true;

        [StringLength(500)]
        public string Description { get; set; } = string.Empty;

        // Navigation property
        [ForeignKey("ProductionRoomId")]
        public ProductionRoom ProductionRoom { get; set; } = null!;

        public ICollection<EmployeeProductionLine> EmployeeProductionLines { get; set; } = new List<EmployeeProductionLine>();
        public ICollection<DailyWorkSchedule> DailyWorkSchedules { get; set; } = new List<DailyWorkSchedule>();
    }
}
