using Afamia_UI.Models;
using Afamia_UI.Models.Entities;
using Afamia_UI.Models.Queries;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;

namespace Afamia_UI.Pages.Admins
{
    public class AddEmployeeModel : PageModel
    {
        [BindProperty]
        public Employee employee { get; set; }

        public string ErrorMessage { set; get; }
        EmployeesServices empObj { get; set; }

   

        public AddEmployeeModel(DB db) { 
            
            empObj = new EmployeesServices(db);
        }
        public void OnGet()
        {
            employee = new Employee();
        }
        public IActionResult OnPost()
        {
            if (employee.Id == 0)
            {
                ErrorMessage = "Plz Fill The SSn Field";
                return Page();
            }
            // Defensive: employee object must be present (binding)
            if (employee == null)
            {
                ErrorMessage = "Invalid request. Employee data is missing.";
                return Page();
            }

            // Id (SSN) required (your example)
            if (employee.Id == 0)
            {
                ErrorMessage = "Please fill the SSN field.";
                return Page();
            }

            // Prevent creating a new employee with an ID that already exists
            if (empObj.GetEmployeeById(employee.Id)!= null)
            {
                ErrorMessage = $"Employee with SSN {employee.Id} already exists.";
                return Page();
            }


            // First name required
            if (string.IsNullOrWhiteSpace(employee.FName))
            {
                ErrorMessage = "Please enter the first name.";
                return Page();
            }

            // Last name required
            if (string.IsNullOrWhiteSpace(employee.LName))
            {
                ErrorMessage = "Please enter the last name.";
                return Page();
            }

            // Name length checks (CK_Employee_Name: length >= 3)
            if (employee.FName.Trim().Length < 3)
            {
                ErrorMessage = "First name must be at least 3 characters long.";
                return Page();
            }
            if (employee.LName.Trim().Length < 3)
            {
                ErrorMessage = "Last name must be at least 3 characters long.";
                return Page();
            }

            // Optional: disallow digits/special chars in names (app-level)
            if (employee.FName.Any(ch => char.IsDigit(ch)))
            {
                ErrorMessage = "First name cannot contain digits.";
                return Page();
            }
            if (employee.LName.Any(ch => char.IsDigit(ch)))
            {
                ErrorMessage = "Last name cannot contain digits.";
                return Page();
            }

            // Role required
            if (string.IsNullOrWhiteSpace(employee.role))
            {
                ErrorMessage = "Please select a role for the employee.";
                return Page();
            }

            // Role length limit (varchar(10))
            if (employee.role.Length > 10)
            {
                ErrorMessage = "Role value is too long (max 10 characters).";
                return Page();
            }

            // If Role is an enum in your app, ensure the value is valid
            
            // Works_in_Factory - optional check (should be boolean). If it's nullable:
            if (employee.Works_in_Factory == null)
            {
                ErrorMessage = "Please specify whether the employee works in factory.";
                return Page();
            }

            // Phone list existence (if you require at least one phone)
            if (employee.Phone == null || employee.Phone.Count == 0)
            {
                // OPTIONAL: only if your business requires a phone
                // ErrorMessage = "Please add at least one phone number.";
                // return Page();
            }

            // Validate each submitted phone number
            if (employee.Phone != null)
            {
                // 1) check duplicates inside submitted list
                var duplicates = employee.Phone
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

                // 2) validate format and uniqueness
                for (int i = 0; i < employee.Phone.Count; i++)
                {
                    var phone = (employee.Phone[i] ?? string.Empty).Trim();

                    if (string.IsNullOrWhiteSpace(phone))
                    {
                        ErrorMessage = $"Phone number #{i + 1} is empty.";
                        return Page();
                    }

                    // length check (CK_Employee_Phone: LEN(Phone) = 11)
                    if (phone.Length != 11)
                    {
                        ErrorMessage = $"Phone number #{i + 1} must be exactly 11 digits.";
                        return Page();
                    }

                    // digits-only check
                    if (!phone.All(char.IsDigit))
                    {
                        ErrorMessage = $"Phone number #{i + 1} must contain digits only.";
                        return Page();
                    }

                    // database uniqueness check (UQ_Employee_Phone)
                    // empObj.IsPhoneTaken(phone, employee.Id) should return true if the phone exists and belongs to another employee
                    if (empObj.IsPhoneTaken(phone))
                    {
                        ErrorMessage = $"Phone number '{phone}' is already used by another employee.";
                        return Page();
                    }
                }
            }

            // Prevent SQL injection style or invalid characters in text fields (optional simple check)
            if (employee.FName.Contains("'") || employee.LName.Contains("'"))
            {
                // Prefer parameterized queries in DB - but warn user if they typed unsupported characters
                ErrorMessage = "Names must not contain single quotes.";
                return Page();
            }

            // Business rule: limit maximum phones per employee (optional)
            const int MaxPhones = 5;
            if (employee.Phone != null && employee.Phone.Count > MaxPhones)
            {
                ErrorMessage = $"You can add up to {MaxPhones} phone numbers only.";
                return Page();
            }

            

            // Catch-all server-side DB error handling example (after attempting Save)
            try
            {
                empObj.AddEmployee(employee);
            }
            catch (SqlException ex)
            {
                // handle common SQL errors and map to user-friendly messages

                // Unique key violation (duplicate phone or duplicate PK)
                if (ex.Number == 2627 || ex.Number == 2601) // SQL Server unique/PK violation
                {
                    ErrorMessage = "A record with the same key already exists (duplicate SSN or phone number).";
                    return Page();
                }

                // Foreign key violation
                if (ex.Number == 547) // FK conflict
                {
                    ErrorMessage = "Related record not found. Make sure referenced data exists.";
                    return Page();
                }

                // Other DB errors - generic message
                ErrorMessage = "Database error: " + ex.Message; // log details and show brief message
                return Page();
            }

            

            
            return RedirectToPage("/Admins/Employees(operations)/Employees");
        }
        public IActionResult OnPostAddPhone()
        {
            employee.Phone.Add("");
            return Page();
        }
        public IActionResult OnPostRemovePhone(int index)
        {
            employee.Phone.RemoveAt(index);
            return Page();
        }
    }

}