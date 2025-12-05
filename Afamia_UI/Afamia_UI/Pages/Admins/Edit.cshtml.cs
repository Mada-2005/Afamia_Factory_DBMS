using Afamia_UI.Models;
using Afamia_UI.Models.Entities;
using Afamia_UI.Models.Queries;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Afamia_UI.Pages.Admins
{
    public class EditModel : PageModel
    {
        [BindProperty]
        public Employee employee { get; set; }
        [BindProperty]
        public int idx { get; set; }

        public EmployeesServices empObj { get; set; }
        
        public EditModel(DB db)
        {
            empObj = new EmployeesServices(db);
        }
        public void OnGet(int id)
        {
            Console.WriteLine("got Id ");
            Console.WriteLine(id);
            employee = empObj.GetEmployeeById(id);

        }
        public IActionResult OnPost()
        {
            Console.WriteLine("Posted Id ");
            Console.WriteLine(employee.Id);
            empObj.EditEmployee(employee);
            return RedirectToPage("/Admins/Employees");
        }

        public IActionResult OnPostRemovePhone(int index)
        {
            employee.Phone.RemoveAt(index);
            empObj.EditEmployee(employee);
            return Page();
        }

        public IActionResult OnPostAddPhone()
        {
            employee.Phone.Add("");
            return Page();
        }
    }
}
