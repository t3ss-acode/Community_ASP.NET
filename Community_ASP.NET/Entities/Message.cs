using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Community_ASP.NET.Entities
{
    [Table("Message")]
    public class Message
    {
        [Key]
        public int Id { get; set; }
        public int SenderId { get; set; }
        public int ReciverId { get; set; }
        public int GroupId { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        [Timestamp]
        public DateTime Timestamp { get; set; }

        [ForeignKey("SenderId")]
        [InverseProperty("Messages")]
        public User Sender { get; set; }
        [ForeignKey("ReciverId")]
        public User Reciver { get; set; }
        [ForeignKey("GroupId")]
        public Group ReciverGroup { get; set; }
    }
}
