using BaggageTrackerApi.Entities;
using BaggageTrackerApi.Enums;
using BaggageTrackerApi.Models;
using BaggageTrackerApi.Models.Registration;
using BaggageTrackerApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace BaggageTrackerApi.Controllers;

[ApiController]
[Route("api/[controller]")]
[Attributes.Authorize(personnelOnly: true)]
public class UserController(UserService userService) : ControllerBase
{
    [HttpPost("register-manifest")]
    public ActionResult<ManifestRegistrationResponse> RegisterManifest([FromBody] FlightManifest flightManifest)
    {
        var credentials = userService.RegisterFlightManifest(flightManifest);
        var status = ValidateManifestRegistration(flightManifest, credentials);

        return Ok(new ManifestRegistrationResponse(status, credentials));
    }

    private ManifestRegistrationStatus ValidateManifestRegistration(
        FlightManifest manifest,
        List<PassengerCredential> credentials)
    {
        if (credentials.Count == manifest.Passengers.Count())
        {
            return ManifestRegistrationStatus.Completed;
        } else if (credentials.Count != manifest.Passengers.Count())
        {
            return ManifestRegistrationStatus.PartiallyCompleted;
        }
        
        return ManifestRegistrationStatus.Failed;
    }

    [HttpDelete]
    public IActionResult DeleteUser([FromQuery] long userId)
    {
        try
        {
            userService.DeleteUser(userId);
        }
        catch (NullReferenceException exception)
        {
            return NotFound(new PlainResponse(exception.Message));
        }
        catch (InvalidOperationException exception)
        {
            return BadRequest(new PlainResponse(exception.Message));
        }

        return NoContent();
    }
    
    [HttpGet]
    public ActionResult<List<User>> GetUsers([FromQuery] bool passengersOnly = false)
    {
        return Ok(userService.GetUsers(passengersOnly));
    }
    
    [HttpGet("{userId:long}")]
    public ActionResult<User> GetUserById([FromRoute] long userId)
    {
        var user = userService.GetUserById(userId);

        return user == null ? NotFound() : Ok(user);
    }
}