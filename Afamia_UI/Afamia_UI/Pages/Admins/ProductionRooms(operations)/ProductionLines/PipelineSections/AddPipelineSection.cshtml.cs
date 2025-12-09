using Afamia_UI.Models;
using Afamia_UI.Models.Entities;
using Afamia_UI.Models.Queries;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;

namespace Afamia_UI.Pages.Admins.ProductionRooms_operations.ProductionLines.PipelineSections
{
    public class AddPipelineSectionModel : PageModel
    {
        [BindProperty]
        public PipelineSection pipelineSection { get; set; }

        public string ErrorMessage { get; set; }
        PipelineSectionServices pipelineSectionObj { get; set; }

        public AddPipelineSectionModel(DB db)
        {
            pipelineSectionObj = new PipelineSectionServices(db);
        }

        public void OnGet(int pipelineId)
        {
            pipelineSection = new PipelineSection();
            pipelineSection.Pipeline_ID = pipelineId;
        }

        public IActionResult OnPost()
        {
            // Validate pipeline section data
            if (pipelineSection == null)
            {
                ErrorMessage = "Invalid request. Pipeline section data is missing.";
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
                pipelineSectionObj.AddPipelineSection(pipelineSection);
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
                    ErrorMessage = "The specified Pipeline ID does not exist. Please check the Pipeline ID.";
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
