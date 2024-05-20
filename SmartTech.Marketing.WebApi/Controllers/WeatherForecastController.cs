using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartTech.Marketing.Application.Contract;
using SmartTech.Marketing.Domain.Entities;


namespace SmartTech.Marketing.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IDataBaseService _dataBaseService;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IDataBaseService dataBaseService)
        {
            _logger = logger;
            _dataBaseService = dataBaseService;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public  IEnumerable<WeatherForecast> Get()
        {
            //Country c= new Country { Id=1,CountryName="EG",CountryPrefix="eg"};
            //_dataBaseService.Country.Add(c);
            //_dataBaseService.DBSaveChanges();
            var t = _dataBaseService.Country.ToList();
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
