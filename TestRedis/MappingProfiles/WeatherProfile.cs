using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestRedis.Models;

namespace TestRedis.MappingProfiles
{
	public class WeatherProfile : Profile
	{
		public WeatherProfile()
		{
			CreateMap<WeatherResponse, Weather>();
			CreateMap<Weather, WeatherResponse>();
		}
	}
}
