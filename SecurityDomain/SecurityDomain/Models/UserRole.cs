using Microsoft.AspNetCore.Identity;

namespace SecurityDomain.Models
{
    public class UserRole : IdentityUserRole<Guid>
    {
        public UserRole()
        {
            
        }

        public virtual AppUser User { get; set; }

        public virtual Role Role { get; set; }


    }
}

