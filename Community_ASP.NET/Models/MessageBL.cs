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
        public static Boolean AddMessage(MessageInfo messageInfo)
        {
            var messages = new List<Message>();
            var recivers = messageInfo.ReceiverId.Split("; ");
            var users = UserBL.GetUsers();
            var groups = GroupBL.GetGroupsInternal();
            recivers = recivers.Distinct().ToArray();

            foreach (var r in recivers)
            {
                //recivers = recivers.Distinct().ToList();
                if (r.Equals(""))
                    continue;
                ///Check if it's a group and it exists
                if (groups.ToList().Exists(ug => ug.UserGroups.ToList().Exists(g => g.Group.Name.Equals(r))))
                {
                    var currentG = groups.ToList().Find(g => g.Name.Equals(r));
                    var sender = UserBL.GetUser(messageInfo.SenderId);
                    foreach (var rg in currentG.UserGroups)
                    {
                        var reciver = rg.UserId;
                        if (messageInfo.SenderId.Equals(reciver)) 
                            continue;
                        //if (messages.Exists(m => m.ReciverId.Equals(rg.UserId)))
                            //continue;
                        
                        var title = messageInfo.Title + " [" + sender.Email + "]";
                        messages.Add(createMessage(messageInfo, reciver, title,currentG));
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

            //If threre are no recipients, return false
            if(messages.Count == 0)
                return false;
            
            MessageDAL.AddMessageToDB(messages);
            return true;
        }


        public static MessageInfo GetMessage(int id)
        {
            var m = MessageDAL.GetMessage(id);
            if (m.IsRead == false)
            {
                m.IsRead = true;
                MessageDAL.UpdateMessage(m);
            }
            return createMessageInfo(m);
        }

        public static IEnumerable<MessageInfo> GetMessages(string userId, string senderId)
        {
            IEnumerable<Message> messageList;
            if (!UserBL.GetUsers().ToList().Exists(u => u.Email == senderId))
            {
                var sender = GroupBL.getGroupInternal(senderId);
                messageList = MessageDAL.GetSenderGroupMessages(userId,sender.Id);
            }
            else 
            {
                var sender = UserBL.GetUserWithEmail(senderId);
                messageList = MessageDAL.GetSenderMessages(userId, sender.Id).ToList().FindAll(m => m.Group == null);
            }

            System.Diagnostics.Debug.WriteLine("In MessageBL getMessages");
            foreach (var m in messageList.ToList())
            {
                System.Diagnostics.Debug.WriteLine(m.IsRead);
            }

            var messageInfoList = new List<MessageInfo>();

            foreach (Message m in messageList)
            {
                messageInfoList.Add(createMessageInfo(m));
            }
            System.Diagnostics.Debug.WriteLine("In MessageBL getMessages message info");
            foreach (var m in messageInfoList)
            {
                System.Diagnostics.Debug.WriteLine(m.IsRead);
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

        public static IEnumerable<UserAndSendersInfo.Sender> GetSendersOfMessages(string id)
        {
            var userList = MessageDAL.GetUserMessages(id);
            var user = UserBL.GetUserInternal(id);
            var grpList = MessageDAL.GetGroupMessages(id);
            var senderList = new List<UserAndSendersInfo.Sender>();

            foreach(var i in userList)
            {
                if (i.Group == null)
                {
                    if (!senderList.Exists(s => s.SenderId.Equals(i.Sender.Email)))
                    {
                        var sender = new UserAndSendersInfo.Sender();
                        sender.ExtraInfo = i.Sender.name;
                        sender.SenderId = i.Sender.Email;
                        senderList.Add(sender);
                    }
                }
                else if (!senderList.Exists(s => s.SenderId.Equals(i.Group.Name)))
                {
                    var sender = new UserAndSendersInfo.Sender();
                    sender.ExtraInfo = i.Sender.Email;
                    sender.SenderId = i.Group.Name;
                    senderList.Add(sender);
                }
            }
            return senderList;
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

        private static Message createMessage(MessageInfo messageInfo, string reciver, string title, Group group)
        {
            var message = new Message();
            message.Title = title;
            message.Body = messageInfo.Body;
            message.SenderId = messageInfo.SenderId;
            message.ReciverId = reciver;
            message.IsRead = false;
            message.GroupId = group.Id;
            return message;
        }

        private static MessageInfo createMessageInfo(Message m)
        {
            var messageInfo = new MessageInfo();
            messageInfo.MessageId = m.Id;
            messageInfo.SenderId = m.GetSender();
            messageInfo.ReceiverId = m.ReciverId;
            messageInfo.Title = m.Title;
            messageInfo.Body = m.Body;
            messageInfo.IsRead = m.IsRead;
            messageInfo.Timestamp = m.Timestamp;
            return messageInfo;
        }
    }
}
