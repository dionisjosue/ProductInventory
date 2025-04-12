using System;
using Microsoft.EntityFrameworkCore;
using SharedLibrary.Domain.Models;
using SharedLibrary.Domain.Repositories;
using SharedLibrary.Infrastructure.Database;
using SharedLibrary.Infrastructure.Repositories;

namespace Solution.Infrastructure.Repositories
{
    public class ProductPriceRepository : BaseRepository<ProductPrices>, IProductPricesRepository
    {
        public ProductPriceRepository(ProductDbContext context) : base(context)
        {
        }

        public async Task<ProductPrices> GetLastProductPriceByProductAndAmountAsync(long id, decimal price)
        {
            var result = await _productDbContext.ProductPrices.FirstOrDefaultAsync(t =>
                                t.Price == price && t.Id == id);

            return result;
        }

        public async Task<List<ProductPrices>> GetProductPricesByProductAsync(long productId)
        {
            var result = await _productDbContext.ProductPrices.Where(t => t.ProductFk == productId)
                .AsNoTracking().ToListAsync();

            return result;
        }
    }
}

