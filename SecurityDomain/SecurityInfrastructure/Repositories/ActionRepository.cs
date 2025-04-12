using System;
using SecurityDomain.Repositories;
using SecurityInfrastructure.Database;

namespace SecurityInfrastructure.Repositories
{
    public class ActionRepository : BaseRepository<Action>, IActionRepository
    {
        public ActionRepository(SecurityDbContext context) : base(context)
        {
        }
    }
}

