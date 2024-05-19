using BaggageTrackerApi.Attributes;
using BaggageTrackerApi.Entities.DTOs;
using BaggageTrackerApi.Enums;
using BaggageTrackerApi.Models;
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
            return NotFound(new PlainResponse(e.Message));
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
            return BadRequest(new PlainResponse(e.Message));
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
            return BadRequest(new PlainResponse(e.Message));
        }
    }
    
    [HttpPost("qr-code-scan")]
    [Authorize]
    public ActionResult<QrCodeScanResponse> ProcessQrCodeScan([FromQuery] string qrCodeData)
    {
        var user = (UserDto?)HttpContext.Items["User"];
        var response = baggageTrackingService.ProcessQrCodeScan(user!, qrCodeData);
        switch (response.ScanResult)
        {
            case QrCodeScanResult.Success:
            case QrCodeScanResult.NotOwnedByPassenger:
                return Ok(response);
            case QrCodeScanResult.CodeInvalid:
            case QrCodeScanResult.UnknownError:
            default:
                return BadRequest(response);
        }
    }
}