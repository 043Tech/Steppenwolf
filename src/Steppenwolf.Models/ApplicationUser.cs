using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace Steppenwolf.Models
{
    public class ApplicationUser : IdentityUser
    {
        public virtual ICollection<IdentityRole> Roles { get; } = new List<IdentityRole>();
    }
}