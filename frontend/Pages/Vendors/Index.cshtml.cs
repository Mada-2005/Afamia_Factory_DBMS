using Microsoft.AspNetCore.Mvc.RazorPages;
using FactorySystemWebpage.Models;

namespace FactorySystemWebpage.Pages.Vendors
{
    public class IndexModel : PageModel
    {
        // TODO: Inject FactoryDbContext when database is ready
        // private readonly FactoryDbContext _context;

        // public IndexModel(FactoryDbContext context)
        // {
        //     _context = context;
        // }

        public IList<Vendor> Vendors { get; set; } = new List<Vendor>();

        public void OnGet()
        {
            // TODO: Load vendors from database
            // Vendors = _context.Vendors.ToList();
        }
    }
}
