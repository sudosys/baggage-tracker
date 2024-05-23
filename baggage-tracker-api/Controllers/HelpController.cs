using BaggageTrackerApi.Attributes;
using BaggageTrackerApi.Models;
using BaggageTrackerApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace BaggageTrackerApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class HelpController(HelpService helpService) : ControllerBase
    {
        [HttpGet]
        public ActionResult<PlainResponse> GetHelpText()
        {
            var helpText = helpService.GetHelpText();

            return helpText != null ? 
                Ok(new PlainResponse(helpText)) :
                NotFound(new PlainResponse("Help text not found."));
        }
    }
}
