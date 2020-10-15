using Community_ASP.NET.Areas.Identity.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Community_ASP.NET.Models
{
    public class LoginLog
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        [Timestamp]
        public byte[] RowVersion { get; set; }
        public Community_ASPNETUser User { get; set; }
    }
}
