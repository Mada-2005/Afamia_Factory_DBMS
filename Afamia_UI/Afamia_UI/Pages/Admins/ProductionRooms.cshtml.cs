using Afamia_UI.Models;
using Afamia_UI.Models.Entities;
using Afamia_UI.Models.Queries;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Afamia_UI.Pages.Admins
{
    public class ProductionRoomsModel : PageModel
    {

        public List<ProductionRoom> productionRooms { get; set; } = new List<ProductionRoom>();

        public ProductionRoomServices Pobj { get; set; }
        public ProductionRoomsModel(DB db) { 
            Pobj = new ProductionRoomServices(db);
        }

        public void OnGet()
        {
            productionRooms = Pobj.GetAllProductionRooms();
        }
    }
}
