using Afamia_UI.Models;
using Afamia_UI.Models.Entities;
using Afamia_UI.Models.Queries;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Afamia_UI.Pages.Admins.Customers_operations_
{
    public class CustomersModel : PageModel
    {
        public List<Customer> customers { get; set; } = new List<Customer>();
        public CustomerServices custObj { get; set; }

        public CustomersModel(DB db)
        {
            custObj = new CustomerServices(db);
        }

        public void OnGet()
        {
            customers = custObj.GetAllCustomers();
        }

        public IActionResult OnPostDelete(int id)
        {
            custObj.DeleteCustomer(id);
            return RedirectToPage();
        }
    }
}
