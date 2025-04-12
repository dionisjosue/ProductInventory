using System;
using DomainLayer.ModelsDTO;
using SharedLibrary.DomainLayer.ModelDTO;

namespace Infrastructure.IServices
{
	public interface IProductService
	{
		Task<List<ProductResponse>> GetAllProductsCachedAsync(string? moneda = null);

        Task<List<ProductResponse>> GetProductsByCategoryCachedAsync(string category, string? moneda = null);

        Task<List<ProductPricesDTO>> GetProductsPricesCachedAsync(long productId,string? moneda= null);


    }
}

