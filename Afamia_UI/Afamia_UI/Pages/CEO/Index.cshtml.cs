using Afamia_UI.Models;
using Afamia_UI.Models.Queries;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Afamia_UI.Pages.CEO
{
    public class IndexModel : PageModel
    {
        public CEODashboardStats Statistics { get; set; }
        public int TotalAdmins { get; set; }

        private CEOStatisticsService statsService { get; set; }
        private AdminAccountService adminService { get; set; }

        public IndexModel(DB db)
        {
            statsService = new CEOStatisticsService(db);
            adminService = new AdminAccountService(db);
        }

        public void OnGet()
        {
            Statistics = statsService.GetDashboardStatistics();
            TotalAdmins = adminService.GetTotalAdmins();
        }
    }
}
