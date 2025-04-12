using System;
using DomainLayer.Models;
using SharedLibrary.Domain.Repositories;
using SharedLibrary.Infrastructure.Database;
using SharedLibrary.Infrastructure.Repositories;

namespace Infrastructure.Repositories
{
    public class ProcessEventsRepository : BaseRepository<ProcessEvents>, IProcessEventsRepository
    {
        public ProcessEventsRepository(ProductDbContext context) : base(context)
        {
        }
    }
}

