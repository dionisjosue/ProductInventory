using System;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace SecurityDomain.Models
{
    public class UserClaim : IdentityUserClaim<Guid>
    {
        public UserClaim()
        {

        }

        [ForeignKey("User")]
        public override Guid UserId { get => base.UserId; set => base.UserId = value; }

        public virtual AppUser User { get; set; }

        [ForeignKey("Action")]
        public Guid ActionId { get; set; }

        public Action Action { get; set; }

    }
}

