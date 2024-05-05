using BaggageTrackerApi.Entities;
using BaggageTrackerApi.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace BaggageTrackerApi.Attributes;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class AuthorizeAttribute(bool personnelOnly = false) : Attribute, IAuthorizationFilter
{
    public void OnAuthorization(AuthorizationFilterContext context)
    {
        if (SkipAuthorization(context))
        {
            return;
        }
        
        var user = (User?)context.HttpContext.Items["User"];

        if (user == null)
        {
            context.Result = new JsonResult("Authentication needed to perform this action.");
            context.HttpContext.Response.StatusCode = StatusCodes.Status401Unauthorized;
        } else if (personnelOnly && user.Role != UserRole.Personnel)
        {
            context.Result = new JsonResult($"Only {nameof(UserRole.Personnel)} is authorized to perform this action.");
            context.HttpContext.Response.StatusCode = StatusCodes.Status403Forbidden;
        }
    }

    private static bool SkipAuthorization(AuthorizationFilterContext context) => 
        context.ActionDescriptor.EndpointMetadata.Any(e => e.GetType() == typeof(AllowAnonymousAttribute));
}