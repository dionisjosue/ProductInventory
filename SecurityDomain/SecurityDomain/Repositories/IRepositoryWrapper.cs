
namespace SecurityDomain.Repositories
{
    public interface IRepositoryWrapper
    {

        IActionRepository Action { get;}

        IRoleActionRepository RoleAction { get; }

        IRoleRepository Role { get;  }

        IUserRoleRepository UserRole { get;  }

        IUserTokenRepository UserToken { get; }

        IAppUserRepository AppUser { get; }

        void Save();

        Task SaveAsync();
    }
}
