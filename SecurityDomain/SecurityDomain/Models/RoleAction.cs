using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace SecurityDomain.Models
{
	public class RoleAction : IdentityRoleClaim<Guid>
    {

        public RoleAction()
        {

        }

        [ForeignKey("Action")]
        public Guid ActionId { get; set; }

        public virtual Role Role { get; set; }

        public Action Action { get; set; }
    }
}

