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
                userInfos.Add(userToUserInfo(u));
            }
            return userInfos;
        }

        public static UserInfo GetUser(string userId)
        {
            return userToUserInfo(UserDAL.GetUser(userId));
        }

        internal static Community_ASPNETUser GetUserInternal(string userId)
        {
            return UserDAL.GetUser(userId);
        }

        public static String GetUserIdString(String email)
        {
            Community_ASPNETUser user = UserDAL.GetUserWithEmail(email);

            return user.Id;
        }

        public static Community_ASPNETUser GetUserWithEmail(string userEmail)
        {
            return UserDAL.GetUserWithEmail(userEmail);
        }

        public static void AddUserToGroup(UserInfo userInfo, GroupInfo groupInfo)
        {
            var user = GetUserWithEmail(userInfo.Email);
            var ug = new UserGroup();
            ug.GroupId = groupInfo.Id;
            ug.UserId = user.Id;
            UserGroupBL.AddUserGroup(ug);
        }

        public static void RemoveUserFromGroup(UserInfo userInfo, GroupInfo groupInfo)
        {
            var user = GetUserWithEmail(userInfo.Email);
            var ug = user.UserGroups.ToList().Find(ug => ug.GroupId == groupInfo.Id);
            UserGroupBL.RemoveUserGroup(ug);
            var group = GroupBL.GetGroup(groupInfo.Id);
            if (GroupBL.isEmpty(group))
                GroupBL.RemoveGroup(group);
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

        private static UserInfo userToUserInfo(Community_ASPNETUser user)
        {
            var userInfo = new UserInfo();
            userInfo.Name = user.name;
            userInfo.Email = user.Email;
            userInfo.LastLogin = latestLogin(user);
            userInfo.NrOfLoginsLastMonth = numberOfLogins(user);
            userInfo.TotalMessages = numberOfMsg(user);
            userInfo.NrOfUnreadMessages = numberOfUnreadMsg(user);
            userInfo.NrOfDeletedMessages = user.numberOfDeletedMessages;

            var groups = new List<GroupInfo>();
            foreach (var g in user.UserGroups)
            {
                var gi = new GroupInfo();
                gi.Id = g.Group.Id;
                gi.Name = g.Group.Name;
            }
            userInfo.Groups = groups;

            userInfo.NrOfReadMessages = userInfo.TotalMessages - userInfo.NrOfUnreadMessages;

            return userInfo;
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
