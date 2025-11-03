using System.ComponentModel.DataAnnotations;

namespace FactorySystemWebpage.Models
{
    public class ProductionRoom
    {
        [Key]
        public int RoomId { get; set; }

        [Required]
        [StringLength(100)]
        public string RoomName { get; set; } = string.Empty;

        [StringLength(50)]
        public string RoomType { get; set; } = string.Empty; // Crackers, Biscuits, Peanuts, Snacks

        [StringLength(100)]
        public string Location { get; set; } = string.Empty; // Building A, Building B

        public bool IsActive { get; set; } = true;

        [StringLength(500)]
        public string Description { get; set; } = string.Empty;

        // Navigation properties
        public ICollection<ProductionPipelineLine> ProductionLines { get; set; } = new List<ProductionPipelineLine>();
        public ICollection<Product> Products { get; set; } = new List<Product>();
        public ICollection<DailyWorkSchedule> DailyWorkSchedules { get; set; } = new List<DailyWorkSchedule>();
    }
}
