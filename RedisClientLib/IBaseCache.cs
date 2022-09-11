using Redis.OM.Searching;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedisClientLib
{
	public interface IBaseCache<T> 
		where T : RedisCache
	{
		RedisCollection<T> Data { get; }
		Task CreateIndexAsync(bool force = false);
		Task Add(T newRec);
		Task Delete(T rec);
	}
}
