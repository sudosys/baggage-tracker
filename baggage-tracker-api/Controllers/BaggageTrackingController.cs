using BaggageTrackerApi.Attributes;
using BaggageTrackerApi.Entities.DTOs;
using BaggageTrackerApi.Enums;
using BaggageTrackerApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace BaggageTrackerApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BaggageTrackingController(BaggageTrackingService baggageTrackingService) : ControllerBase
{
    [HttpGet("baggage-qr-code")]
    [Authorize(personnelOnly: true)]
    public IActionResult GetBaggageQrCodes([FromQuery] string flightNumber)
    {
        try
        {
            var compressedQrCodes = baggageTrackingService.GenerateQrCodesForFlight(flightNumber);

            var fileStreamResult = new FileStreamResult(compressedQrCodes, "application/zip")
            {
                FileDownloadName = $"{flightNumber}_GeneratedQrCodes.zip"
            };

            return fileStreamResult;
        }
        catch (Exception e)
        {
            return NotFound(e.Message);
        }
    }

    [HttpGet("baggage-status")]
    [Authorize]
    public ActionResult<List<BaggageDto>> GetBaggageStatus([FromQuery] long userId)
    {
        try
        {
            var baggages = baggageTrackingService.GetBaggageStatus(userId);
            return Ok(baggages);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    
    [HttpPost("baggage-status")]
    [Authorize]
    public IActionResult SetBaggageStatus([FromQuery] Guid baggageId, [FromQuery] BaggageStatus newStatus)
    {
        try
        {
            var user = (UserDto?)HttpContext.Items["User"];
            baggageTrackingService.SetBaggageStatus(user!, baggageId, newStatus);
            return NoContent();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}