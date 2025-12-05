using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

namespace Afamia_UI.Pages
{
    public class LoginModel : PageModel
    {
        [BindProperty]
        public String Username { get; set; }
        [BindProperty]
        public String Password { get; set; }

        [BindProperty]
        public bool RememberMe { get; set; }

        private readonly Models.Queries.Login loginQuery;

        public LoginModel(Models.Queries.Login loginQuery)
        {
            this.loginQuery = loginQuery;
        }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var userType = loginQuery.VerifyCredentials(Username, Password);
            if (userType == null)
            {
                // Add error message
                ModelState.AddModelError(string.Empty, "Invalid username or password");
                // Show login form again with error
                return Page();
            }
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, Username),
                new Claim(ClaimTypes.Role, userType)  // "Admin", "CEO", or "Employee"
            };
            var claimsIdentity = new ClaimsIdentity(
                claims,
                CookieAuthenticationDefaults.AuthenticationScheme);
            // Step 5: Create principal (represents the user)
            var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);



            var authProperties = new AuthenticationProperties
            {
                IsPersistent = RememberMe
            };

            // Only set ExpiresUtc if RememberMe is true
            if (RememberMe)
            {
                authProperties.ExpiresUtc = DateTimeOffset.UtcNow.AddHours(8);
            }
            // If RememberMe is false, don't set ExpiresUtc at all!
            Console.WriteLine($"RememberMe: {RememberMe}");
            Console.WriteLine($"IsPersistent: {authProperties.IsPersistent}");
            Console.WriteLine($"Expires: {authProperties.ExpiresUtc}");
            // Step 6: Sign in - creates encrypted cookie
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal, authProperties);  

            return userType switch
            {
                "Admin" => RedirectToPage("/Admins/Index"),
                "CEO" => RedirectToPage("/CEO/Index"),
                "Employee" => RedirectToPage("/Employees/Index"),
                _ => RedirectToPage("/Login")  // Fallback (shouldn't happen)
            };
        }
    }
}
