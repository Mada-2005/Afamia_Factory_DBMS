using Afamia_UI.Models;
using Afamia_UI.Models.Entities;
using Afamia_UI.Models.Queries;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;

namespace Afamia_UI.Pages.Admins.DailyWorkSchedule_operations
{
    public class EditModel : PageModel
    {
        [BindProperty]
        public DailyWorkSchedule schedule { get; set; }

        public List<Employee> employees { get; set; } = new List<Employee>();
        public List<PipelineSection> sections { get; set; } = new List<PipelineSection>();
        public string ErrorMessage { get; set; }
        public int RoomId { get; set; }

        DailyWorkScheduleServices scheduleService { get; set; }
        ProductionLineServices pipelineService { get; set; }

        public EditModel(DB db)
        {
            scheduleService = new DailyWorkScheduleServices(db);
            pipelineService = new ProductionLineServices(db);
        }

        public IActionResult OnGet(int id)
        {
            // Load the schedule by ID
            schedule = scheduleService.GetScheduleById(id);

            if (schedule == null)
            {
                return RedirectToPage("/Admins/Index");
            }

            // Get pipeline information
            var pipeline = pipelineService.GetProductionLineById(schedule.Pipeline_ID);
            if (pipeline != null)
            {
                RoomId = pipeline.Room_ID;
            }

            // Load available employees and sections
            employees = scheduleService.GetAvailableEmployees();
            sections = scheduleService.GetSectionsByPipeline(schedule.Pipeline_ID);

            return Page();
        }

        public IActionResult OnPost()
        {
            // Validation
            if (schedule.EmployeeID == 0)
            {
                ErrorMessage = "Please select an employee";
                employees = scheduleService.GetAvailableEmployees();
                sections = scheduleService.GetSectionsByPipeline(schedule.Pipeline_ID);
                return Page();
            }

            if (schedule.SectionID == 0)
            {
                ErrorMessage = "Please select a section";
                employees = scheduleService.GetAvailableEmployees();
                sections = scheduleService.GetSectionsByPipeline(schedule.Pipeline_ID);
                return Page();
            }

            if (!schedule.Start_Time.HasValue)
            {
                ErrorMessage = "Please provide a start time";
                employees = scheduleService.GetAvailableEmployees();
                sections = scheduleService.GetSectionsByPipeline(schedule.Pipeline_ID);
                return Page();
            }

            // Check if end time is after start time
            if (schedule.End_Time.HasValue && schedule.End_Time.Value <= schedule.Start_Time.Value)
            {
                ErrorMessage = "End time must be after start time";
                employees = scheduleService.GetAvailableEmployees();
                sections = scheduleService.GetSectionsByPipeline(schedule.Pipeline_ID);
                return Page();
            }

            // Try to update the schedule
            try
            {
                scheduleService.UpdateSchedule(schedule);
            }
            catch (SqlException ex)
            {
                if (ex.Message.Contains("CHK_Time_Order"))
                {
                    ErrorMessage = "End time must be after start time";
                }
                else
                {
                    ErrorMessage = "Database error: " + ex.Message;
                }
                employees = scheduleService.GetAvailableEmployees();
                sections = scheduleService.GetSectionsByPipeline(schedule.Pipeline_ID);
                return Page();
            }
            catch (Exception ex)
            {
                ErrorMessage = "Error: " + ex.Message;
                employees = scheduleService.GetAvailableEmployees();
                sections = scheduleService.GetSectionsByPipeline(schedule.Pipeline_ID);
                return Page();
            }

            return RedirectToPage("/Admins/DailyWorkSchedule(operations)/DailyWorkSchedule",
                new { pipelineId = schedule.Pipeline_ID, workDate = schedule.Work_Date.ToString("yyyy-MM-dd") });
        }
    }
}
