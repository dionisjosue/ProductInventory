using System;
using Infrastructure.EventServices;
using SharedLibrary.Domain.Models;
using SharedLibrary.DomainLayer.ModelDTO;

namespace Infrastructure.IServices
{
	public interface IStockServiceCached
	{
		Task<List<Stock>> GetProductStockCachedAsync(long productId);

		Task ManageProductEventsInStockAsync(ProductEventsDTO product);

    }
}

