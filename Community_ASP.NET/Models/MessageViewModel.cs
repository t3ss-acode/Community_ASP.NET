using Community_ASP.NET.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Community_ASP.NET.Models
{
    public class MessageViewModel
    {
        public UserInfo User { get; set; }
        public MessageInfo MessageInfo { get; set; }
    }
}
