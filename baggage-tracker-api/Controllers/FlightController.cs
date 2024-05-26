using BaggageTrackerApi.Attributes;
using BaggageTrackerApi.Exceptions;
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
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<object>> RegisterManifest([FromBody] List<FlightManifest> flightManifest, CancellationToken cancellationToken)
    {
        try
        {
            var compressed = await flightService.RegisterFlightManifests(flightManifest, cancellationToken);
            var fileStreamResult = new FileStreamResult(compressed, "application/zip")
            {
                FileDownloadName = "passenger-credentials.zip"
            };
            return fileStreamResult;
        }
        catch (FlightAlreadyExistsException e)
        {
            return BadRequest(new PlainResponse(e.Message));
        }
    }

    [HttpGet("active-flight")]
    public async Task<ActionResult<ActiveFlightsResponse>> GetActiveFlights(CancellationToken cancellationToken)
    {
        var activeFlights = await flightService.GetActiveFlights(cancellationToken);

        return Ok(new ActiveFlightsResponse(activeFlights));
    }
}