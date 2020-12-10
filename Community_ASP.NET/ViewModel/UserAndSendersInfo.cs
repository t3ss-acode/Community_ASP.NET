using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Community_ASP.NET.ViewModel
{
    public class UserAndSendersInfo
    {
        public UserInfo user { get; set; } 
        public IEnumerable<Sender> senders { get; set; }

        public class Sender
        {
            public string ExtraInfo { get; set; }
            public string SenderId { get; set; }
        }
    }
}
