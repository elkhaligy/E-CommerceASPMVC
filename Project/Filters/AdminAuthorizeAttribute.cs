using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace YourNamespace.Filters
{
    public class AdminAuthorizeAttribute : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var role = context.HttpContext.Session.GetString("UserRole");

            if (string.IsNullOrEmpty(role))
            {
                // Not logged in
                context.Result = new RedirectToActionResult("SignIn", "Admin", null);
            }
            else if (!role.Equals("Admin", StringComparison.OrdinalIgnoreCase))
            {
                // Not an Admin
                context.Result = new RedirectToActionResult("SignIn", "Admin", null);
            }
        }
    }
}
