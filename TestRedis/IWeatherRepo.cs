using TestRedis.Models;

namespace TestRedis
{
	public interface IWeatherRepo
	{
		Task<Weather?> GetWeatherAsync(string city);
		Task Initialize();
	}
}