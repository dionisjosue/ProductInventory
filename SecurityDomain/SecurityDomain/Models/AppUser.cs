

using Microsoft.AspNetCore.Identity;

namespace SecurityDomain.Models
{
    public class AppUser: IdentityUser<Guid>
    {
        public AppUser()
        {
        }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsActive { get; set; }
        public bool IsSuperAdmin { get; set; }
        public string? HousePhone { get; set; }
        public string? Address { get; set; }
        public string? ZipCode { get; set; }
        public string? MiddleName { get; set; }
        public DateTime? CreatedDate { get; set; }
        public Guid? CreatedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public Guid? ModifiedBy { get; set; }
        public DateTime? DeletedDate { get; set; }
        public Guid? DeletedBy { get; set; }


        public virtual ICollection<UserToken> UserTokens { get; set; }
        public virtual ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();





    }
}

