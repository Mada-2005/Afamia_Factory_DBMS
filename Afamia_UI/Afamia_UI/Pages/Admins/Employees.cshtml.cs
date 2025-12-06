using Afamia_UI.Models;
using Afamia_UI.Models.Entities;
using Afamia_UI.Models.Queries;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Afamia_UI.Pages.Admins
{
    public class EmployeesModel : PageModel
    {
        public List<Employee> employees { get; set; } = new List<Employee>();
        EmployeesServices empObj { get; set; }

        public EmployeesModel(DB db)
        {
            empObj = new EmployeesServices(db);
        }
        public void OnGet()
        {

            employees = empObj.GetEmployees();
        }

        public IActionResult OnPostDelete(int id)
        {
            empObj.DeleteEmployee(id);
            return RedirectToPage();
        }

        public IActionResult OnPostSearch(string Name)
        {
            if (string.IsNullOrWhiteSpace(Name))
            {
                employees = empObj.GetEmployees();
                return Page();
            }
            List<string> nameParts = Name.Split(' ', StringSplitOptions.RemoveEmptyEntries).ToList();
            string FName = nameParts.Count > 0 ? nameParts[0] : "";
            string LName = nameParts.Count > 1 ? nameParts[1] : "";
            if (!string.IsNullOrWhiteSpace(FName) && !string.IsNullOrWhiteSpace(LName))
            {
                employees = empObj.GetEmployeeByName(FName, LName);
            }
            else if (!string.IsNullOrWhiteSpace(FName))
            {
                employees = empObj.GetEmployeeByName(FName);
            }
                return Page();
        }
    }
}
