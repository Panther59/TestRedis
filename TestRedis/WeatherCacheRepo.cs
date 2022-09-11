using Redis.OM;
using RedisClientLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestRedis.Models;

namespace TestRedis
{
	public class WeatherCacheRepo : BaseCache<Weather>, IBaseCache<Weather>, IWeatherCacheRepo
	{
		public const string DefaultPrefix = "Weather";
		public WeatherCacheRepo(RedisConnectionProvider provider) : base(provider)
		{
		}

		public override string Prefix => DefaultPrefix;

		public async Task<Weather?> GetWeatherAsync(string city)
		{
			//var list = this.Data.ToList();
			var list = this.Data.Where(x => x.Location.Name == city).ToList();
			return list?.FirstOrDefault();
		}
	}
}
