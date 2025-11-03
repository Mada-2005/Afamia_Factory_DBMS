using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using FactorySystemWebpage.Helpers;

namespace FactorySystemWebpage.Pages
{
    public class LogoutModel : PageModel
    {
        public IActionResult OnGet()
        {
            HttpContext.Session.ClearUserSession();
            return RedirectToPage("/Login");
        }
    }
}
