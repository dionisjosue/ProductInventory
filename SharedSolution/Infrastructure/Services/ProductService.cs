using System;
using System.Text.Json;
using AutoMapper;
using DomainLayer.ModelsDTO;
using Infrastructure.IServices;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using SharedLibrary.Domain.Models;
using SharedLibrary.Domain.Repositories;
using SharedLibrary.DomainLayer.ModelDTO;

namespace Infrastructure.Services
{
	public class ProductService:IProductService
	{
        private readonly IMemoryCache _cache;
        private IRateService _rateService { get; set; }
        private IRepositoryWrapper _repository { get; set; }
        private IMapper _mapper { get; set; }

		public ProductService(IMemoryCache cache, IRateService rateService,
            IRepositoryWrapper repository, IMapper mapper)
		{
			_cache = cache;
			_rateService = rateService;
			_repository = repository;
            _mapper = mapper;
		}

        public async Task<List<ProductResponse>> GetAllProductsCachedAsync(string? moneda = null)
        {
            const string cacheKey = "products_all";
            var cachedData = _cache.Get<List<ProductResponse>>(cacheKey);
            if (cachedData != null)
                return cachedData;

            var products = await _repository.Products.FindAll().ToListAsync();
            var productsResult = _mapper.Map<List<ProductResponse>>(products);

            if (!string.IsNullOrWhiteSpace(moneda))
            {
                var rate = await _rateService.GetCurrencyValueAsync(moneda);
                foreach (var product in productsResult)
                {
                    product.ConvertedPrice = _rateService.GetPriceConverted(rate, product.CurrentPrice);
                }
            }

            _cache.Set(cacheKey, products, TimeSpan.FromMinutes(10));
            return productsResult;
        }

        public async Task<List<ProductResponse>> GetProductsByCategoryCachedAsync(string category,string? moneda = null)
        {
            string cacheKey = $"products_all_{category}";
            var cachedData = _cache.Get<List<ProductResponse>>(cacheKey);
            if (cachedData != null)
                return cachedData;

            var products = await _repository.Products.GetProductByCategoryAsync(category);

            var productsResult = _mapper.Map<List<ProductResponse>>(products);

            if (!string.IsNullOrWhiteSpace(moneda))
            {
                var rate = await _rateService.GetCurrencyValueAsync(moneda);
                foreach (var product in productsResult)
                {
                    product.ConvertedPrice = _rateService.GetPriceConverted(rate, product.CurrentPrice);
                }
            }

            _cache.Set(cacheKey, products, TimeSpan.FromMinutes(10));

            return productsResult;
        }

        public async Task<List<ProductPricesDTO>> GetProductsPricesCachedAsync(long productId, string? moneda = null)
        {
            string cacheKey = $"products_prices_{productId}";

            var cachedData = _cache.Get<List<ProductPricesDTO>>(cacheKey);
            if (cachedData != null)
                return cachedData;

            var products = await _repository.ProductPrices.GetProductPricesByProductAsync(productId);

            var productsResult = _mapper.Map<List<ProductPricesDTO>>(products);

            if (!string.IsNullOrWhiteSpace(moneda))
            {
                var rate = await _rateService.GetCurrencyValueAsync(moneda);
                foreach (var product in productsResult)
                {
                    product.ConvertedPrice = _rateService.GetPriceConverted(rate, product.Price);
                }
            }

            _cache.Set(cacheKey, products, TimeSpan.FromMinutes(10));

            return productsResult;

        }
    }

}

