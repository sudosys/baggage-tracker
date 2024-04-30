using BaggageTrackerApi.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BaggageTrackerApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController(BaggageTrackerDbContext baggageTrackerDbContext, UserService userService) : ControllerBase
{
    [HttpGet]
    public IActionResult GetUsers()
    {
        var users = baggageTrackerDbContext.Users
            .Include(u => u.ActiveFlight)
            .Include(u => u.Baggages)
            .Select(userService.ConvertToDto)
            .ToList();

        return Ok(users);
    }
    
    [HttpGet("{userId:long}")]
    public IActionResult GetUser([FromRoute] long userId)
    {
        var user = baggageTrackerDbContext.Users
            .Include(u => u.ActiveFlight)
            .Include(u => u.Baggages)
            .FirstOrDefault(u => u.Id == userId);

        if (user == null)
        {
            return NotFound();
        }

        var dto = userService.ConvertToDto(user);
        return Ok(dto);
    }
}