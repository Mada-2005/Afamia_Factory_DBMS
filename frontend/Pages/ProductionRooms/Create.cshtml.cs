using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using FactorySystemWebpage.Models;

namespace FactorySystemWebpage.Pages.ProductionRooms
{
    public class CreateModel : PageModel
    {
        [BindProperty]
        public ProductionRoom ProductionRoom { get; set; } = new ProductionRoom();

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
            TempData["SuccessMessage"] = "Production Room created successfully! (Database not connected yet)";
            return RedirectToPage("./Index");
        }
    }
}
