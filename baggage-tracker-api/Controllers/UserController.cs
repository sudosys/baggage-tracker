using BaggageTrackerApi.Entities.DTOs;
using BaggageTrackerApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace BaggageTrackerApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController(UserService userService) : ControllerBase
{
    [HttpGet]
    public ActionResult<List<UserDto>> GetUsers()
    {
        return Ok(userService.GetUsers());
    }
    
    [HttpGet("{userId:long}")]
    public IActionResult GetUserById([FromRoute] long userId)
    {
        var user = userService.GetUserById(userId);

        return user == null ? NotFound() : Ok(user);
    }
}