    using Community_ASP.NET.Data;
using Community_ASP.NET.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Community_ASP.NET.Areas.Identity.Data;

namespace Community_ASP.NET.DAL
{
    public class MessageDAL
    {
        public static void AddMessageToDB(Message messageToDB)
        {
            using (var db = new Community_ASPNETContext())
            {
                db.Add(messageToDB);
                db.SaveChanges();
            }
        }

        public static Message GetMessage(int id)
        {
            using (var db = new Community_ASPNETContext())
            {
                var message = db.Messages
                    .Include(m => m.Sender)
                    .Include(m => m.Reciver)
                    .First(m => m.Id.Equals(id));
                return message;
            }
        }

        public static IEnumerable<Message> GetMessages()
        {
            using (var db = new Community_ASPNETContext())
            {
                var messages = db.Messages
                    .Include(m => m.Sender)
                    .Include(m => m.Reciver)
                    .OrderBy(m => m.Id);
                return messages.ToArray();
            }
        }

        public static IEnumerable<Message> GetUserMessages(string userId)
        {
            using (var db = new Community_ASPNETContext())
            {
                var message = db.Messages
                    .Include(m => m.Sender)
                    .Include(m => m.Reciver)
                    .Where(m => m.ReciverId.Equals(userId));
                return message.ToArray();
            }
        }

        public static IEnumerable<Message> GetSenderMessages(string userId, string senderId)
        {
            using (var db = new Community_ASPNETContext())
            {
                var message = db.Messages
                    .Include(m => m.Sender)
                    .Include(m => m.Reciver)
                    .Where(m => m.ReciverId.Equals(userId) & m.SenderId.Equals(senderId));
                return message.ToArray();
            }
        }

        public static bool UpdateMessage(Message updatedMessage)
        {
            try
            {
                using (var db = new Community_ASPNETContext())
                {
                    db.Update(updatedMessage);
                    db.SaveChanges();
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }

        public static bool DeleteMessage(Message deletedMessage)
        {
            try
            {
                using (var db = new Community_ASPNETContext())
                {
                    db.Remove(deletedMessage);
                    foreach(var u in db.Users)
                        if(u.Id == deletedMessage.ReciverId)
                        {
                            db.Remove(deletedMessage);
                            u.numberOfDeletedMessages++;
                            db.Update(u);
                            break;
                        }
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
