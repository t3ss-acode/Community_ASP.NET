﻿    using Community_ASP.NET.Data;
using Community_ASP.NET.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Community_ASP.NET.DAL
{
    public class MessageDAL
    {
        public static void AddMessageToDB(IEnumerable<Message> messageToDB)
        {
            using (var db = new Community_ASPNETContext())
            {
                foreach (var m in messageToDB)
                {
                    db.Add(m);
                }
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
                    .Include(m => m.Group)
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
                    .Include(m => m.Group)
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
                    .Include(m => m.Group)
                    .Where(m => m.ReciverId.Equals(userId));
                return message.ToArray();
            }
        }

        public static IEnumerable<Message> GetGroupMessages(string groupId)
        {
            using (var db = new Community_ASPNETContext())
            {
                var message = db.Messages
                    .Include(m => m.Sender)
                    .Include(m => m.Reciver)
                    .Include(m => m.Group)
                    .Where(m => m.GroupId.Equals(groupId));
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
                    .Include(m => m.Group)
                    .Where(m => m.ReciverId.Equals(userId) & m.SenderId.Equals(senderId));
                return message.ToArray();
            }
        }

        public static IEnumerable<Message> GetSenderGroupMessages(string userId, int senderId)
        {
            using (var db = new Community_ASPNETContext())
            {
                var message = db.Messages
                    .Include(m => m.Sender)
                    .Include(m => m.Reciver)
                    .Include(m => m.Group)
                    .Where(m => m.ReciverId.Equals(userId) & m.GroupId.Equals(senderId));
                return message.ToArray();
            }
        }

        public static bool UpdateMessage(Message updatedMessage)
        {
            using (var db = new Community_ASPNETContext())
            {
                db.Update(updatedMessage);
                db.SaveChanges();
                return true;
            }
        }

        public static bool DeleteMessage(Message deletedMessage)
        {
            using (var db = new Community_ASPNETContext())
            {
                db.Remove(deletedMessage);
                foreach (var u in db.Users)
                    if (u.Id == deletedMessage.ReciverId)
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
    }
}
