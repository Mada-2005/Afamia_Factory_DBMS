using Afamia_UI.Models;
using Afamia_UI.Models.Entities;
using Afamia_UI.Models.Queries;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Afamia_UI.Pages.Admins
{
    public class EmployeesModel : PageModel
    {
        public List<Employee> employees { get; set; }
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
    }
}
