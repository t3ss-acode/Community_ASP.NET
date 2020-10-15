using Community_ASP.NET.Areas.Identity.Data;
using Community_ASP.NET.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Community_ASP.NET.Models
{
    public static class UserBL
    {
        public static void AddUser(Community_ASPNETUser user)
        {
            UserDAL.AddUserToDB(user);
        }

        public static IEnumerable<Community_ASPNETUser> GetUsers()
        {
            return UserDAL.GetUsers();
        }

        public static Community_ASPNETUser GetUser(Community_ASPNETUser user)
        {
            return UserDAL.GetUser(user.Id);
        }

        public static void UpdateUser(Community_ASPNETUser user)
        {
            UserDAL.UpdateUser(user);
        }

        public static void RemoveUser(Community_ASPNETUser user)
        {
            UserDAL.DeleteUser(user);
        }
    }
}
