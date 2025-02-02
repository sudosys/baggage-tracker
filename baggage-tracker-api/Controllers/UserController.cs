using BaggageTrackerApi.Entities;
using BaggageTrackerApi.Exceptions;
using BaggageTrackerApi.Models;
using BaggageTrackerApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace BaggageTrackerApi.Controllers;

[ApiController]
[Route("api/[controller]")]
[Attributes.Authorize(personnelOnly: true)]
public class UserController(UserService userService) : ControllerBase
{
    [HttpDelete]
    public IActionResult DeleteUser([FromQuery] long userId)
    {
        try
        {
            userService.DeleteUser(userId);
        }
        catch (UserDoesNotExistException exception)
        {
            return NotFound(new PlainResponse(exception.Message));
        }
        catch (PersonnelCanNotBeDeletedException exception)
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