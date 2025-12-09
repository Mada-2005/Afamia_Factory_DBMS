using Afamia_UI.Models;
using Afamia_UI.Models.Entities;
using Afamia_UI.Models.Queries;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Afamia_UI.Pages.Admins.ProductionRooms_operations.ProductionLines.PipelineSections
{
    public class PipelineSectionsModel : PageModel
    {
        public List<PipelineSection> pipelineSections { get; set; }
        public int PipelineId { get; set; }

        PipelineSectionServices pipelineSectionObj { get; set; }

        public PipelineSectionsModel(DB db)
        {
            pipelineSectionObj = new PipelineSectionServices(db);
        }

        public void OnGet(int pipelineId)
        {
            PipelineId = pipelineId;
            pipelineSections = pipelineSectionObj.GetPipelineSectionsByPipelineId(pipelineId);
        }

        public IActionResult OnPostDelete(int id, int pipelineId)
        {
            pipelineSectionObj.Delete(id, pipelineId);
            return RedirectToPage(new { pipelineId = pipelineId });
        }
    }
}
