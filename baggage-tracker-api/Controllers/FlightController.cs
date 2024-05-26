using BaggageTrackerApi.Attributes;
using BaggageTrackerApi.Models;
using BaggageTrackerApi.Models.Registration;
using BaggageTrackerApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace BaggageTrackerApi.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize(personnelOnly: true)]
public class FlightController(FlightService flightService) : ControllerBase
{
    [HttpPost("register-manifest")]
    [ProducesResponseType(typeof(FileStreamResult), StatusCodes.Status200OK)]
    public async Task<IActionResult> RegisterManifest([FromBody] List<FlightManifest> flightManifest, CancellationToken cancellationToken)
    {
        var compressed = await flightService.RegisterFlightManifests(flightManifest, cancellationToken);
        var fileStreamResult = new FileStreamResult(compressed, "application/zip")
        {
            FileDownloadName = "Manifests.zip"
        };

        return fileStreamResult;
    }

    [HttpGet("active-flight")]
    public async Task<ActionResult<ActiveFlightsResponse>> GetActiveFlights(CancellationToken cancellationToken)
    {
        var activeFlights = await flightService.GetActiveFlights(cancellationToken);

        return Ok(new ActiveFlightsResponse(activeFlights));
    }
}