namespace Afamia_UI.Models.Entities
{
    public class DailyWorkSchedule
    {
        public int Schedule_ID { get; set; }
        public int EmployeeID { get; set; }
        public DateTime Work_Date { get; set; }
        public TimeSpan? Start_Time { get; set; }
        public TimeSpan? End_Time { get; set; }
        public int SectionID { get; set; }
        public int Pipeline_ID { get; set; }

        // Additional properties for display purposes
        public string EmployeeName { get; set; }
        public string SectionName { get; set; }
        public string PipelineName { get; set; }

        public DailyWorkSchedule()
        {
            Work_Date = DateTime.Today;
        }
    }
}
