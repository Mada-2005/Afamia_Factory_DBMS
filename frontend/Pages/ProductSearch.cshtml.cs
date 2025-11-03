using Microsoft.AspNetCore.Mvc;

namespace FactorySystemWebpage.Pages
{
    public class ProductSearchModel : AuthenticatedPageModel
    {
        // TODO: Inject FactoryDbContext when database is ready
        // private readonly FactoryDbContext _context;

        [BindProperty(SupportsGet = true)]
        public string ProductCode { get; set; } = string.Empty;

        public bool ProductFound { get; set; } = false;

        // TODO: Add properties for product details when database is connected
        // public Product? Product { get; set; }
        // public List<ProductMaterial>? RawMaterials { get; set; }
        // public List<Employee>? Employees { get; set; }
        // public Customer? Customer { get; set; }

        public void OnGet()
        {
            // Page load - search form displayed
        }

        public void OnPost(string productCode)
        {
            ProductCode = productCode;

            if (!string.IsNullOrEmpty(ProductCode))
            {
                // TODO: Search database for product
                // Simulate search for demo
                if (ProductCode.StartsWith("CR") || ProductCode.StartsWith("BS") || ProductCode.StartsWith("SN") || ProductCode.StartsWith("PN"))
                {
                    ProductFound = true;
                }

                /* TODO: Uncomment when database is ready
                Product = _context.Products
                    .Include(p => p.ProductionRoom)
                    .Include(p => p.ProductMaterials)
                        .ThenInclude(pm => pm.RawMaterial)
                            .ThenInclude(rm => rm.Vendor)
                    .Include(p => p.CustomerProducts)
                        .ThenInclude(cp => cp.Customer)
                    .FirstOrDefault(p => p.ProductCode == ProductCode);

                if (Product != null)
                {
                    ProductFound = true;

                    // Get materials used
                    RawMaterials = Product.ProductMaterials.ToList();

                    // Get employees who worked on this product
                    if (Product.ProductionRoomId.HasValue)
                    {
                        var schedules = _context.DailyWorkSchedules
                            .Include(s => s.Employee)
                            .Where(s => s.ProductionRoomId == Product.ProductionRoomId &&
                                       s.WorkDate.Date == Product.ProductionDate.Date)
                            .ToList();

                        Employees = schedules.Select(s => s.Employee).Distinct().ToList();
                    }

                    // Get customer if distributed
                    var customerProduct = Product.CustomerProducts.FirstOrDefault();
                    if (customerProduct != null)
                    {
                        Customer = customerProduct.Customer;
                    }
                }
                */
            }
        }
    }
}
