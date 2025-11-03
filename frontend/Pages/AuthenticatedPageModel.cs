using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Filters;
using FactorySystemWebpage.Helpers;

namespace FactorySystemWebpage.Pages
{
    public class AuthenticatedPageModel : PageModel
    {
        public bool IsLoggedIn => HttpContext.Session.IsLoggedIn();
        public bool IsAdmin => HttpContext.Session.IsAdmin();
        public bool IsEmployee => HttpContext.Session.IsEmployee();
        public string? CurrentUsername => HttpContext.Session.GetUsername();
        public string? CurrentFullName => HttpContext.Session.GetFullName();
        public int? CurrentUserId => HttpContext.Session.GetUserId();
        public int? CurrentEmployeeId => HttpContext.Session.GetEmployeeId();
        public bool IsArabic => HttpContext.Session.GetString("Language") == "ar";

        public override void OnPageHandlerExecuting(PageHandlerExecutingContext context)
        {
            if (!IsLoggedIn)
            {
                context.Result = new RedirectToPageResult("/Login");
            }
            base.OnPageHandlerExecuting(context);
        }

        public string Translate(string english, string arabic)
        {
            return IsArabic ? arabic : english;
        }
    }

    public class AdminPageModel : AuthenticatedPageModel
    {
        public override void OnPageHandlerExecuting(PageHandlerExecutingContext context)
        {
            base.OnPageHandlerExecuting(context);

            if (IsLoggedIn && !IsAdmin)
            {
                context.Result = new RedirectToPageResult("/Dashboard/Employee/Index");
            }
        }
    }

    public class EmployeePageModel : AuthenticatedPageModel
    {
        public override void OnPageHandlerExecuting(PageHandlerExecutingContext context)
        {
            base.OnPageHandlerExecuting(context);

            if (IsLoggedIn && !IsEmployee)
            {
                context.Result = new RedirectToPageResult("/Dashboard/Admin/Index");
            }
        }
    }
}
