using Community_ASP.NET.DAL;
using Community_ASP.NET.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Community_ASP.NET.Models
{
    public class GroupBL
    {
        public static void AddGroup(GroupInfo groupInfo)
        {
            var groups = GetGroups();
            foreach (var g in groups)
                if (g.Name.Equals(groupInfo.Name))
                    return;
            var group = new Group();
            group.Name = groupInfo.Name;
            GroupDAL.AddGroupToDB(group);
        }

        public static IEnumerable<GroupInfo> GetGroups()
        {
            var grpList = GroupDAL.GetGroups();
            var groups = new List<GroupInfo>();
            foreach (var g in grpList)
            {
                var tmp = new GroupInfo();
                tmp.Name = g.Name;
                tmp.Id = g.Id;

                groups.Add(tmp);
            }
            return groups;
        }

        internal static IEnumerable<Group> GetGroupsInternal()
        {
            return GroupDAL.GetGroups();
        }

        public static GroupInfo GetGroup(int groupId)
        {
            var group = GroupDAL.GetGroup(groupId);
            var groupInfo = new GroupInfo();
            groupInfo.Id = group.Id;
            groupInfo.Name = group.Name;
            return groupInfo;
        }

        public static void UpdateGroup(GroupInfo groupInfo)
        {
            var grp = GetGroup(groupInfo);
            grp.Name = groupInfo.Name;
            GroupDAL.UpdateGroup(grp);
        }

        public static void RemoveGroup(GroupInfo group)
        {
            GroupDAL.DeleteGroup(GetGroup(group));
        }

        public static bool isEmpty(GroupInfo groupInfo)
        {
            if (GetGroup(groupInfo).UserGroups.Count <= 0)
                return true;
            return false;
        }

        private static Group GetGroup(GroupInfo groupInfo)
        {
            return GroupDAL.GetGroup(groupInfo.Id);
        } 
    }
}
