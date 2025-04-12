using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace SecurityDomain.Models
{
    public class UserLogin : IdentityUserLogin<Guid>
    {
        public UserLogin()
        {

        }


        public virtual AppUser User { get; set; }
    }
}

