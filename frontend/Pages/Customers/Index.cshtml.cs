using Microsoft.AspNetCore.Mvc.RazorPages;
using FactorySystemWebpage.Models;

namespace FactorySystemWebpage.Pages.Customers
{
    public class IndexModel : PageModel
    {
        public IList<Customer> Customers { get; set; } = new List<Customer>();

        public void OnGet()
        {
            // TODO: Load customers from database
        }
    }
}
