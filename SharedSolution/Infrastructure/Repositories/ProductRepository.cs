using System;
using Microsoft.EntityFrameworkCore;
using SharedLibrary.Domain.Models;
using SharedLibrary.Domain.Repositories;
using SharedLibrary.Infrastructure.Database;
using SharedLibrary.Infrastructure.Repositories;
using SharedLibrary.SharedItems.Shared;

namespace Solution.Infrastructure.Repositories
{
    public class ProductRepository : BaseRepository<Product>, IProductRepository
    {
        public ProductRepository(ProductDbContext context) : base(context)
        {
        }

        public async Task<Product> FindByIdAsync(long id)
        {
            var product = await _productDbContext.Products.FirstOrDefaultAsync(t => t.Id == id);

            return product;
        }

        public async Task<List<Product>> GetProductByCategoryAsync(string category)
        {
            var products = await _productDbContext.Products.Where(t =>
                            string.Equals(t.Category,category,StringComparison.OrdinalIgnoreCase))
                            .ToListAsync();

            return products;
        }
    }
}

