using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Community_ASP.NET.ViewModel
{
    public class MessageInfo
    {

        public MessageInfo()
        {

        }
        public MessageInfo(string senderId, string receiverId, string title, string body, Boolean isRead, DateTime timestamp)
        {
            SenderId = senderId;
            ReceiverId = receiverId;
            Title = title;
            Body = body;
            IsRead = isRead;
            Timestamp = timestamp;
        }

        public string SenderId { get; set; }
        public string ReceiverId { get; set; }
        public string Title { get; set; }
        public string Body { get; set; } 
        public Boolean IsRead { get; set; } 
        public DateTime Timestamp { get; set; } 

    }
}
