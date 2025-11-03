using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FactorySystemWebpage.Models
{
    public class DailyWorkSchedule
    {
        [Key]
        public int ScheduleId { get; set; }

        [Required]
        public int EmployeeId { get; set; }

        [Required]
        public DateTime WorkDate { get; set; }

        [Required]
        public TimeSpan StartTime { get; set; }

        [Required]
        public TimeSpan EndTime { get; set; }

        public int? ProductionRoomId { get; set; }

        public int? ProductionLineId { get; set; }

        [StringLength(100)]
        public string SectionAssignment { get; set; } = string.Empty; // Mixing, Forming, Baking, Packaging

        public int WorkedHours { get; set; }

        public bool IsPresent { get; set; } = true;

        [StringLength(500)]
        public string Notes { get; set; } = string.Empty;

        // Navigation properties
        [ForeignKey("EmployeeId")]
        public Employee Employee { get; set; } = null!;

        [ForeignKey("ProductionRoomId")]
        public ProductionRoom? ProductionRoom { get; set; }

        [ForeignKey("ProductionLineId")]
        public ProductionPipelineLine? ProductionLine { get; set; }
    }
}
