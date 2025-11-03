using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using FactorySystemWebpage.Models;

namespace FactorySystemWebpage.Pages.Vendors
{
    public class CreateModel : PageModel
    {
        // TODO: Inject FactoryDbContext when database is ready
        // private readonly FactoryDbContext _context;

        // public CreateModel(FactoryDbContext context)
        // {
        //     _context = context;
        // }

        [BindProperty]
        public Vendor Vendor { get; set; } = new Vendor();

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
            // _context.Vendors.Add(Vendor);
            // _context.SaveChanges();

            TempData["SuccessMessage"] = "Vendor created successfully! (Database not connected yet)";
            return RedirectToPage("./Index");
        }
    }
}
