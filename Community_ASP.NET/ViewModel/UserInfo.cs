using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Community_ASP.NET.ViewModel
{
    public class UserInfo
    {
        public UserInfo()
        {

        }

        public UserInfo(string name, string email, DateTime lastLogin, int nrOflogins, int totalMessages, int nrOfUnread, int nrOfDeleted)
        {
            Name = name;
            Email = email;
            LastLogin = lastLogin;
            NrOfLoginsLastMonth = nrOflogins;
            TotalMessages = totalMessages;
            NrOfReadMessages = totalMessages - nrOfUnread;
            Debug.WriteLine(NrOfReadMessages);
            Debug.WriteLine(totalMessages);
            Debug.WriteLine(nrOfUnread);
            Debug.WriteLine("\n\n\n");

            NrOfUnreadMessages = nrOfUnread;
            NrOfDeletedMessages = nrOfDeleted;
        }

        public String Name { get; set; }
        public String Email { get; set; }
        public DateTime LastLogin { get; set; }
        public int NrOfLoginsLastMonth { get; set; }
        public int TotalMessages { get; set; }
        public int NrOfReadMessages { get; set; }
        public int NrOfUnreadMessages { get; set; }
        public int NrOfDeletedMessages { get; set; }
        public IEnumerable<GroupInfo> Groups { get; set; }
    }
}
