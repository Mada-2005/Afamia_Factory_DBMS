using Afamia_UI.Models;
using Afamia_UI.Models.Entities;
using Afamia_UI.Models.Queries;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;

namespace Afamia_UI.Pages.Admins.ProductionRooms_operations.ProductionLines
{
    public class EditProductionLineModel : PageModel
    {
        [BindProperty]
        public ProductionLine productionLine { get; set; }

        public string ErrorMessage { get; set; }
        public ProductionLineServices prodLineObj { get; set; }

        public EditProductionLineModel(DB db)
        {
            prodLineObj = new ProductionLineServices(db);
        }

        public void OnGet(int id)
        {
            Console.WriteLine("got Id ");
            Console.WriteLine(id);
            productionLine = prodLineObj.GetProductionLineById(id);
        }

        public IActionResult OnPost()
        {
            Console.WriteLine("Posted Id ");
            Console.WriteLine(productionLine.Id);

            // Validate production line data
            if (productionLine == null)
            {
                ErrorMessage = "Invalid request. Production line data is missing.";
                return Page();
            }

            // ID required
            if (productionLine.Id == 0)
            {
                ErrorMessage = "Production line ID is required.";
                return Page();
            }

            // Name required
            if (string.IsNullOrWhiteSpace(productionLine.Name))
            {
                ErrorMessage = "Please enter the production line name.";
                return Page();
            }

            // Name length check
            if (productionLine.Name.Trim().Length < 3)
            {
                ErrorMessage = "Production line name must be at least 3 characters long.";
                return Page();
            }

            // Room ID required
            if (productionLine.Room_ID == 0)
            {
                ErrorMessage = "Room ID is required.";
                return Page();
            }

            try
            {
                prodLineObj.EditProductionLine(productionLine);
            }
            catch (SqlException ex)
            {
                // Handle common SQL errors
                if (ex.Number == 2627 || ex.Number == 2601) // Unique/PK violation
                {
                    ErrorMessage = "A production line with the same ID already exists.";
                    return Page();
                }

                if (ex.Number == 547) // FK conflict
                {
                    ErrorMessage = "Cannot update: The specified Room ID does not exist or this production line is referenced by other records.";
                    return Page();
                }

                // Other DB errors
                ErrorMessage = "Database error: " + ex.Message;
                return Page();
            }

            return RedirectToPage("/Admins/ProductionRooms(operations)/ProductionLines/ProductionLines", new { roomId = productionLine.Room_ID });
        }
    }
}
