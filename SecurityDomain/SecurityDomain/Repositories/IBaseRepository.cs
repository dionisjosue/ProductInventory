using System.Linq.Expressions;


namespace SecurityDomain.Repositories
{
    public interface IBaseRepository<T>where T : class
    {
        IQueryable<T> FindAll();
        IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression);


        void Create(T entity);
        void Update(T entity);
        void Delete(T entity);

        Task CreateAsync(T entity);

        void CreateRange(List<T> entity);

        Task CreateRangeAsync(List<T> entity);

        void BeginTransaction();
        void CommitTransaction();
        void RollbackTransaction();

    }
}
