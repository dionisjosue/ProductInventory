using System;
using SharedLibrary.SharedItems.Shared;
using SharedLibrary.Domain.Models;

namespace SharedLibrary.Domain.Repositories
{
	public interface IProductRepository:IBaseRepository<Product>
	{
		Task<Product> FindByIdAsync(long id);

        Task<List<Product>> GetProductByCategoryAsync(string category);

    }
}

