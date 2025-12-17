using Afamia_UI.Models;
using Afamia_UI.Models.Entities;
using Afamia_UI.Models.Queries;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Afamia_UI.Pages.Admins.Employees_operations
{
    public class EmployeeScheduleModel : PageModel
    {
        public List<DailyWorkSchedule> schedules { get; set; } = new List<DailyWorkSchedule>();
        public Employee employee { get; set; }
        public DateTime? WorkDate { get; set; }

        DailyWorkScheduleServices scheduleService { get; set; }
        EmployeesServices employeeService { get; set; }

        public EmployeeScheduleModel(DB db)
        {
            scheduleService = new DailyWorkScheduleServices(db);
            employeeService = new EmployeesServices(db);
        }

        public IActionResult OnGet(int id, DateTime? workDate = null)
        {
            // Get employee information
            employee = employeeService.GetEmployeeById(id);

            if (employee == null)
            {
                return RedirectToPage("/Admins/Employees(operations)/Employees");
            }

            WorkDate = workDate;

            // Get employee's full schedule
            schedules = scheduleService.GetEmployeeSchedule(id);

            // Filter by date if specified
            if (workDate.HasValue)
            {
                schedules = schedules
                    .Where(s => s.Work_Date.Date == workDate.Value.Date)
                    .ToList();
            }

            return Page();
        }

        public IActionResult OnPostDelete(int id, int employeeId, DateTime? workDate)
        {
            scheduleService.DeleteSchedule(id);
            return RedirectToPage(new { id = employeeId, workDate = workDate?.ToString("yyyy-MM-dd") });
        }
    }
}