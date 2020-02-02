using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JWTOne.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using Newtonsoft.Json;

namespace JWTOne.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        private IUser _userService;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IUser userService)
        {
            _logger = logger;
            _userService = userService;
        }

        [HttpGet]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IEnumerable<WeatherForecast> Get()
        {
            try
            {
                var rng = new Random();
                return Enumerable.Range(1, 5).Select(index => new WeatherForecast
                {
                    Date = DateTime.Now.AddDays(index),
                    TemperatureC = rng.Next(-20, 55),
                    Summary = Summaries[rng.Next(Summaries.Length)]
                })
                .ToArray();
            }
            catch (Exception)
            {
                return null;
            }
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public IActionResult Authenticate() //[FromBody]string model
        {
            try
            {
                var tok = _userService.GenerateToken();
                var resp = new JsonResult(tok);
                return resp;
            }
            catch (Exception)
            {
                return StatusCode(500); //Internal Server Error.
            }
        }
    }
}
