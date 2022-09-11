using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedisClientLib
{
	public static class ServicesExtention
	{
		public static void AddRedis(this IServiceCollection services, string redisUrl)
		{
			//Configure other services up here
			var multiplexer = ConnectionMultiplexer.Connect(redisUrl);
			services.AddSingleton<IConnectionMultiplexer>(multiplexer);
		}
	}
}
