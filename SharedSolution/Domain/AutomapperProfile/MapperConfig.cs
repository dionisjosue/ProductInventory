using System;
using AutoMapper;
using DomainLayer.AutomapperProfile;

namespace SharedLibrary.DomainLayer.AutomapperProfile
{
    public static class MapperConfig
    {
        public static IMapper GetMapperConfigs()
        {
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new ProductProfile());
                mc.AddProfile(new StockProfile());


            });
            return mappingConfig.CreateMapper();
        }
    }
}

