using System;
using AutoMapper;
using DomainLayer.ModelsDTO;
using SharedLibrary.Domain.Models;
using SharedLibrary.DomainLayer.ModelDTO;

namespace SharedLibrary.DomainLayer.AutomapperProfile
{
	public class ProductProfile:Profile
	{
		public ProductProfile()
		{

			CreateMap<ProductDTO, Product>().ReverseMap();
            CreateMap<ProductUpdateDTO, Product>().ReverseMap();
            CreateMap<ProductResponse, Product>().ReverseMap();

            CreateMap<ProductPrices, ProductPricesDTO>().ReverseMap();



        }

    }
}

