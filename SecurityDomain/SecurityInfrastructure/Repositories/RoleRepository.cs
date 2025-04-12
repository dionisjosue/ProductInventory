using System;
using SecurityDomain.Models;
using SecurityDomain.Repositories;
using SecurityInfrastructure.Database;

namespace SecurityInfrastructure.Repositories
{
    public class RoleRepository : BaseRepository<Role>, IRoleRepository
    {
        public RoleRepository(SecurityDbContext context) : base(context)
        {
        }
    }
}

