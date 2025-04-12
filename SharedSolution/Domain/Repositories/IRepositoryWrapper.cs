using SharedLibrary.Domain.Repositories;

namespace SharedLibrary.Domain.Repositories
{
    public interface IRepositoryWrapper
    {

        IProductRepository Products { get; }

        IStockRepository Stocks { get; }

        IProductPricesRepository ProductPrices { get; }

        IProcessEventsRepository ProcessEvents { get; }


        void Save();

        Task SaveAsync();
    }
}
