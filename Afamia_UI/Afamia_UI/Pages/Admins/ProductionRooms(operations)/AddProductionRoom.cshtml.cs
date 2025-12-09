using Afamia_UI.Models;
using Afamia_UI.Models.Entities;
using Afamia_UI.Models.Queries;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;

namespace Afamia_UI.Pages.Admins
{
    public class AddProductionRoomModel : PageModel
    {
        [BindProperty]
        public ProductionRoom productionRoom { get; set; } = new ProductionRoom();
        public string ErrorMessage { get; set; }
        public ProductionRoomServices Pobj { get; set; }

        public AddProductionRoomModel(DB db)
        {
            Pobj = new ProductionRoomServices(db);
        }
        public void OnGet()
        {

        }

        public IActionResult OnPost()
        {
            // Validate production room data
            if (productionRoom == null)
            {
                ErrorMessage = "Invalid request. Production room data is missing.";
                return Page();
            }

            // ID required
            if (productionRoom.Id == 0)
            {
                ErrorMessage = "Please fill the ID field.";
                return Page();
            }

            // Check if production room with same ID already exists
            if (Pobj.GetProductionRoomById(productionRoom.Id) != null)
            {
                ErrorMessage = $"Production room with ID {productionRoom.Id} already exists.";
                return Page();
            }

            // Place required
            if (string.IsNullOrWhiteSpace(productionRoom.Place))
            {
                ErrorMessage = "Please enter the place/location.";
                return Page();
            }

            // Place length check (adjust based on your DB schema)
            if (productionRoom.Place.Trim().Length < 3)
            {
                ErrorMessage = "Place must be at least 3 characters long.";
                return Page();
            }

            try
            {
                Pobj.AddProductionRoom(productionRoom);
            }
            catch (SqlException ex)
            {
                // Handle common SQL errors
                if (ex.Number == 2627 || ex.Number == 2601) // Unique/PK violation
                {
                    ErrorMessage = "A production room with the same ID already exists.";
                    return Page();
                }

                if (ex.Number == 547) // FK conflict
                {
                    ErrorMessage = "Related record not found. Make sure referenced data exists.";
                    return Page();
                }

                // Other DB errors
                ErrorMessage = "Database error: " + ex.Message;
                return Page();
            }

            return RedirectToPage("/ProductionRooms(operations)/ProductionRooms");
        }
    }
}
