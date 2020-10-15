using Community_ASP.NET.Areas.Identity.Data;
using Community_ASP.NET.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Community_ASP.NET.Models
{
    public class MessageBL
    {
        public static void AddMessage(Message message)
        {
            MessageDAL.AddMessageToDB(message);
        }

        public static IEnumerable<Message> GetMessages()
        {
            return MessageDAL.GetMessages();
        }

        public static IEnumerable<Message> GetMessage(Community_ASPNETUser user)
        {
            return MessageDAL.GetUserMessages(user.Id);
        }

        public static void UpdateMessage(Message message)
        {
            MessageDAL.UpdateMessage(message);
        }

        public static void RemoveMessage(Message message)
        {
            MessageDAL.DeleteMessage(message);
        }
    }
}
