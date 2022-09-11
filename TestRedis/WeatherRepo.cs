using AutoMapper;
using Redis.OM;
using RedisClientLib;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using TestRedis.Models;

namespace TestRedis
{
	public class WeatherRepo : IWeatherRepo
	{
		private readonly ILogger<WeatherRepo> logger;
		private readonly IMapper mapper;
		private readonly IWeatherCacheRepo weatherCacheRepo;

		public WeatherRepo(ILogger<WeatherRepo> logger, IMapper mapper, IWeatherCacheRepo weatherCacheRepo)
		{
			this.logger = logger;
			this.mapper = mapper;
			this.weatherCacheRepo = weatherCacheRepo;
		}

		public async Task<Weather?> GetWeatherAsync(string city)
		{
			var weather = await this.weatherCacheRepo.GetWeatherAsync(city);
			if (weather == null)
			{
				weather = await GetWeatherInfoFromService(city);
				if (weather != null)
				{
					await this.weatherCacheRepo.Add(weather);
				}
			}
			else
			{
				this.logger.LogInformation($"Returning Weather information for {city} from Cache");
			}

			return weather;
		}

		private async Task<Weather> GetWeatherInfoFromService(string city)
		{
			this.logger.LogInformation($"Getting Weather information for {city} from Internet");
			var client = new RestClient($"https://weatherapi-com.p.rapidapi.com/current.json?q={city}");
			var request = new RestRequest();
			request.AddHeader("X-RapidAPI-Key", "4dbd3008a1msh080d71cbc850b58p17c575jsn8cfe59ab7bfb");
			request.AddHeader("X-RapidAPI-Host", "weatherapi-com.p.rapidapi.com");
			RestResponse response = await client.ExecuteAsync<Weather>(request);
			var data = JsonSerializer.Deserialize<WeatherResponse>(response.Content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
			return this.mapper.Map<Weather>(data);
		}

		public async Task Initialize()
		{
			var cities = new List<string>() { "Mumbai" };
			foreach (var city in cities)
			{
				await this.GetWeatherAsync(city);
			}
		}
	}
}
