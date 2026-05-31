using System.Text.Json;
using ManagementKimThoa.Constants;
using ManagementKimThoa.DTOs.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ManagementKimThoa.Attributes
{
    public class AuthorizeAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var allowAnonymous = context.ActionDescriptor.EndpointMetadata.OfType<AllowAnonymousAttribute>().Any();

            if (allowAnonymous)
            {
                base.OnActionExecuting(context);
                return;
            }

            var json = context.HttpContext.Session.GetString(SessionConstant.CurrentUser);

            if (string.IsNullOrEmpty(json))
            {
                context.Result = new RedirectResult(RouteConstant.Login);
                return;
            }

            var currentUser = JsonSerializer.Deserialize<UserSession>(json);

            if (currentUser == null)
            {
                context.Result = new RedirectResult(RouteConstant.Login);
                return;
            }

            base.OnActionExecuting(context);
        }
    }
}

