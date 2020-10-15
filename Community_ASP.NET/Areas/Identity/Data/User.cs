using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Community_ASP.NET.Models;
using Microsoft.AspNetCore.Identity;

namespace Community_ASP.NET.Areas.Identity.Data
{
    // Add profile data for application users by adding properties to the User class
    public class User : IdentityUser
    {
        public ICollection<UserGroup> UserGroups { get; set; } = new Collection<UserGroup>();
        public ICollection<Message> Messages { get; set; } = new Collection<Message>();
    }
}
