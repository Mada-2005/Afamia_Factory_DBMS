using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using FactorySystemWebpage.Models;

namespace FactorySystemWebpage.Pages.Customers
{
    public class CreateModel : PageModel
    {
        [BindProperty]
        public Customer Customer { get; set; } = new Customer();

        public IActionResult OnGet()
        {
            return Page();
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            // TODO: Save to database when ready
            TempData["SuccessMessage"] = "Customer created successfully! (Database not connected yet)";
            return RedirectToPage("./Index");
        }
    }
}
