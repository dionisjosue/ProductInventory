using System;
using SecurityDomain.Models;
using SecurityDomain.Repositories;
using SecurityInfrastructure.Database;

namespace SecurityInfrastructure.Repositories
{
    public class RoleActionRepository : BaseRepository<RoleAction>, IRoleActionRepository
    {
        public RoleActionRepository(SecurityDbContext context) : base(context)
        {
        }
    }
}

