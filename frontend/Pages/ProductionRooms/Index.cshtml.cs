using Microsoft.AspNetCore.Mvc.RazorPages;
using FactorySystemWebpage.Models;

namespace FactorySystemWebpage.Pages.ProductionRooms
{
    public class IndexModel : PageModel
    {
        public IList<ProductionRoom> ProductionRooms { get; set; } = new List<ProductionRoom>();

        public void OnGet()
        {
            // TODO: Load production rooms from database
        }
    }
}
