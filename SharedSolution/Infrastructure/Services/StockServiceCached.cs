using System;
using AutoMapper;
using Infrastructure.EventServices;
using Infrastructure.IServices;
using Microsoft.Extensions.Caching.Memory;
using SharedLibrary.Domain.Models;
using SharedLibrary.Domain.Repositories;
using SharedLibrary.DomainLayer.ModelDTO;
using SharedLibrary.SharedItems.Shared;

namespace Infrastructure.Services
{
	public class StockServiceCached:IStockServiceCached
	{
        private readonly IMemoryCache _cache;
        private IRepositoryWrapper _repository { get; set; }

        public StockServiceCached(IMemoryCache cache, 
            IRepositoryWrapper repository)
        {
            _cache = cache;
            _repository = repository;
        }

        public async Task<List<Stock>> GetProductStockCachedAsync(long productId)
        {

            string cacheKey = $"stocks_product_history_{productId}";
            var cachedData = _cache.Get<List<Stock>>(cacheKey);
            if (cachedData != null)
                return cachedData;

            var stocks = await _repository.Stocks.GetProductStockAsync(productId);

            _cache.Set(cacheKey, stocks, TimeSpan.FromMinutes(10));

            return stocks;


        }

        public async Task ManageProductEventsInStockAsync(ProductEventsDTO product)
        {
            var stock = new Stock();
            if (product.WasCreated)
            {
                stock = new Stock()
                {
                    ProductFk = product.Id,
                    Quantity = 0,
                    Description = "Producto creado nuevo, primer registro"
                };

            }
            else if (product.WasDeleted)
            {
                var quantity = _repository.Stocks.GetProductQuantity(product.Id);
                stock = new Stock()
                {
                    ProductFk = product.Id,
                    Quantity = quantity * -1,
                    Description = "El producto ha sido eliminado, registro para setear cantidades en 0"
                };

            }

            await _repository.Stocks.CreateAsync(stock);

            await _repository.SaveAsync();
        }
    }
}

