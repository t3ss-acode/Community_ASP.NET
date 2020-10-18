using Community_ASP.NET.DAL;
using Community_ASP.NET.ViewModel;
using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Threading.Tasks;

namespace Community_ASP.NET.Models
{
    public class MessageBL
    {
        public static void AddMessage(MessageInfo messageInfo)
        {
            var message = new Message();
            message.Title = messageInfo.Title;
            message.Body = messageInfo.Body;
            message.SenderId = messageInfo.SenderId;
            message.ReciverId = messageInfo.ReceiverId;
            message.IsRead = false;

            MessageDAL.AddMessageToDB(message);
        }

        public static MessageInfo GetMessage(int id)
        {
            var m = MessageDAL.GetMessage(id);

            var messageInfo = new MessageInfo();
            messageInfo.MessageId = m.Id;
            messageInfo.SenderId = m.SenderId;
            messageInfo.ReceiverId = m.ReciverId;
            messageInfo.Title = m.Title;
            messageInfo.Body = m.Body;
            messageInfo.IsRead = m.IsRead;
            messageInfo.Timestamp = m.Timestamp;

            return messageInfo;
        }

        public static IEnumerable<MessageInfo> GetMessages(string userId, string senderEmail)
        {
            var sender = UserDAL.GetUserWithEmail(senderEmail);
            var messageList = MessageDAL.GetSenderMessages(userId, sender.Id);

            var messageInfoList = new List<MessageInfo>();

            foreach (Message m in messageList)
            {
                var messageInfo = new MessageInfo();
                messageInfo.MessageId = m.Id;
                messageInfo.SenderId = m.SenderId;
                messageInfo.ReceiverId = m.ReciverId;
                messageInfo.Title = m.Title;
                messageInfo.Body = m.Body;
                messageInfo.IsRead = m.IsRead;
                messageInfo.Timestamp = m.Timestamp;

                messageInfoList.Add(messageInfo);
            }

            return messageInfoList;
        }

        public static IEnumerable<UserInfo> GetUsersOfMessages(string id)
        {
            var messageList = MessageDAL.GetUserMessages(id);
            var userList = new List<Community_ASPNETUser>();

            foreach(Message m in messageList)
            {
                if (!userList.Contains(m.Sender))
                    userList.Add(m.Sender);
            }

            var userInfoList = new List<UserInfo>();
            foreach(Community_ASPNETUser u in userList)
            {
                var userInfo = new UserInfo();
                userInfo.Name = u.name;
                userInfo.Email = u.Email;
                userInfoList.Add(userInfo);
            }

            return userInfoList;
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
