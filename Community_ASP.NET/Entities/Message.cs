using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Community_ASP.NET.Entities
{
    public class Message
    {
        public int Id { get; set; }
        public User Sender { get; set; }
        public User Reciver { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public DateTime Timestamp { get; set; }

    }
}
