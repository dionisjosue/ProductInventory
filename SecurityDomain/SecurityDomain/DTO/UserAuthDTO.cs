using System;
using SecurityDomain.Models;

namespace SecurityDomain.DTO
{
	public class UserAuthDTO
	{
        public UserAuthDTO()
        {
            BearerToken = string.Empty;
        }

        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string BearerToken { get; set; }
        public bool IsAuthenticated { get; set; }
        public string ProfilePhoto { get; set; }

    }
}

