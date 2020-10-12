using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Community_ASP.NET.Entities
{
    public class MessageStatus
    {
        public Message Message { get; set; }
        public User User { get; set; }
        public bool IsRead { get; set; }
        public bool IsRemoved { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
