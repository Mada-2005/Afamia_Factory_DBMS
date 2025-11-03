using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FactorySystemWebpage.Pages.Dashboard
{
    public class IndexModel : PageModel
    {
        public void OnGet()
        {
            // TODO: Load dashboard statistics from database
            // - Count of raw materials
            // - Count of products
            // - Count of active employees
            // - Count of active production rooms
            // - List of expiring materials
            // - Recent production
        }

        public void OnPost(string productCode)
        {
            // TODO: Search for product by code and display traceability information
        }
    }
}
