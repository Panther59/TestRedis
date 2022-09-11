using Redis.OM;
using Redis.OM.Searching;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedisClientLib
{
	public abstract class BaseCache<T> : IBaseCache<T>
		where T : RedisCache
	{
		private readonly RedisConnectionProvider provider;
		private RedisCollection<T> list;
		public abstract string Prefix { get; }
		public BaseCache(RedisConnectionProvider provider)
		{
			this.provider = provider;
			this.list = (RedisCollection<T>)provider.RedisCollection<T>();
		}

		public RedisCollection<T> Data => this.list;

		public async Task Add(T newRec)
		{
			await this.Data.InsertAsync(newRec);
		}

		public async Task CreateIndexAsync(bool force = false)
		{
			var info = (await provider.Connection.ExecuteAsync("FT._LIST")).ToArray().Select(x => x.ToString());
			if (info.All(x => x == "weather-idx"))
			{
				if (force)
				{
					await provider.Connection.DropIndexAsync(typeof(T));
					await provider.Connection.CreateIndexAsync(typeof(T));
				}
			}
			else
			{
				await provider.Connection.CreateIndexAsync(typeof(T));
			}
		}

		public async Task Delete(T rec)
		{
			await provider.Connection.UnlinkAsync($"{this.Prefix}:{rec.Id}");
		}
	}
}
