using Afamia_UI.Models;
using Afamia_UI.Models.Entities;
using Afamia_UI.Models.Queries;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Afamia_UI.Pages.Admins.TraceProduct
{
    public class TraceProductModel : PageModel
    {
        [BindProperty]
        public int BatchId { get; set; }

        public BatchInfo ProductInfo { get; set; }
        public List<ProductTraceResult> WorkerResults { get; set; } = new List<ProductTraceResult>();
        public string ErrorMessage { get; set; }
        public bool SearchPerformed { get; set; } = false;

        private TraceProductQueries traceQueries { get; set; }
        private ProductServices prodObj { get; set; }

        public TraceProductModel(DB db)
        {
            traceQueries = new TraceProductQueries(db);
            prodObj = new ProductServices(db);
        }

        public void OnGet()
        {
            // Initial page load - no search performed yet
        }

        public IActionResult OnPostSearch()
        {
            SearchPerformed = true;

            // Validate batch ID
            if (BatchId == 0)
            {
                ErrorMessage = "Please enter a valid Batch ID.";
                return Page();
            }

            // Get batch information (get one product from the batch since all have same info)
            ProductInfo = traceQueries.GetBatchInfo(BatchId);

            if (ProductInfo == null)
            {
                ErrorMessage = $"Batch #{BatchId} not found.";
                return Page();
            }

            // Get workers who worked on this batch
            WorkerResults = traceQueries.GetProductionWorkersForBatch(BatchId);

            // No error if no workers found - just display empty or message
            return Page();
        }
    }
}
