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
        [Required]
        public string ReciverId { get; set; }
        [Required]
        [MaxLength(50)]
        public string Title { get; set; }
        [Required]
        public string Body { get; set; }
        [Required]
        public bool IsRead { get; set; }
        [Timestamp]
        public DateTime Timestamp { get; set; }

        [ForeignKey("SenderId")]
        [InverseProperty("Messages")]
        public Community_ASPNETUser Sender { get; set; }
        [ForeignKey("ReciverId")]
        public Community_ASPNETUser Reciver { get; set; }
    }
}
