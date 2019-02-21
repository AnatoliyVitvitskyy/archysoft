using System;
using Microsoft.AspNetCore.Identity;

namespace Archysoft.D1.Data.Entities
{
    public class User : IdentityUser<Guid>
    {
        public UserProfile Profile { get; set; }
    }
}
