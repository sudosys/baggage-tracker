using BaggageTrackerApi.Attributes;
using BaggageTrackerApi.Entities;
using BaggageTrackerApi.Entities.DTOs;
using BaggageTrackerApi.Enums;
using BaggageTrackerApi.Exceptions;
using BaggageTrackerApi.Models;
using BaggageTrackerApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace BaggageTrackerApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BaggageTrackingController(BaggageTrackingService baggageTrackingService) : ControllerBase
{
    [HttpGet("baggage-qr-code")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(FileStreamResult), StatusCodes.Status200OK)]
    [Authorize(personnelOnly: true)]
    public ActionResult<object> GetBaggageQrCodes([FromQuery] string flightNumber)
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
        catch (FlightDoesNotExistException e)
        {
            return NotFound(new PlainResponse(e.Message));
        }
    }

    [HttpGet("baggage-status")]
    [Authorize]
    public ActionResult<BaggageInfoResponse> GetBaggageStatus([FromQuery] long userId)
    {
        try
        {
            var baggages = baggageTrackingService.GetBaggageStatus(userId);
            return Ok(baggages);
        }
        catch (ApiDomainException e)
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
        catch (ApiDomainException e)
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
    
    [HttpGet("{flightNumber}")]
    public ActionResult<List<User>> GetUsersByFlightNumber([FromRoute] string flightNumber)
    {
        try
        {
            var usersByFlightNumber = baggageTrackingService.GetPassengersByFlightNumber(flightNumber);
            return Ok(usersByFlightNumber);
        }
        catch (FlightDoesNotExistException e)
        {
            return NotFound(new PlainResponse(e.Message));
        }
    }
    
    [HttpGet("passenger-allowed-statuses")]
    [Authorize]
    public ActionResult<PlainResponse> GetPassengerAllowedStatuses() => 
        Ok(new PlainResponse(BaggageTrackingService.PassengerAllowedStatuses));
    
    [HttpGet("personnel-allowed-statuses")]
    [Authorize(personnelOnly: true)]
    public ActionResult<PlainResponse> GetPersonnelAllowedStatuses() => 
        Ok(new PlainResponse(BaggageTrackingService.PersonnelAllowedStatuses));
}