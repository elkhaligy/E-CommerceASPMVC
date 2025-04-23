using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Claims;

namespace YourNamespace.Filters
{
    public class AdminAuthorizeAttribute : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var user = context.HttpContext.User;

            // Not logged in
            if (user == null || user.Identity == null || !user.Identity.IsAuthenticated)
            {
                context.Result = new RedirectToActionResult("SignIn", "Admin", null);
                return;
            }


            // Check if user has Admin role
            var roleClaim = user.FindFirst(ClaimTypes.Role);
            if (roleClaim == null || !roleClaim.Value.Equals("Admin", StringComparison.OrdinalIgnoreCase))
            {
                context.Result = new RedirectToActionResult("SignIn", "Admin", null);
            }
        }
    }
}
