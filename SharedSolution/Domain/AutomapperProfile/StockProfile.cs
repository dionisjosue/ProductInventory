using System;
using AutoMapper;
using SharedLibrary.Domain.Models;
using SharedLibrary.DomainLayer.ModelDTO;

namespace DomainLayer.AutomapperProfile
{
	public class StockProfile:Profile
	{
		public StockProfile()
		{

            CreateMap<Stock, StockDTO>().ReverseMap();

            CreateMap<Stock, StockUpdateDTO>().ReverseMap();

        }

    }
}

