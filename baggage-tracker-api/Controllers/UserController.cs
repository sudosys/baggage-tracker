using BaggageTrackerApi.Entities;
using BaggageTrackerApi.Enums;
using BaggageTrackerApi.Models.Authentication;
using BaggageTrackerApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BaggageTrackerApi.Controllers;

[ApiController]
[Route("api/[controller]")]
[Attributes.Authorize(personnelOnly: true)]
public class UserController(UserService userService, AuthenticationService authenticationService) : ControllerBase
{
    [HttpPost("authenticate")]
    [AllowAnonymous]
    public async Task<ActionResult<AuthenticationResponse>> Authenticate([FromBody] AuthenticationRequest request)
    {
        var authResponse = await authenticationService.AuthenticateUser(request);

        return authResponse.Status == AuthenticationStatus.Success ? Ok(authResponse) : BadRequest(authResponse);
    }
    
    [HttpGet]
    public ActionResult<List<User>> GetUsers([FromQuery] bool passengersOnly = false)
    {
        return Ok(userService.GetUsers(passengersOnly));
    }
    
    [HttpGet("{userId:long}")]
    public IActionResult GetUserById([FromRoute] long userId)
    {
        var user = userService.GetUserById(userId);

        return user == null ? NotFound() : Ok(user);
    }
}