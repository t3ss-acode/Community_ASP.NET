using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Community_ASP.NET.Models
{
    public class Group
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<UserGroup> UserGroups { get; set; } = new Collection<UserGroup>();
        public ICollection<Message> Messages { get; set; } = new Collection<Message>();
    }
}
