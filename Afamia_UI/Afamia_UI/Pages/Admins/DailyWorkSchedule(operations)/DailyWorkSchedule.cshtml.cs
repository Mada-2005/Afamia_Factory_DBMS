using Afamia_UI.Models;
using Afamia_UI.Models.Entities;
using Afamia_UI.Models.Queries;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Afamia_UI.Pages.Admins.DailyWorkSchedule_operations
{
    public class DailyWorkScheduleModel : PageModel
    {
        public List<DailyWorkSchedule> schedules { get; set; } = new List<DailyWorkSchedule>();
        public int PipelineId { get; set; }
        public int RoomId { get; set; }
        public DateTime WorkDate { get; set; }
        public string PipelineName { get; set; }

        DailyWorkScheduleServices scheduleService { get; set; }
        ProductionLineServices pipelineService { get; set; }

        public DailyWorkScheduleModel(DB db)
        {
            scheduleService = new DailyWorkScheduleServices(db);
            pipelineService = new ProductionLineServices(db);
        }

        public void OnGet(int pipelineId, DateTime? workDate)
        {
            PipelineId = pipelineId;
            WorkDate = workDate ?? DateTime.Today;

            // Get pipeline information
            var pipeline = pipelineService.GetProductionLineById(pipelineId);
            if (pipeline != null)
            {
                PipelineName = pipeline.Name;
                RoomId = pipeline.Room_ID;
            }

            // Get schedules for the specified date and pipeline
            schedules = scheduleService.GetSchedulesByDateAndPipeline(WorkDate, pipelineId);
        }

        public IActionResult OnPostDelete(int id, int pipelineId, DateTime workDate)
        {
            scheduleService.DeleteSchedule(id);
            return RedirectToPage(new { pipelineId = pipelineId, workDate = workDate.ToString("yyyy-MM-dd") });
        }
    }
}
