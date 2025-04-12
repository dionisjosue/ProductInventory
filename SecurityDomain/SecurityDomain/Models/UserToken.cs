using Microsoft.AspNetCore.Identity;

namespace SecurityDomain.Models
{
    public class UserToken : IdentityUserToken<Guid>
    {
        public UserToken()
        {
           
        }

        public virtual AppUser User { get; set; } = null;

    }
}

