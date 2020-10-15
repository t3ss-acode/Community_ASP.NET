using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Community_ASP.NET.ViewModel
{
    public class GroupInfo
    {
        /*
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<UserGroup> UserGroups { get; set; } = new Collection<UserGroup>();
        public ICollection<Message> Messages { get; set; } = new Collection<Message>();
         */
        public GroupInfo()
        {

        }
        public GroupInfo(string name)
        {
            Name = name;
        }

        public string Name { get; set; }

    }
}
