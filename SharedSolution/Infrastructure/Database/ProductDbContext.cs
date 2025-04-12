using System;
using DomainLayer.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using SharedLibrary.Domain.Models;
using SharedLibrary.SharedItems.Shared;

namespace SharedLibrary.Infrastructure.Database
{
    public class ProductDbContext : DbContext
    {
        public ProductDbContext(DbContextOptions<ProductDbContext> options) : base(options)
        {
        }


        public DbSet<Product> Products { get; set; }

        public DbSet<ProductPrices> ProductPrices { get; set; }

        public DbSet<Stock> Stocks { get; set; }

        public DbSet<ProcessEvents> ProcessEvents { get; set; }


        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            // Get the entities that are being added or modified
            var modifiedEntities = ChangeTracker.Entries()
                .Where(e => e.State == EntityState.Added || e.State == EntityState.Modified);

            // Set the LastModifiedDate for each modified entity
            foreach (var entityEntry in modifiedEntities)
            {
                if (entityEntry.Entity is BaseEntity<long> baseEntity)
                {
                    baseEntity.LastModifiedDate = DateTime.Now;
                }
            }

            // Continue with the base SaveChangesAsync method
            return await base.SaveChangesAsync(cancellationToken);
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

        }

 
    }
}

