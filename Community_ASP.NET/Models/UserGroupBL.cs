using Community_ASP.NET.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Community_ASP.NET.Models
{
    public class UserGroupBL
    {
        public static void AddUserGroup(UserGroup newUserGroup)
        {
            UserGroupDAL.AddUserGroupToDB(newUserGroup);
        }

        public static void RemoveUserGroup(UserGroup userGroupToBeDeleted)
        {
            UserGroupDAL.DeleteUserGroup(userGroupToBeDeleted);
        }
    }
}
