using BaggageTrackerApi.Entities.DTOs;
using BaggageTrackerApi.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace BaggageTrackerApi.Attributes;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class AuthorizeAttribute(UserRole? role) : Attribute, IAuthorizationFilter
{
    private readonly JsonResult _unauthenticatedResult =
        new(new { Message = "Authentication needed to perform this action.", StatusCode = StatusCodes.Status401Unauthorized });
    
    private readonly JsonResult _forbiddenResult =
        new(new { Message = $"Only users with {role.ToString()} role are authorized to perform this action.", StatusCode = StatusCodes.Status403Forbidden});
    
    public void OnAuthorization(AuthorizationFilterContext context)
    {
        var user = (UserDto?)context.HttpContext.Items["User"];

        if (user == null)
        {
            context.Result = _unauthenticatedResult;
        } else if (role != null && user.Role != role)
        {
            context.Result = _forbiddenResult;
        }
    }
}