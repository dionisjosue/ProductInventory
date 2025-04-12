

using SharedLibrary.Domain.Models;

namespace SharedLibrary.Domain.Repositories
{
	public interface IStockRepository:IBaseRepository<Stock>
	{

		long GetProductQuantity(long productId);

        Task<List<Stock>> GetProductStockAsync(long productId);


    }
}

