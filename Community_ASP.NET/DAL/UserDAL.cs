using Community_ASP.NET.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Community_ASP.NET.DAL
{
    public static class UserDAL
    {
        public static void AddUserToDB(User userToDB)
        {
            using (var db = new CommunityContext())
            {
                db.Add(userToDB);
                db.SaveChanges();
            }
        }

        public static IEnumerable<User> GetUsers()
        {
            using (var db = new CommunityContext())
            {
                var users = db.Users
                    .Include(u => u.UserGroups)
                        .ThenInclude(g => g.Group)
                    .OrderBy(u => u.Id);
                return users.ToArray();
            }
        }

        public static User GetUser(int id)
        {
            using (var db = new CommunityContext())
            {
                var user = db.Users
                    .Include(u => u.UserGroups)
                        .ThenInclude(g => g.Group)
                    .First(u => u.Id == id);
                return user;
            }
        }

        public static bool UpdateUser(User updatedUser)
        {
            try
            {
                using (var db = new CommunityContext())
                {
                    db.Update(updatedUser);
                    db.SaveChanges();
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }

        public static bool DeleteUser(User deletedUser)
        {
            try
            {
                using (var db = new CommunityContext())
                {
                    db.Remove(deletedUser);
                    db.SaveChanges();
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }
    }
}
