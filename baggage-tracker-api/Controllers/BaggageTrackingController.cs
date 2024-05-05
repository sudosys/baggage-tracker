using BaggageTrackerApi.Attributes;
using BaggageTrackerApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace BaggageTrackerApi.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(personnelOnly: true)]
public class BaggageTrackingController(BaggageTrackingService baggageTrackingService) : ControllerBase
{
    [HttpGet("baggage-qr-code")]
    public IActionResult GetBaggageQrCodes(string flightNumber)
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
}