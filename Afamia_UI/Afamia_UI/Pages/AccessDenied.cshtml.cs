// Pages/AccessDenied.cshtml.cs
using Azure;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net;

namespace Afamia_UI.Pages
{
    // ❌ DO NOT PUT [Authorize] HERE!
    // This page must be accessible to everyone
    public class AccessDeniedModel : PageModel
    {
       
        public void OnGet()
        {
            Console.WriteLine("AccessDenied page accessed");
            
        }
    }
}
