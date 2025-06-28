using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Controllers
{
    [ApiController] // it is used for automatic model validation and model binding !!!
    [Route("api/WeatherForecast")] // inga vandhu controller nu sollitu irrukum ok va ??? 
    // adha vandhu namma route eppdi irrukanu nu paakanu correct ah??
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

 // idhu just oru instance , ok va (instance na ILogger<WeatherForecastController> -> ippdi oru class ah namma declare pandrom ok va !!!)
        private readonly ILogger<WeatherForecastController> _logger;

        // indhu vandhu constructor function
        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }


        // idha vandhu action method nu soldrom (idhu dha vandhu endpoints ok va ???)
        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }
    }
}
