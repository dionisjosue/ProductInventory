using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace SecurityDomain.Models
{
	public class Role : IdentityRole<Guid>
    {

        public Role()
        {

        }

        public string Description { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreatedDate { get; set; }
        public Guid? CreatedBy { get; set; }
        public DateTime ModifiedDate { get; set; }
        public Guid ModifiedBy { get; set; }
        public DateTime? DeletedDate { get; set; }
        public Guid? DeletedBy { get; set; }
        [ForeignKey("CreatedBy")]
        public AppUser CreatedByUser { get; set; }
        [ForeignKey("ModifiedBy")]
        public AppUser ModifiedByUser { get; set; }
        [ForeignKey("DeletedBy")]
        public AppUser DeletedByUser { get; set; }
        public bool IsSuperRole { get; set; } = false;
        public virtual ICollection<UserRole> UserRoles { get; set; }
        public virtual ICollection<RoleAction> RoleClaims { get; set; }
    }
}

