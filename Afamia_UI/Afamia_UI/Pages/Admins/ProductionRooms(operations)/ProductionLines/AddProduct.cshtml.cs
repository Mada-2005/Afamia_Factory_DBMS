using Afamia_UI.Models;
using Afamia_UI.Models.Entities;
using Afamia_UI.Models.Queries;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;

namespace Afamia_UI.Pages.Admins.ProductionRooms_operations_.ProductionLines
{
    public class AddProductModel : PageModel
    {
        [BindProperty]
        public Product product { get; set; }

        public string ErrorMessage { get; set; }
        public List<Products_Type> ProductTypes { get; set; } = new List<Products_Type>();
        public List<Customer> Customers { get; set; } = new List<Customer>();
        public int PipelineId { get; set; }

        private ProductServices prodObj { get; set; }
        private CustomerServices custObj { get; set; }

        public AddProductModel(DB db)
        {
            prodObj = new ProductServices(db);
            custObj = new CustomerServices(db);
        }

        public void OnGet(int pipelineId)
        {
            PipelineId = pipelineId;
            product = new Product();
            product.Production_Line = pipelineId;
            ProductTypes = prodObj.GetAllProductTypes();
            Customers = custObj.GetAllCustomers();
        }

        public IActionResult OnPost()
        {
            // Defensive: product object must be present (binding)
            if (product == null)
            {
                ErrorMessage = "Invalid request. Product data is missing.";
                ProductTypes = prodObj.GetAllProductTypes();
                Customers = custObj.GetAllCustomers();
                return Page();
            }

            // Id required
            if (product.Batch_Id == 0)
            {
                ErrorMessage = "Please fill the Product ID field.";
                ProductTypes = prodObj.GetAllProductTypes();
                Customers = custObj.GetAllCustomers();
                return Page();
            }

            
            

            // Production Date required
            if (product.ProductionDate == default(DateTime))
            {
                ErrorMessage = "Please select the production date.";
                ProductTypes = prodObj.GetAllProductTypes();
                Customers = custObj.GetAllCustomers();
                return Page();
            }

            // Expiration Date required
            if (product.ExpirationDate == default(DateTime))
            {
                ErrorMessage = "Please select the expiration date.";
                ProductTypes = prodObj.GetAllProductTypes();
                Customers = custObj.GetAllCustomers();
                return Page();
            }

            // Expiration date must be after production date
            if (product.ExpirationDate <= product.ProductionDate)
            {
                ErrorMessage = "Expiration date must be after production date.";
                ProductTypes = prodObj.GetAllProductTypes();
                Customers = custObj.GetAllCustomers();
                return Page();
            }

            // Type required
            if (product.type == 0)
            {
                ErrorMessage = "Please select a product type.";
                ProductTypes = prodObj.GetAllProductTypes();
                Customers = custObj.GetAllCustomers();
                return Page();
            }

            // Validate product type exists
            var selectedType = prodObj.GetProductTypeById(product.type);
            if (selectedType == null)
            {
                ErrorMessage = "Invalid product type selected.";
                ProductTypes = prodObj.GetAllProductTypes();
                Customers = custObj.GetAllCustomers();
                return Page();
            }

            // Production Line required
            if (product.Production_Line == 0)
            {
                ErrorMessage = "Production line is required.";
                ProductTypes = prodObj.GetAllProductTypes();
                Customers = custObj.GetAllCustomers();
                return Page();
            }

            // Customer ID is optional (can be 0 for no customer)

            // Start time validation (optional but if provided, must be valid)
            if (product.Start_time.HasValue)
            {
                if (product.Start_time.Value < TimeSpan.Zero || product.Start_time.Value >= TimeSpan.FromHours(24))
                {
                    ErrorMessage = "Start time must be between 00:00:00 and 23:59:59.";
                    ProductTypes = prodObj.GetAllProductTypes();
                    Customers = custObj.GetAllCustomers();
                    return Page();
                }
            }

            // End time validation (optional but if provided, must be valid)
            if (product.End_time.HasValue)
            {
                if (product.End_time.Value < TimeSpan.Zero || product.End_time.Value >= TimeSpan.FromHours(24))
                {
                    ErrorMessage = "End time must be between 00:00:00 and 23:59:59.";
                    ProductTypes = prodObj.GetAllProductTypes();
                    Customers = custObj.GetAllCustomers();
                    return Page();
                }
            }

            // If both start and end times are provided, end time must be after start time
            if (product.Start_time.HasValue && product.End_time.HasValue)
            {
                if (product.End_time.Value <= product.Start_time.Value)
                {
                    ErrorMessage = "End time must be after start time.";
                    ProductTypes = prodObj.GetAllProductTypes();
                    Customers = custObj.GetAllCustomers();
                    return Page();
                }
            }

            // Quantity validation for batch creation
            if (product.Quantity <= 0)
            {
                ErrorMessage = "Quantity must be at least 1.";
                ProductTypes = prodObj.GetAllProductTypes();
                Customers = custObj.GetAllCustomers();
                return Page();
            }

            // Batch ID required
            if (product.Batch_Id == 0)
            {
                ErrorMessage = "Please enter the batch number.";
                ProductTypes = prodObj.GetAllProductTypes();
                Customers = custObj.GetAllCustomers();
                return Page();
            }



            // Batch creation - loop through quantity and create products with incremental IDs
            try
            {
                int startId = product.Id;
                for (int i = 0; i < product.Quantity; i++)
                {
                    // Check if ID already exists
                    

                    Product newProduct = new Product
                    {
                        
                        Name = product.Name,
                        ProductionDate = product.ProductionDate,
                        ExpirationDate = product.ExpirationDate,
                        type = product.type,
                        Batch_Id = product.Batch_Id,
                        Production_Line = product.Production_Line,
                        Customer_Id = product.Customer_Id == 0 ? 0 : product.Customer_Id,
                        Start_time = product.Start_time,
                        End_time = product.End_time
                    };

                    prodObj.AddProduct(newProduct);
                }
            }
            catch (SqlException ex)
            {
                // Unique key violation (duplicate ID)
                if (ex.Number == 2627 || ex.Number == 2601)
                {
                    ErrorMessage = "A product with the same ID already exists.";
                    ProductTypes = prodObj.GetAllProductTypes();
                    Customers = custObj.GetAllCustomers();
                    return Page();
                }

                // Foreign key violation
                if (ex.Number == 547)
                {
                    ErrorMessage = "Related record not found. Make sure the production line and customer exist.";
                    ProductTypes = prodObj.GetAllProductTypes();
                    Customers = custObj.GetAllCustomers();
                    return Page();
                }

                // Other DB errors
                ErrorMessage = "Database error: " + ex.Message;
                ProductTypes = prodObj.GetAllProductTypes();
                Customers = custObj.GetAllCustomers();
                return Page();
            }

            return RedirectToPage("/Admins/ProductionRooms(operations)/ProductionLines/PipelineSections/PipelineSections", new { pipelineId = product.Production_Line });
        }
    }
}
