using AutoMapper;
using Redis.OM;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using TestRedis.Models;

namespace TestRedis.HostedServices
{
	public class BootstapService : IHostedService
	{
		private readonly RedisConnectionProvider provider;
		private readonly IWeatherCacheRepo weatherCacheRepo;
		private readonly IWeatherRepo weatherRepo;

		public BootstapService(
			RedisConnectionProvider provider,
			IWeatherCacheRepo weatherCacheRepo,
			IWeatherRepo weatherRepo)
		{
			this.provider = provider;
			this.weatherCacheRepo = weatherCacheRepo;
			this.weatherRepo = weatherRepo;
		}

		public async Task StartAsync(CancellationToken cancellationToken)
		{
			await this.weatherCacheRepo.CreateIndexAsync(true);
			await this.weatherRepo.Initialize();
		}



		public Task StopAsync(CancellationToken cancellationToken)
		{
			return Task.CompletedTask;
		}
	}
}
