using Afamia_UI.Models;
using Afamia_UI.Models.Queries;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Afamia_UI.Pages.Admins
{
    public class IndexModel : PageModel
    {

        public EmployeesServices empObj { get; set; }
        public ProductionRoomServices prodRoomObj { get; set; }
        public CustomerServices custObj { get; set; }
        public int TotalActiveEmployees { get; set; }
        public int TotalProductionRooms { get; set; }
        public int TotalCustomers { get; set; }

        public IndexModel(DB db)
        {
            empObj = new EmployeesServices(db);
            TotalActiveEmployees = empObj.GetNumOfActiveEmployees();
            prodRoomObj = new ProductionRoomServices(db);
            TotalProductionRooms = prodRoomObj.GetNumOfProductionRooms();
            custObj = new CustomerServices(db);
            TotalCustomers = custObj.GetTotalCustomers();
        }

        public void OnGet()
        {

        }
    }
}
