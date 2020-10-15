using Community_ASP.NET.Areas.Identity.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Community_ASP.NET.Models
{
    public class Message
    {
        [Key]
        public int Id { get; set; }
        public string SenderId { get; set; }
        public string ReciverId { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public bool IsRead { get; set; }
        public bool IsRemoved { get; set; }
        [Timestamp]
        public byte[] RowVersion { get; set; }

        [ForeignKey("SenderId")]
        [InverseProperty("Messages")]
        public Community_ASPNETUser Sender { get; set; }
        [ForeignKey("ReciverId")]
        public Community_ASPNETUser Reciver { get; set; }
    }
}
