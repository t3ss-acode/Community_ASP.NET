using Community_ASP.NET.Data;
using Community_ASP.NET.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Community_ASP.NET.DAL
{
    public static class UserDAL
    {
        public static void AddUserToDB(Community_ASPNETUser userToDB)
        {
            using (var db = new Community_ASPNETContext())
            {
                db.Add(userToDB);
                db.SaveChanges();
            }
        }

        public static IEnumerable<Community_ASPNETUser> GetUsers()
        {
            using (var db = new Community_ASPNETContext())
            {
                var users = db.Users
                    .Include(u => u.UserGroups)
                        .ThenInclude(g => g.Group)
                    .Include(u => u.LoginLogs)
                    .OrderBy(u => u.Id);
                return users.ToArray();
            }
        }

        public static Community_ASPNETUser GetUser(string userId)
        {
            using (var db = new Community_ASPNETContext())
            {
                var user = db.Users
                    .Include(u => u.UserGroups)
                        .ThenInclude(g => g.Group)
                    .Include(u => u.LoginLogs)
                    .Include(u => u.Messages)
                    .First(u => u.Id.Equals(userId));
                return user;
            }
        }

        public static Community_ASPNETUser GetUserWithEmail(string email)
        {
            using (var db = new Community_ASPNETContext())
            {
                var user = db.Users
                    .Include(u => u.UserGroups)
                        .ThenInclude(g => g.Group)
                    .Include(u => u.LoginLogs)
                    .First(u => u.Email.Equals(email));
                return user;
            }
        }

        public static bool UpdateUser(Community_ASPNETUser updatedUser)
        {
            using (var db = new Community_ASPNETContext())
            {
                db.Update(updatedUser);
                db.SaveChanges();
                return true;
            }
        }

        public static bool DeleteUser(Community_ASPNETUser deletedUser)
        {
            using (var db = new Community_ASPNETContext())
            {
                db.Remove(deletedUser);
                db.SaveChanges();
                return true;
            }
        }
    }
}
