using System;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Options;
using SecurityDomain.Models;
using SharedLibrary.SharedItems.Shared;

namespace SecurityInfrastructure.Database
{
    public class SecurityDbContext : IdentityDbContext<AppUser, Role, Guid, UserClaim, UserRole, UserLogin, RoleAction, UserToken>
    {

        public SecurityDbContext(DbContextOptions<SecurityDbContext> options) : base(options)
        {

        }

        public DbSet<SecurityDomain.Models.Action> Actions { get; set; }
        public DbSet<AppUser> AppUsers { get; set; }
        public DbSet<Role>Roles { get; set; }
        public DbSet<RoleAction> RoleActions { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<UserToken> UserTokens { get; set; }

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

            modelBuilder.Entity<UserRole>(t =>
            {
                t.HasKey(r => new { r.RoleId, r.UserId});
            });

            modelBuilder.Entity<UserClaim>(t =>
            {
                t.HasKey(r => new { r.ActionId, r.UserId });
            });

            modelBuilder.Entity<UserLogin>(t =>
            {
                t.HasKey(r => new { r.ProviderKey, r.UserId });
            });

            modelBuilder.Entity<UserToken>(t =>
            {
                t.HasKey(r => new { r.LoginProvider, r.UserId });
            });
        }

     

    }
}

