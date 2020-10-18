using Community_ASP.NET.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Community_ASP.NET.Models
{
    public class GroupBL
    {
        public static void AddGroup(Group group)
        {
            var groups = GetGroups();
            foreach (var g in groups)
                if (g.Name.Equals(group.Name))
                    return;

            GroupDAL.AddGroupToDB(group);
        }

        public static IEnumerable<Group> GetGroups()
        {
            return GroupDAL.GetGroups();
        }

        public static Group GetGroup(Group group)
        {
            return GroupDAL.GetGroup(group.Id);
        }

        public static void UpdateGroup(Group group)
        {
            GroupDAL.UpdateGroup(group);
        }

        public static void RemoveGroup(Group group)
        {
            GroupDAL.DeleteGroup(group);
        }
    }
}
