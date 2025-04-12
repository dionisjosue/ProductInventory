


using SharedItems.JWT;
using SecurityDomain.Repositories;
using SecurityInfrastructure.Database;

namespace SecurityInfrastructure.Repositories
{
    public class RepositoryWrapper : IRepositoryWrapper
    {
        private SecurityDbContext _context { get; set; }

        private JwtSettings _jwtSettings { get; set; }

        public RepositoryWrapper(SecurityDbContext context,JwtSettings jwtSettings)
        {
            _jwtSettings = jwtSettings;
            _context = context;
        }

        private IActionRepository _action;
        private IRoleActionRepository _roleAction;
        private IRoleRepository _role;
        private IUserRoleRepository _userRole;
        private IUserTokenRepository _userToken;
        private IAppUserRepository _appUser;





        public IActionRepository Action {
            get
            {
                if(_action == null)
                {
                    _action = new ActionRepository(_context);
                }
                return _action;
            }
        }

        public IRoleActionRepository RoleAction
        {
            get
            {
                if(_roleAction == null)
                {
                    _roleAction = new RoleActionRepository(_context);
                }
                return _roleAction;
            }
        }
        public IRoleRepository Role
        {
            get
            {
                if(_role == null)
                {
                    _role = new RoleRepository(_context);
                }
                return _role;
            }
        }
        public IUserRoleRepository UserRole
        {
            get
            {
                if(_userRole == null)
                {
                    _userRole = new UserRoleRepository(_context);
                }
                return _userRole;
            }
        }
        public IUserTokenRepository UserToken
        {
            get
            {
                if(_userToken == null)
                {
                    _userToken = new UserTokenRepository(_context);
                }
                return _userToken;
            }
        }
        public IAppUserRepository AppUser
        {
            get
            {
                if(_appUser == null)
                {
                    _appUser = new AppUserRepository(_context, _jwtSettings);
                }
                return _appUser;
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
