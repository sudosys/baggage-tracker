using BaggageTrackerApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace BaggageTrackerApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MockDataController(MockDataService mockDataService) : ControllerBase
{
    private readonly MockDataService _mockDataService = 
        mockDataService ?? throw new ArgumentNullException(nameof(mockDataService));
    
    [HttpPost("mock-user")]
    public IActionResult SetMockUsers()
    {
        var result = mockDataService.PersistUserMockData();

        return result == 0 ? NoContent() : BadRequest();
    }
    
    [HttpPost("mock-flight")]
    public IActionResult SetMockFlights()
    {
        var result = mockDataService.PersistFlightMockData();

        return result == 0 ? NoContent() : BadRequest();
    }
    
    [HttpPost("mock-baggage")]
    public IActionResult SetMockBaggage()
    {
        var result = mockDataService.PersistBaggageMockData();

        return result == 0 ? NoContent() : BadRequest();
    }
    
    [HttpDelete("mock-user")]
    public IActionResult DeleteMockUsers()
    {
        mockDataService.DeleteUserMockData();

        return NoContent();
    }
    
    [HttpDelete("mock-flight")]
    public IActionResult DeleteMockFlights()
    {
        mockDataService.DeleteFlightMockData();

        return NoContent();
    }
    
    [HttpDelete("mock-baggage")]
    public IActionResult DeleteMockBaggages()
    {
        mockDataService.DeleteBaggageMockData();

        return NoContent();
    }
}