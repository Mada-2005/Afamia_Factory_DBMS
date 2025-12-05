using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

namespace Afamia_UI.Pages
{
    public class HomeModel : PageModel
    {
        public IActionResult OnGet()
        {
            if (User.Identity.IsAuthenticated)
            {
                // Get user's role
                var role = User.FindFirst(ClaimTypes.Role)?.Value;

                // Redirect based on role
                return role switch
                {
                    "Admin" => RedirectToPage("/Admins/Index"),
                    "CEO" => RedirectToPage("/CEO/Index"),
                    "Employee" => RedirectToPage("/Employees/Index"),
                    _ => RedirectToPage("/Login")
                };
            }

            // Not logged in → go to login
            return RedirectToPage("/Login");
        }
    }
   }

