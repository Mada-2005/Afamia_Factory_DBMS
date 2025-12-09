using Afamia_UI.Models;
using Afamia_UI.Models.Entities;
using Afamia_UI.Models.Queries;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;

namespace Afamia_UI.Pages.Admins
{
    public class EditModel : PageModel
    {
        [BindProperty]
        public Employee employee { get; set; }
        [BindProperty]
        public int idx { get; set; }

        public string ErrorMessage { set; get; }
        public EmployeesServices empObj { get; set; }
        
        public EditModel(DB db)
        {
            empObj = new EmployeesServices(db);
        }
        public void OnGet(int id)
        {
            Console.WriteLine("got Id ");
            Console.WriteLine(id);
            employee = empObj.GetEmployeeById(id);

        }
        public IActionResult OnPost()
        {
            Console.WriteLine("Posted Id ");
            Console.WriteLine(employee.Id);
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

            try
            {
                empObj.EditEmployee(employee);
            }
            catch (SqlException ex)
            {
                // Handle common SQL errors
                if (ex.Number == 2627 || ex.Number == 2601) // Unique/PK violation
                {
                    ErrorMessage = "A record with the same key already exists (duplicate SSN or phone number).";
                    return Page();
                }

                if (ex.Number == 547) // FK conflict
                {
                    ErrorMessage = "Related record not found. Make sure referenced data exists.";
                    return Page();
                }

                // Other DB errors
                ErrorMessage = "Database error: " + ex.Message;
                return Page();
            }

            return RedirectToPage("/Admins/Employees(operations)/Employees");
        }

        public IActionResult OnPostRemovePhone(int index)
        {
            employee.Phone.RemoveAt(index);
            empObj.EditEmployee(employee);
            return Page();
        }

        public IActionResult OnPostAddPhone()
        {
            employee.Phone.Add("");
            return Page();
        }
    }
}
