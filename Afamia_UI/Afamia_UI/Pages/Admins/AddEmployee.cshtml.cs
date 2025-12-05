using Afamia_UI.Models;
using Afamia_UI.Models.Entities;
using Afamia_UI.Models.Queries;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Afamia_UI.Pages.Admins
{
    public class AddEmployeeModel : PageModel
    {
        [BindProperty]
        public Employee employee { get; set; }
        
        EmployeesServices empObj { get; set; }

   

        public AddEmployeeModel(DB db) { 
            
            empObj = new EmployeesServices(db);
        }
        public void OnGet()
        {
            employee = new Employee();
        }
        public IActionResult OnPost()
        {
            empObj.AddEmployee(employee);
            return RedirectToPage("/Admins/Employees");
        }
        public IActionResult OnPostAddPhone()
        {
            employee.Phone.Add("");
            return Page();
        }
        public IActionResult OnPostRemovePhone(int index)
        {
            employee.Phone.RemoveAt(index);
            return Page();
        }
    }

}