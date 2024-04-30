using BaggageTrackerApi.Entities.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace BaggageTrackerApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController(BaggageTrackerDbContext baggageTrackerDbContext) : ControllerBase
{
    private readonly BaggageTrackerDbContext _baggageTrackerDbContext = 
        baggageTrackerDbContext ?? throw new ArgumentNullException(nameof(baggageTrackerDbContext));

    [HttpGet("demo-user")]
    public IActionResult GetUsers()
    {
        var users = _baggageTrackerDbContext.Users.Select(u => new UserDto
        {
            Id = u.Id,
            Name = u.Name,
            Surname = u.Surname,
            ActiveFlight = new FlightDto
            {
                Id = u.ActiveFlight.Id,
                FlightNumber = u.ActiveFlight.FlightNumber
            },
            Baggages = u.Baggages.Select(b => new BaggageDto
            {
                Id = b.Id,
                TagNumber = b.TagNumber
            })
        }).ToList();

        return Ok(users);
    }
}