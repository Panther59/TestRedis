using Redis.OM.Modeling;
using RedisClientLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace TestRedis.Models
{
	// Root myDeserializedClass = JsonSerializer.Deserialize<Root>(myJsonResponse);
	public class Condition
	{
		[Searchable]
		public string Text { get; set; }

		public string Icon { get; set; }

		public int Code { get; set; }
	}

	public class Current
	{
		[Indexed]
		public int LastUpdatedEpoch { get; set; }

		[Indexed]
		public string LastUpdated { get; set; }

		public double TempC { get; set; }

		public double TempF { get; set; }

		[Indexed]
		public int IsDay { get; set; }

		[Indexed(CascadeDepth = 1)]
		public Condition Condition { get; set; }

		public double WindMph { get; set; }

		public double WindKph { get; set; }

		public double WindDegree { get; set; }

		public string WindDir { get; set; }
		
		public double PressureMb { get; set; }

		public double PressureIn { get; set; }

		public double PrecipMm { get; set; }

		public double PrecipIn { get; set; }

		public double Humidity { get; set; }
		
		public double Cloud { get; set; }

		public double FeelslikeC { get; set; }

		public double FeelslikeF { get; set; }

		public double VisKm { get; set; }

		public double VisMiles { get; set; }

		public double Uv { get; set; }

		public double GustMph { get; set; }

		public double GustKph { get; set; }
	}

	public class Location
	{
		[Indexed]
		public string? Name { get; set; }

		[Indexed]
		public string Region { get; set; }

		[Indexed]
		public string Country { get; set; }

		public double Lat { get; set; }

		public double Lon { get; set; }

		public string TzId { get; set; }

		public int LocaltimeEpoch { get; set; }

		public string Localtime { get; set; }
	}

	[Document(StorageType = StorageType.Json, Prefixes = new[] { WeatherCacheRepo.DefaultPrefix })]
	public class Weather : RedisCache
	{
		[Indexed(CascadeDepth = 1)]
		public Location? Location { get; set; }

		[Indexed(CascadeDepth = 1)]
		public Current? Current { get; set; }

		[RedisIdField]
		[Indexed]
		public override string Id => this.Location?.Name ?? Guid.NewGuid().ToString();
	}

	
}
