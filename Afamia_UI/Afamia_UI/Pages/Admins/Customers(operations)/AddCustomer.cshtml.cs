using Afamia_UI.Models;
using Afamia_UI.Models.Entities;
using Afamia_UI.Models.Queries;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;

namespace Afamia_UI.Pages.Admins.Customers_operations_
{
    public class AddCustomerModel : PageModel
    {
        [BindProperty]
        public Customer customer { get; set; }

        public string ErrorMessage { set; get; }
        CustomerServices custObj { get; set; }

        public AddCustomerModel(DB db)
        {
            custObj = new CustomerServices(db);
        }

        public void OnGet()
        {
            customer = new Customer();
        }

        public IActionResult OnPost()
        {
            // Defensive: customer object must be present (binding)
            if (customer == null)
            {
                ErrorMessage = "Invalid request. Customer data is missing.";
                return Page();
            }

            // Id required
            if (customer.Id == 0)
            {
                ErrorMessage = "Please fill the Customer ID field.";
                return Page();
            }

            // Prevent creating a new customer with an ID that already exists
            if (custObj.GetCustomerById(customer.Id) != null)
            {
                ErrorMessage = $"Customer with ID {customer.Id} already exists.";
                return Page();
            }

            // First name required
            if (string.IsNullOrWhiteSpace(customer.FName))
            {
                ErrorMessage = "Please enter the first name.";
                return Page();
            }

            // Last name required
            if (string.IsNullOrWhiteSpace(customer.LName))
            {
                ErrorMessage = "Please enter the last name.";
                return Page();
            }

            // Name length checks (varchar(20))
            if (customer.FName.Trim().Length < 3)
            {
                ErrorMessage = "First name must be at least 3 characters long.";
                return Page();
            }

            if (customer.FName.Trim().Length > 20)
            {
                ErrorMessage = "First name must be at most 20 characters long.";
                return Page();
            }

            if (customer.LName.Trim().Length < 3)
            {
                ErrorMessage = "Last name must be at least 3 characters long.";
                return Page();
            }

            if (customer.LName.Trim().Length > 20)
            {
                ErrorMessage = "Last name must be at most 20 characters long.";
                return Page();
            }

            // Optional: disallow digits in names
            if (customer.FName.Any(ch => char.IsDigit(ch)))
            {
                ErrorMessage = "First name cannot contain digits.";
                return Page();
            }

            if (customer.LName.Any(ch => char.IsDigit(ch)))
            {
                ErrorMessage = "Last name cannot contain digits.";
                return Page();
            }

            // Validate each submitted phone number
            if (customer.Phone != null)
            {
                // Check duplicates inside submitted list
                var duplicates = customer.Phone
                                .Where(p => !string.IsNullOrWhiteSpace(p))
                                .GroupBy(p => p.Trim())
                                .Where(g => g.Count() > 1)
                                .Select(g => g.Key)
                                .ToList();
                if (duplicates.Any())
                {
                    ErrorMessage = "Duplicate phone numbers found in the submission: " + string.Join(", ", duplicates);
                    return Page();
                }

                // Validate format and uniqueness
                for (int i = 0; i < customer.Phone.Count; i++)
                {
                    var phone = (customer.Phone[i] ?? string.Empty).Trim();

                    if (string.IsNullOrWhiteSpace(phone))
                    {
                        ErrorMessage = $"Phone number #{i + 1} is empty.";
                        return Page();
                    }

                    // Length check (varchar(11))
                    if (phone.Length != 11)
                    {
                        ErrorMessage = $"Phone number #{i + 1} must be exactly 11 digits.";
                        return Page();
                    }

                    // Digits-only check
                    if (!phone.All(char.IsDigit))
                    {
                        ErrorMessage = $"Phone number #{i + 1} must contain digits only.";
                        return Page();
                    }

                    // Database uniqueness check
                    if (custObj.IsPhoneTaken(phone))
                    {
                        ErrorMessage = $"Phone number '{phone}' is already used by another customer.";
                        return Page();
                    }
                }
            }

            // Prevent single quotes in names
            if (customer.FName.Contains("'") || customer.LName.Contains("'"))
            {
                ErrorMessage = "Names must not contain single quotes.";
                return Page();
            }

            // Business rule: limit maximum phones per customer
            const int MaxPhones = 5;
            if (customer.Phone != null && customer.Phone.Count > MaxPhones)
            {
                ErrorMessage = $"You can add up to {MaxPhones} phone numbers only.";
                return Page();
            }

            // Catch-all server-side DB error handling
            try
            {
                custObj.AddCustomer(customer);
            }
            catch (SqlException ex)
            {
                // Unique key violation
                if (ex.Number == 2627 || ex.Number == 2601)
                {
                    ErrorMessage = "A record with the same key already exists (duplicate ID or phone number).";
                    return Page();
                }

                // Foreign key violation
                if (ex.Number == 547)
                {
                    ErrorMessage = "Related record not found. Make sure referenced data exists.";
                    return Page();
                }

                // Other DB errors
                ErrorMessage = "Database error: " + ex.Message;
                return Page();
            }

            return RedirectToPage("/Admins/Customers(operations)/Customers");
        }

        public IActionResult OnPostAddPhone()
        {
            customer.Phone.Add("");
            return Page();
        }

        public IActionResult OnPostRemovePhone(int index)
        {
            customer.Phone.RemoveAt(index);
            return Page();
        }
    }
}
