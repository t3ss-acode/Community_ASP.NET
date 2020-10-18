using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Community_ASP.NET.ViewModel
{
    public class UserAndSendersInfo
    {
        public UserInfo user { get; set; } 
        public IEnumerable<UserInfo> senders { get; set; }
    }
}
