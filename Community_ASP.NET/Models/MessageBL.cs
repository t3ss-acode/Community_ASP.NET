using Community_ASP.NET.DAL;
using Community_ASP.NET.ViewModel;
using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Community_ASP.NET.Models
{
    public class MessageBL
    {
        /// <summary>
        /// Checks if valid message and reciver
        /// </summary>
        /// <param name="messageInfo"></param>
        public static void AddMessage(MessageInfo messageInfo)
        {
            var messages = new List<Message>();
            var recivers = messageInfo.ReceiverId.Split("; ");
            var users = UserBL.GetUsers();
            var groups = GroupBL.GetGroupsInternal();

            foreach (var r in recivers)
            {
                if (r.Equals(""))
                    continue;
                ///Check if it's a group and it exists
                if (groups.ToList().Exists(ug => ug.UserGroups.ToList().Exists(g => g.Group.Name.Equals(r))))
                {
                    var currentG = groups.ToList().Find(g => g.Name.Equals(r));
                    foreach (var rg in currentG.UserGroups)
                    {
                        var reciver = rg.UserId;
                        if (messageInfo.SenderId.Equals(reciver))
                            continue;
                        if (messages.Exists(m => m.ReciverId.Equals(rg.UserId)))
                            continue;
                        var title = messageInfo.Title + " ["+rg.Group.Name+"]";
                        messages.Add(createMessage(messageInfo, reciver, title));
                    }
                }
                ///Checks if it's a user and it exits
                else if (users.ToList().Exists(u => u.Email.Equals(r)))
                {
                    var reciver = UserBL.GetUserWithEmail(r).Id;
                    if (messageInfo.SenderId.Equals(reciver))
                        continue;
                    if (messages.Exists(m => m.ReciverId.Equals(reciver)))
                        continue;
                    messages.Add(createMessage(messageInfo, reciver));
                }
                else
                {
                    continue;
                }
            }

            MessageDAL.AddMessageToDB(messages);
        }


        public static MessageInfo GetMessage(int id)
        {
            return createMessageInfo(MessageDAL.GetMessage(id));
        }

        public static IEnumerable<MessageInfo> GetMessages(string userId, string senderEmail)
        {
            var sender = UserBL.GetUserWithEmail(senderEmail);
            var messageList = MessageDAL.GetSenderMessages(userId, sender.Id);

            var messageInfoList = new List<MessageInfo>();

            foreach (Message m in messageList)
            {
                messageInfoList.Add(createMessageInfo(m));
            }

            return messageInfoList;
        }

        public static IEnumerable<UserInfo> GetUsersOfMessages(string id)
        {
            var messageList = MessageDAL.GetUserMessages(id);
            var userList = new List<Community_ASPNETUser>();

            foreach (Message m in messageList)
            {
                if (!userList.Contains(m.Sender))
                    userList.Add(m.Sender);
            }

            var userInfoList = new List<UserInfo>();
            foreach (Community_ASPNETUser u in userList)
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

        public static void RemoveMessage(int id)
        {
            var message = MessageDAL.GetMessage(id);
            MessageDAL.DeleteMessage(message);
        }

        private static Message createMessage(MessageInfo messageInfo, string reciver)
        {
            var message = new Message();
            message.Title = messageInfo.Title;
            message.Body = messageInfo.Body;
            message.SenderId = messageInfo.SenderId;
            message.ReciverId = reciver;
            message.IsRead = false;
            return message;
        }

        private static Message createMessage(MessageInfo messageInfo, string reciver, string title)
        {
            var message = new Message();
            message.Title = title;
            message.Body = messageInfo.Body;
            message.SenderId = messageInfo.SenderId;
            message.ReciverId = reciver;
            message.IsRead = false;
            return message;
        }

        private static MessageInfo createMessageInfo(Message m)
        {
            if (m.IsRead == false)
            {
                m.IsRead = true;
                MessageDAL.UpdateMessage(m);
            }
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
    }
}
