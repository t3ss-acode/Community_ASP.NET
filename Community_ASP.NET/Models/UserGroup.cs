﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Community_ASP.NET.Models
{
    
    public class UserGroup
    {
        public string UserId { get; set; }
        public Community_ASPNETUser User { get; set; }
        public int GroupId { get; set; }
        public Group Group { get; set; }
    }
}
