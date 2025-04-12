using System;
using SecurityDomain.DTO;
using SecurityDomain.Models;

namespace SecurityDomain.Repositories
{
	public interface IAppUserRepository: IBaseRepository<AppUser>
    {

        Task<UserAuthDTO> BuildUserAuthObject(AppUser appUser);

    }
}

