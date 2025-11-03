using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using FactorySystemWebpage.Helpers;

namespace FactorySystemWebpage.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public IActionResult OnGet()
        {
            // Check if user is logged in
            if (!HttpContext.Session.IsLoggedIn())
            {
                // Redirect to login if not authenticated
                return RedirectToPage("/Login");
            }

            // Redirect based on role
            if (HttpContext.Session.IsAdmin())
            {
                return RedirectToPage("/Dashboard/Admin/Index");
            }
            else
            {
                return RedirectToPage("/Dashboard/Employee/Index");
            }
        }
    }
}
