using Microsoft.AspNetCore.Mvc;
using TestRedis.Models;

namespace TestRedis.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class WeatherForecastController : ControllerBase
	{
		private readonly ILogger<WeatherForecastController> _logger;
		private readonly IWeatherRepo weatherRepo;

		public WeatherForecastController(ILogger<WeatherForecastController> logger, IWeatherRepo weatherRepo)
		{
			_logger = logger;
			this.weatherRepo = weatherRepo;
		}

		[HttpGet]
		public async Task<Weather?> Get([FromQuery] string city)
		{
			return await weatherRepo.GetWeatherAsync(city);	
		}
	}
}