


using Infrastructure.Repositories;
using SharedLibrary.Domain.Repositories;
using SharedLibrary.Infrastructure.Database;
using Solution.Infrastructure.Repositories;

namespace SharedLibrary.Infrastructure.Repositories
{
    public class RepositoryWrapper : IRepositoryWrapper
    {
        private ProductDbContext _context { get; set; }

        public RepositoryWrapper(ProductDbContext context)
        {
            _context = context;
        }

        private IProductPricesRepository _productPrices;

        private IProductRepository _products;

        private IStockRepository _stocks;

        private IProcessEventsRepository _processEvents;



        public IProductRepository Products
        {
            get
            {
                if(_products == null)
                {
                    _products = new ProductRepository(_context);
                }
                return _products;
            }
        }

        public IStockRepository Stocks
        {
            get
            {
                if (_stocks == null)
                {
                    _stocks = new StockRepository(_context);
                }
                return _stocks;
            }

        }

        public IProductPricesRepository ProductPrices
        {
            get
            {
                if(_productPrices == null)
                {
                    _productPrices = new ProductPriceRepository(_context);
                }
                return _productPrices;
            }
        }

        public IProcessEventsRepository ProcessEvents
        {
            get
            {
                if(_processEvents == null)
                {
                    _processEvents = new ProcessEventsRepository(_context);
                }
                return _processEvents;
            }
        }

        public void Save()
        {
            _context.SaveChanges();
        }
        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }

}
