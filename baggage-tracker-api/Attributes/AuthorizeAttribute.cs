using BaggageTrackerApi.Entities;
using BaggageTrackerApi.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace BaggageTrackerApi.Attributes;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class AuthorizeAttribute(bool personnelOnly = false) : Attribute, IAuthorizationFilter
{
    private readonly JsonResult _unauthenticatedResult =
        new(new { Message = "Authentication needed to perform this action.", StatusCode = StatusCodes.Status401Unauthorized });
    
    private readonly JsonResult _forbiddenResult =
        new(new { Message = $"Only {nameof(UserRole.Personnel)} is authorized to perform this action.", StatusCode = StatusCodes.Status403Forbidden});
    
    public void OnAuthorization(AuthorizationFilterContext context)
    {
        if (SkipAuthorization(context))
        {
            return;
        }
        
        var user = (User?)context.HttpContext.Items["User"];

        if (user == null)
        {
            context.Result = _unauthenticatedResult;
        } else if (personnelOnly && user.Role != UserRole.Personnel)
        {
            context.Result = _forbiddenResult;
        }
    }

    private static bool SkipAuthorization(AuthorizationFilterContext context) => 
        context.ActionDescriptor.EndpointMetadata.Any(e => e.GetType() == typeof(AllowAnonymousAttribute));
}