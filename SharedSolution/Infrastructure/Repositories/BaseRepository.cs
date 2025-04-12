using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore.Storage;
using SharedLibrary.Domain.Repositories;
using SharedLibrary.Infrastructure.Database;

namespace SharedLibrary.Infrastructure.Repositories
{
    public abstract class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        protected ProductDbContext _productDbContext { get; set; }
        private IDbContextTransaction _transaction;


        public BaseRepository(ProductDbContext context)
        {
            _productDbContext = context;
        }

        public void Create(T entity)
        {
            _productDbContext.Set<T>().Add(entity);

        }
        public void CreateRange(List<T> entity)
        {
            _productDbContext.Set<T>().AddRange(entity);

        }

        public async Task CreateRangeAsync(List<T> entity)
        {
            await _productDbContext.Set<T>().AddRangeAsync(entity);

        }

        public void Delete(T entity)
        {
            _productDbContext.Set<T>().Remove(entity);
        }

        public IQueryable<T> FindAll()
        {
            return _productDbContext.Set<T>();
        }

        public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression)
        {
            return _productDbContext.Set<T>().Where(expression);
        }

        public void Update(T entity)
        {
            _productDbContext.Set<T>().Update(entity);
        }

        public async Task CreateAsync(T entity)
        {
            await _productDbContext.Set<T>().AddAsync(entity);

        }

        public void BeginTransaction()
        {
            _transaction = _productDbContext.Database.BeginTransaction();
        }

        public void CommitTransaction()
        {
            _transaction.Commit();
            _transaction = null;
        }

        public void RollbackTransaction()
        {
            _transaction.Rollback();
            _transaction = null;
        }
    }
}
