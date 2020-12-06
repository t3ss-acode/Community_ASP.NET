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

        public static IEnumerable<UserInfo> GetUsers()
        {
            IEnumerable<Community_ASPNETUser> users = UserDAL.GetUsers();
            List<UserInfo> userInfos = new List<UserInfo>();
            foreach (var u in users)
            {
                var userInfo = new UserInfo();
                userInfo.Name = u.name;
                userInfo.Email = u.Email;
                userInfo.LastLogin = latestLogin(u);
                userInfo.NrOfLoginsLastMonth = numberOfLogins(u);
                userInfo.TotalMessages = numberOfMsg(u);
                userInfo.NrOfUnreadMessages = numberOfUnreadMsg(u);
                userInfo.NrOfDeletedMessages = u.numberOfDeletedMessages;

                userInfo.NrOfReadMessages = userInfo.TotalMessages - userInfo.NrOfUnreadMessages;

                userInfos.Add(userInfo);
            }
            return userInfos;
        }

        public static UserInfo GetUser(string userId)
        {
            Community_ASPNETUser user = UserDAL.GetUser(userId);
            
            var userInfo = new UserInfo();
            userInfo.Name = user.name;
            userInfo.Email = user.Email;
            userInfo.LastLogin = latestLogin(user);
            userInfo.NrOfLoginsLastMonth = numberOfLogins(user);
            userInfo.TotalMessages = numberOfMsg(user);
            userInfo.NrOfUnreadMessages = numberOfUnreadMsg(user);
            userInfo.NrOfDeletedMessages = user.numberOfDeletedMessages;

            userInfo.NrOfReadMessages = userInfo.TotalMessages - userInfo.NrOfUnreadMessages;

            return userInfo;
        }



        public static String GetUserIdString(String email)
        {
            Community_ASPNETUser user = UserDAL.GetUserWithEmail(email);

            return user.Id;
        }

        /*public static UserInfo GetUserWithEmail(string userEmail)
        {
            Community_ASPNETUser user = UserDAL.GetUserWithEmail(userEmail);

            var userInfo = new UserInfo();
            userInfo.Name = user.name;
            userInfo.Email = user.Email;
            userInfo.LastLogin = latestLogin(user);
            userInfo.NrOfLoginsLastMonth = numberOfLogins(user);
            userInfo.TotalMessages = numberOfMsg(user);
            userInfo.NrOfUnreadMessages = numberOfUnreadMsg(user);
            userInfo.NrOfDeletedMessages = user.numberOfDeletedMessages;

            userInfo.NrOfReadMessages = userInfo.TotalMessages - userInfo.NrOfUnreadMessages;

            return userInfo;
        }*/

        public static Community_ASPNETUser GetUserWithEmail(string userEmail)
        {
            return UserDAL.GetUserWithEmail(userEmail);
        }

        public static void UpdateUser(Community_ASPNETUser user)
        {
            UserDAL.UpdateUser(user);
        }

        public static void RemoveUser(Community_ASPNETUser user)
        {
            UserDAL.DeleteUser(user);
        }

        public static void LogLogin(string userId)
        {
            var user = UserDAL.GetUser(userId);
            user.LoginLogs.Add(new LoginLog { User = user, UserId = user.Id });
            UpdateUser(user);
        }

        private static int numberOfLogins(Community_ASPNETUser user)
        {
            int logins = 0;
            foreach (var l in user.LoginLogs)
                if (l.Timestamp.Month == DateTime.Now.Month)
                    logins++;
            return logins;
        }

        private static int numberOfMsg(Community_ASPNETUser user)
        {
            return user.Messages.Count();
        }

        private static int numberOfUnreadMsg(Community_ASPNETUser user)
        {
            int unread = 0;
            foreach (var m in user.Messages)
                if (m.IsRead == false)
                    unread++;
            return unread;
        }

        private static DateTime latestLogin(Community_ASPNETUser user)
        {
            DateTime latest = user.LoginLogs.First().Timestamp;
            foreach (var time in user.LoginLogs)
            {
                DateTime check = time.Timestamp;
                if (DateTime.Compare(latest, check) < 0)
            latest = check;

            }

            return latest;
        }
    }
}
