using Afamia_UI.Models;
using Afamia_UI.Models.Entities;
using Afamia_UI.Models.Queries;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;

namespace Afamia_UI.Pages.Admins.ProductionRooms_operations
{
    public class EditProductionRoomModel : PageModel
    {
        [BindProperty]
        public ProductionRoom productionRoom { get; set; }

        public string ErrorMessage { get; set; }
        public ProductionRoomServices prodRoomObj { get; set; }

        public EditProductionRoomModel(DB db)
        {
            prodRoomObj = new ProductionRoomServices(db);
        }
        public void OnGet(int id)
        {
            Console.WriteLine("got Id ");
            Console.WriteLine(id);
            productionRoom = prodRoomObj.GetProductionRoomById(id);

        }
        public IActionResult OnPost()
        {
            Console.WriteLine("Posted Id ");
            Console.WriteLine(productionRoom.Id);

            // Validate production room data
            if (productionRoom == null)
            {
                ErrorMessage = "Invalid request. Production room data is missing.";
                return Page();
            }

            // ID required
            if (productionRoom.Id == 0)
            {
                ErrorMessage = "Production room ID is required.";
                return Page();
            }

            // Place required
            if (string.IsNullOrWhiteSpace(productionRoom.Place))
            {
                ErrorMessage = "Please enter the place/location.";
                return Page();
            }

            // Place length check
            if (productionRoom.Place.Trim().Length < 3)
            {
                ErrorMessage = "Place must be at least 3 characters long.";
                return Page();
            }

            try
            {
                prodRoomObj.EditProductionRoom(productionRoom);
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
                    ErrorMessage = "Cannot update: This production room is referenced by other records.";
                    return Page();
                }

                // Other DB errors
                ErrorMessage = "Database error: " + ex.Message;
                return Page();
            }

            return RedirectToPage("/Admins/ProductionRooms(operations)/ProductionRooms");
        }
    }
}
