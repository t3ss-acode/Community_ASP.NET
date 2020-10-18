using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Community_ASP.NET.Models;
using Microsoft.AspNetCore.Identity;

namespace Community_ASP.NET.Models
{
    // Add profile data for application users by adding properties to the Community_ASPNETUser class
    public class Community_ASPNETUser : IdentityUser
    {
        [Required]
        public int numberOfDeletedMessages { get; set; }
        public ICollection<UserGroup> UserGroups { get; set; } = new Collection<UserGroup>();
        public ICollection<Message> Messages { get; set; } = new Collection<Message>();
        public ICollection<LoginLog> LoginLogs { get; set; }
    }
}
