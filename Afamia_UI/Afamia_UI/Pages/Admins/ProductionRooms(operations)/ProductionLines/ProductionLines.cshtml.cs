using Afamia_UI.Models;
using Afamia_UI.Models.Entities;
using Afamia_UI.Models.Queries;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Afamia_UI.Pages.Admins.ProductionRooms_operations.ProductionLines
{
    public class ProductionLinesModel : PageModel
    {
        public List<ProductionLine> productionLines { get; set; }
        public int RoomId { get; set; }

        ProductionLineServices prodLineObj { get; set; }

        public ProductionLinesModel(DB db)
        {
            prodLineObj = new ProductionLineServices(db);
        }

        public void OnGet(int roomId)
        {
            RoomId = roomId;
            productionLines = prodLineObj.GetProductionLinesByRoomId(roomId);
        }

        public IActionResult OnPostDelete(int id)
        {
            prodLineObj.Delete(id);
            return RedirectToPage();
        }
    }
}
