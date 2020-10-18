using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Community_ASP.NET.Models
{
    public class LoginLog
    {
        [Key]
        public int Id { get; set; }
        public string UserId { get; set; }
        [Timestamp]
        public DateTime Timestamp { get; set; }
        public Community_ASPNETUser User { get; set; }
    }
}
