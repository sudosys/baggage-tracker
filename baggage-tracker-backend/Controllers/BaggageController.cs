using BaggageTrackerApi.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections;

namespace BaggageTrackerApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaggageController : ControllerBase
    {
        [HttpGet]
        [Route("CheckBaggagePossession")]
        public bool CheckBaggagePossession(string ubc, string passengerHash)
        {
            return BaggageService.CheckBaggagePossession(ubc, passengerHash);
        }

        [HttpGet]
        [Route("CheckBaggageStatus")]
        public ArrayList CheckBaggageStatus(string passengerHash)
        {
            return BaggageService.CheckBaggageStatus(passengerHash);
        }

        [HttpGet]
        [Route("SetBaggageStatus")]
        public void SetBaggageStatus(string ubc, string status)
        {
            BaggageService.SetBaggageStatus(ubc, status);
        }
    }
}
