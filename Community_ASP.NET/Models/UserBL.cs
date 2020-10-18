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
            
            Community_ASPNETUser user = UserDAL.GetUser(userId);

            var userInfo = new UserInfo();
            userInfo.Name = user.UserName + "name";
            userInfo.Email = user.Email;
            userInfo.LastLogin = DateTime.Now;  //todo: Change to proper timestamp
            userInfo.NrOfLoginsLastMonth = numberOfLogins(user);   //method for loginlog
            userInfo.NrOfUnreadMessages = numberOfUnreadMsg(user);   //method for messages
            userInfo.NrOfDeletedMessages = user.numberOfDeletedMessages;   //variable in user

            return userInfo;
        }

        public static void UpdateUser(Community_ASPNETUser user)
        {
            UserDAL.UpdateUser(user);
        }

        public static void RemoveUser(Community_ASPNETUser user)
        {
            UserDAL.DeleteUser(user);
        }

        public static void LogLogin(Community_ASPNETUser user)
        {
            user.LoginLogs.Add(new LoginLog { User = user, UserId = user.Id });
            UpdateUser(user);
        }

        private static int numberOfLogins(Community_ASPNETUser user)
        {
            int logins = 0;
            foreach (var l in user.LoginLogs)
                if (Community_ASPNETUser.convertToDateTime(l.RowVersion).Month == DateTime.Now.Month)
                    logins++;
            return logins;
        } 

        private static int numberOfUnreadMsg(Community_ASPNETUser user)
        {
            int unread = 0;
            foreach (var m in user.Messages)
                if (m.IsRead == false)
                    unread++;
            return unread;
        }
    }
}
