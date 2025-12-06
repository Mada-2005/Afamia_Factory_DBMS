using Afamia_UI.Models;
using Afamia_UI.Models.Entities;
using Afamia_UI.Models.Queries;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Afamia_UI.Pages.Admins
{
    public class AddProductionRoomModel : PageModel
    {
        [BindProperty]
        public ProductionRoom productionRoom { get; set; } = new ProductionRoom();
        public ProductionRoomServices Pobj { get; set; }

        public AddProductionRoomModel(DB db)
        {
            Pobj = new ProductionRoomServices(db);
        }
        public void OnGet()
        {

        }
    }
}
