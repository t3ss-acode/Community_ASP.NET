using Community_ASP.NET.Data;
using Community_ASP.NET.Models;
using Microsoft.AspNetCore.Mvc.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Community_ASP.NET.DAL
{
    public static class GroupDAL
    {
        public static void AddGroupToDB(Group groupToDb)
        {
            using (var db = new Community_ASPNETContext())
            {
                db.Add(groupToDb);
                db.SaveChanges();
            }
        }

        public static IEnumerable<Group> GetGroups()
        {
            using (var db = new Community_ASPNETContext())
            {
                var groups = db.Groups
                    .Include(g => g.Messages)
                    .Include(g => g.UserGroups)
                        .ThenInclude(u => u.User)
                    .OrderBy(g => g.Id);

                return groups.ToArray();
            }
        }

        public static Group GetGroup(int id)
        {
            using (var db = new Community_ASPNETContext())
            {
                var group = db.Groups
                    .Include(g => g.Messages)
                    .Include(g => g.UserGroups)
                        .ThenInclude(u => u.User)
                    .First(g => g.Id == id);
                return group;
            }
        }

        public static bool UpdateGroup(Group updatedGroup)
        {
            using (var db = new Community_ASPNETContext())
            {
                db.Update(updatedGroup);
                db.SaveChanges();
                return true;
            }
        }

        public static bool DeleteGroup(Group deletedGroup)
        {
            using (var db = new Community_ASPNETContext())
            {
                db.Remove(deletedGroup);
                db.SaveChanges();
                return true;
            }
        }
    }
}
