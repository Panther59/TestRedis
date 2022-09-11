using RedisClientLib;
using TestRedis.Models;

namespace TestRedis
{
	public interface IWeatherCacheRepo : IBaseCache<Weather>
	{
		Task<Weather> GetWeatherAsync(string city);
	}
}