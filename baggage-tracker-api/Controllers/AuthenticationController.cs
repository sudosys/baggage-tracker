using BaggageTrackerApi.Enums;
using BaggageTrackerApi.Models;
using BaggageTrackerApi.Models.Authentication;
using BaggageTrackerApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BaggageTrackerApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthenticationController(AuthenticationService authenticationService) : ControllerBase
{
    [HttpPost("authenticate")]
    [AllowAnonymous]
    public async Task<ActionResult<AuthenticationResponse>> Authenticate([FromBody] AuthenticationRequest request)
    {
        var authResponse = await authenticationService.AuthenticateUser(request);

        return authResponse.Status == AuthenticationStatus.Success ? 
            Ok(authResponse) : 
            BadRequest(new PlainResponse($"{authResponse.Status}: Invalid username or password."));
    }
}