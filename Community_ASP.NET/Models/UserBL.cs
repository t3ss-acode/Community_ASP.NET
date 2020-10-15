using Community_ASP.NET.Areas.Identity.Data;
using Community_ASP.NET.DAL;
using Community_ASP.NET.ViewModel;
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

        public static UserInfo GetUser(string userId)
        {
            /*
            Community_ASPNETUser user = UserDAL.GetUser(userId);

            var userInfo = new UserInfo();
            userInfo.Name = user.UserName + "name";
            userInfo.Email = user.Email;
            userInfo.LastLogin = DateTime.Now;  //todo: Change to proper timestamp
            userInfo.NrOfLoginsLastMonth = 3;   //method for loginlog
            userInfo.NrOfUnreadMessages = 40;   //method for messages
            userInfo.NrOfDeletedMessages = 5;   //variable in user

            return userInfo;
            */
            return new UserInfo("test name", "test@kth.se", DateTime.Now, 4, 5, 20);
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
