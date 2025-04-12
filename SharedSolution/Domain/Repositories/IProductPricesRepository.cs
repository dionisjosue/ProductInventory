using SharedLibrary.Domain.Models;

namespace SharedLibrary.Domain.Repositories
{
	public interface IProductPricesRepository:IBaseRepository<ProductPrices>
	{

		Task<ProductPrices> GetLastProductPriceByProductAndAmountAsync(long id, decimal price);

		Task<List<ProductPrices>> GetProductPricesByProductAsync(long productId);
	}
}

