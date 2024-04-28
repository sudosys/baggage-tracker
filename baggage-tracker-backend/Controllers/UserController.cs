using BaggageTrackerApi.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections;

namespace BaggageTrackerApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {

        [HttpGet]
        [Route("GetUserData")]
        public ArrayList GetUserData(string username, string flightNumber)
        {
            return UserService.GetUserData(username, flightNumber);
        }
    }

}
