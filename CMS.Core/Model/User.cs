using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CMS.Core.Model
{
    public class User : IdentityUser
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public string NationalCode { get; set; }
        public DateTime? BirthDate { get; set; }
        public string Details { get; set; }
        public byte[] Image { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<User> manager)
        {
            var userIdentity = await manager
                .CreateIdentityAsync(this, DefaultAuthenticationTypes.ExternalBearer);
            // Add custom user claims here
            return userIdentity;
        }
    }
}
