using BaggageTrackerApi.Entities;
using BaggageTrackerApi.Enums;
using BaggageTrackerApi.Models.Registration;
using BaggageTrackerApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace BaggageTrackerApi.Controllers;

[ApiController]
[Route("api/[controller]")]
[Attributes.Authorize(personnelOnly: true)]
public class UserController(UserService userService) : ControllerBase
{
    [HttpPost("register")]
    public IActionResult RegisterUser([FromBody] UserRegistration userRegistration)
    {
        userService.RegisterUser(userRegistration);

        return NoContent();
    }

    [HttpDelete]
    public IActionResult DeleteUser(long userId)
    {
        try
        {
            userService.DeleteUser(userId);
        }
        catch (NullReferenceException exception)
        {
            return NotFound(exception.Message);
        }
        catch (InvalidOperationException exception)
        {
            return BadRequest(exception.Message);
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
    
    [HttpGet("{flightNumber}")]
    public ActionResult<List<User>> GetUsersByFlightNumber([FromRoute] string flightNumber)
    {
        try
        {
            var usersByFlightNumber = userService.GetUsersByFlightNumber(flightNumber);
            return Ok(usersByFlightNumber);
        }
        catch (Exception e)
        {
            return NotFound(e.Message);
        }
    }
}