using Afamia_UI.Models;
using Afamia_UI.Models.Entities;
using Afamia_UI.Models.Queries;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;

namespace Afamia_UI.Pages.Admins.ProductionRooms_operations.ProductionLines.PipelineSections
{
    public class EditPipelineSectionModel : PageModel
    {
        [BindProperty]
        public PipelineSection pipelineSection { get; set; }

        [BindProperty]
        public int OldId { get; set; }

        [BindProperty]
        public int OldPipelineId { get; set; }

        public string ErrorMessage { get; set; }
        public PipelineSectionServices pipelineSectionObj { get; set; }

        public EditPipelineSectionModel(DB db)
        {
            pipelineSectionObj = new PipelineSectionServices(db);
        }

        public void OnGet(int id, int pipelineId)
        {
            Console.WriteLine("got Id ");
            Console.WriteLine(id);
            Console.WriteLine("got Pipeline Id ");
            Console.WriteLine(pipelineId);
            pipelineSection = pipelineSectionObj.GetPipelineSectionById(id, pipelineId);
            OldId = id;
            OldPipelineId = pipelineId;
        }

        public IActionResult OnPost()
        {
            Console.WriteLine("Posted Id ");
            Console.WriteLine(pipelineSection.ID);
            Console.WriteLine("Posted Pipeline Id ");
            Console.WriteLine(pipelineSection.Pipeline_ID);

            // Validate pipeline section data
            if (pipelineSection == null)
            {
                ErrorMessage = "Invalid request. Pipeline section data is missing.";
                return Page();
            }

            // ID required
            if (pipelineSection.ID == 0)
            {
                ErrorMessage = "Pipeline section ID is required.";
                return Page();
            }

            // Name required
            if (string.IsNullOrWhiteSpace(pipelineSection.Name))
            {
                ErrorMessage = "Please enter the pipeline section name.";
                return Page();
            }

            // Name length check
            if (pipelineSection.Name.Trim().Length < 3)
            {
                ErrorMessage = "Pipeline section name must be at least 3 characters long.";
                return Page();
            }

            // Pipeline ID required
            if (pipelineSection.Pipeline_ID == 0)
            {
                ErrorMessage = "Pipeline ID is required.";
                return Page();
            }

            try
            {
                pipelineSectionObj.EditPipelineSection(pipelineSection, OldId, OldPipelineId);
            }
            catch (SqlException ex)
            {
                // Handle common SQL errors
                if (ex.Number == 2627 || ex.Number == 2601) // Unique/PK violation
                {
                    ErrorMessage = "A pipeline section with the same ID and Pipeline ID already exists.";
                    return Page();
                }

                if (ex.Number == 547) // FK conflict
                {
                    ErrorMessage = "Cannot update: The specified Pipeline ID does not exist or this section is referenced by other records.";
                    return Page();
                }

                // Other DB errors
                ErrorMessage = "Database error: " + ex.Message;
                return Page();
            }

            return RedirectToPage("/Admins/ProductionRooms(operations)/ProductionLines/PipelineSections/PipelineSections", new { pipelineId = pipelineSection.Pipeline_ID });
        }
    }
}
