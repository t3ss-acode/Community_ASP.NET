using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Community_ASP.NET.Entities
{
    [Table("MessageStatus")]
    public class MessageStatus
    {
        [Key, Column(Order = 0)]
        [ForeignKey("Message")]
        public int MessageId { get; set; }
        [Key, Column(Order = 1)]
        [ForeignKey("User")]
        public int UserId { get; set; }
        public bool IsRead { get; set; }
        public bool IsRemoved { get; set; }
        [Timestamp]
        public DateTime Timestamp { get; set; }
        
        [ForeignKey("MessageId")]
        public virtual Message Message { get; set; }
        [ForeignKey("UserId")]
        public virtual User User { get; set; }
    }
}
