using BaggageTrackerApi.Entities;
using BaggageTrackerApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace BaggageTrackerApi.Controllers;

[ApiController]
[Route("api/[controller]")]
[Attributes.Authorize(personnelOnly: true)]
public class UserController(UserService userService, AuthenticationService authenticationService) : ControllerBase
{
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
    
    [HttpGet("{flightNumber}")]
    public ActionResult<List<User>> GetUsersByFlightNumber([FromRoute] string flightNumber)
    {
        if (!userService.DoesFlightExist(flightNumber))
        {
            return NotFound($"Flight {flightNumber} does not exist.");
        }
        
        var usersByFlightNumber = userService.GetUsersByFlightNumber(flightNumber);

        return Ok(usersByFlightNumber);
    }
}