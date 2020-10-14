using Community_ASP.NET.Entities;
using Microsoft.AspNetCore.Mvc.DataAnnotations;
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
            using (var db = new CommunityContext())
            {
                db.Add(groupToDb);
                db.SaveChanges();
            }
        }

        public static IEnumerable<Group> GetGroups()
        {
            using (var db = new CommunityContext())
            {
                var groups = db.Groups
                    .OrderBy(g => g.Id);

                return groups.ToArray();
            }
        }

        public static Group GetGroup(int id)
        {
            using (var db = new CommunityContext())
            {
                var group = db.Groups
                    .First(g => g.Id == id);
                return group;
            }
        }

        public static bool UpdateGroup(Group updatedGroup)
        {
            try
            {
                using (var db = new CommunityContext())
                {
                    var group = db.Groups
                        .First(g => g.Id == updatedGroup.Id);
                    group.Name = updatedGroup.Name;
                    group.Message = updatedGroup.Message;
                    db.SaveChanges();
                    return true;
                }
            }
            catch
            {
                return false;
            }
            
        }

        public static bool DeleteGroup(Group deletedGroup)
        {
            try
            {
                using (var db = new CommunityContext())
                {
                    db.Remove(deletedGroup);
                    db.SaveChanges();
                    return true;
                }
            }catch
            {
                return false;
            }
        }
    }
}
