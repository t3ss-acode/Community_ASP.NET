using Community_ASP.NET.Data;
using Community_ASP.NET.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Community_ASP.NET.DAL
{
    public class UserGroupDAL
    {
        public static void AddUserGroupToDB(UserGroup userGroupToDB)
        {
            using (var db = new Community_ASPNETContext())
            {
                db.Add(userGroupToDB);
                db.SaveChanges();
            }
        }

        public static bool DeleteUserGroup(UserGroup deletedUserGroup)
        {
            using (var db = new Community_ASPNETContext())
            {
                db.Remove(deletedUserGroup);
                db.SaveChanges();
                return true;
            }
        }
    }
}
