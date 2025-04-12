using System;
using Microsoft.EntityFrameworkCore;
using SharedLibrary.Domain.Models;
using SharedLibrary.Domain.Repositories;
using SharedLibrary.Infrastructure.Database;
using SharedLibrary.Infrastructure.Repositories;

namespace Solution.Infrastructure.Repositories
{
    public class StockRepository : BaseRepository<Stock>, IStockRepository
    {
        public StockRepository(ProductDbContext context) : base(context)
        {
        }

        //Get current quantity of products stocks
        public long GetProductQuantity(long productId)
        {
            var result = _productDbContext.Stocks
                .Where(t => t.ProductFk == productId)
                .Sum(t => t.Quantity);

            return result;
        }

        public async Task<List<Stock>> GetProductStockAsync(long productId)
        {
            var stocks = await _productDbContext.Stocks
                .Include(t=> t.Product)
                .Where(t => t.ProductFk == productId).AsNoTracking().ToListAsync();

            return stocks;
        }

    }
}

