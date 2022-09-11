using Redis.OM.Modeling;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace RedisClientLib
{
	public abstract class RedisCache
	{
		[RedisIdField]
		[Indexed]
		public abstract string Id { get; }
	}
}
