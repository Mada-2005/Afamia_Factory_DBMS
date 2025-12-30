using Afamia_UI.Models;
using Afamia_UI.Models.Entities;
using Afamia_UI.Models.Queries;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Afamia_UI.Pages.CEO
{
    public class ManageAdminsModel : PageModel
    {
        [BindProperty]
        public string Username { get; set; }

        [BindProperty]
        public string Password { get; set; }

        [BindProperty]
        public string ConfirmPassword { get; set; }

        public List<UserAccount> AdminAccounts { get; set; }
        public string SuccessMessage { get; set; }
        public string ErrorMessage { get; set; }

        private AdminAccountService adminService { get; set; }

        public ManageAdminsModel(DB db)
        {
            adminService = new AdminAccountService(db);
        }

        public void OnGet()
        {
            LoadAdmins();
        }

        public IActionResult OnPostCreate()
        {
            LoadAdmins();

            // Validation
            if (string.IsNullOrWhiteSpace(Username))
            {
                ErrorMessage = "Username is required.";
                return Page();
            }

            if (Username.Length < 3)
            {
                ErrorMessage = "Username must be at least 3 characters long.";
                return Page();
            }

            if (string.IsNullOrWhiteSpace(Password))
            {
                ErrorMessage = "Password is required.";
                return Page();
            }

            if (Password.Length < 6)
            {
                ErrorMessage = "Password must be at least 6 characters long.";
                return Page();
            }

            if (Password != ConfirmPassword)
            {
                ErrorMessage = "Passwords do not match.";
                return Page();
            }

            // Check if username already exists
            if (adminService.UsernameExists(Username))
            {
                ErrorMessage = $"Username '{Username}' is already taken.";
                return Page();
            }

            // Create the admin account
            bool success = adminService.CreateAdminAccount(Username, Password);

            if (success)
            {
                SuccessMessage = $"Admin account '{Username}' created successfully!";
                Username = "";
                Password = "";
                ConfirmPassword = "";
                LoadAdmins();
            }
            else
            {
                ErrorMessage = "Failed to create admin account. Please try again.";
            }

            return Page();
        }

        public IActionResult OnPostDelete(string username)
        {
            LoadAdmins();

            if (string.IsNullOrWhiteSpace(username))
            {
                ErrorMessage = "Invalid username.";
                return Page();
            }

            bool success = adminService.DeleteAdminAccount(username);

            if (success)
            {
                SuccessMessage = $"Admin account '{username}' deleted successfully.";
                LoadAdmins();
            }
            else
            {
                ErrorMessage = $"Failed to delete admin account '{username}'.";
            }

            return Page();
        }

        private void LoadAdmins()
        {
            AdminAccounts = adminService.GetAllAdmins();
        }
    }
}
