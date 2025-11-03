using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using FactorySystemWebpage.Helpers;
using System.ComponentModel.DataAnnotations;

namespace FactorySystemWebpage.Pages
{
    public class LoginModel : PageModel
    {
        // TODO: Inject FactoryDbContext when database is ready
        // private readonly FactoryDbContext _context;

        [BindProperty]
        [Required(ErrorMessage = "Username is required")]
        public string Username { get; set; } = string.Empty;

        [BindProperty]
        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; } = string.Empty;

        [BindProperty]
        public string Language { get; set; } = "ar";

        public string ErrorMessage { get; set; } = string.Empty;

        public bool IsArabic => Language == "ar";

        public void OnGet(string? lang)
        {
            if (!string.IsNullOrEmpty(lang))
            {
                Language = lang;
            }
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                ErrorMessage = IsArabic ? "يرجى ملء جميع الحقول" : "Please fill all fields";
                return Page();
            }

            // TODO: Replace with actual database authentication
            // For now, use demo credentials
            bool isValidUser = false;
            string role = "";
            int? employeeId = null;
            string fullName = "";

            if (Username == "admin" && Password == "admin123")
            {
                isValidUser = true;
                role = "Admin";
                fullName = IsArabic ? "المدير العام" : "System Administrator";
            }
            else if (Username == "employee" && Password == "emp123")
            {
                isValidUser = true;
                role = "Employee";
                employeeId = 1; // Demo employee ID
                fullName = IsArabic ? "أحمد حسن" : "Ahmed Hassan";
            }

            /* TODO: Uncomment when database is ready
            var user = _context.Users
                .Include(u => u.Employee)
                .FirstOrDefault(u => u.Username == Username && u.IsActive);

            if (user != null && PasswordHelper.VerifyPassword(Password, user.PasswordHash))
            {
                isValidUser = true;
                role = user.Role;
                employeeId = user.EmployeeId;
                fullName = user.FullName;

                user.LastLogin = DateTime.Now;
                _context.SaveChanges();
            }
            */

            if (isValidUser)
            {
                // Set session
                HttpContext.Session.SetUserSession(1, Username, role, employeeId, fullName);

                // Store language preference
                HttpContext.Session.SetString("Language", Language);

                // Redirect based on role
                if (role == "Admin")
                {
                    return RedirectToPage("/Dashboard/Admin/Index");
                }
                else
                {
                    return RedirectToPage("/Dashboard/Employee/Index");
                }
            }

            ErrorMessage = IsArabic
                ? "اسم المستخدم أو كلمة المرور غير صحيحة"
                : "Invalid username or password";
            return Page();
        }
    }
}
