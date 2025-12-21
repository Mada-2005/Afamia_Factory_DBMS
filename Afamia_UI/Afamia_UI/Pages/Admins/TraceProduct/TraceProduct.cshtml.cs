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
        public int ProductId { get; set; }

        public Product ProductInfo { get; set; }
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

            // Validate product ID
            if (ProductId == 0)
            {
                ErrorMessage = "Please enter a valid Product ID.";
                return Page();
            }

            // Get product information
            ProductInfo = prodObj.GetProductById(ProductId);

            if (ProductInfo == null)
            {
                ErrorMessage = $"Product with ID {ProductId} not found.";
                return Page();
            }

            // Check if product has both start and end times
            if (!ProductInfo.Start_time.HasValue || !ProductInfo.End_time.HasValue)
            {
                ErrorMessage = $"Product {ProductId} does not have start time and/or end time recorded. Cannot trace workers.";
                return Page();
            }

            // Get workers who worked on this product
            WorkerResults = traceQueries.GetProductionWorkersForProduct(ProductId);

            if (WorkerResults.Count == 0)
            {
                ErrorMessage = $"No workers found for product {ProductId}. This could mean no work schedules match the production timeline.";
                return Page();
            }

            return Page();
        }
    }
}
