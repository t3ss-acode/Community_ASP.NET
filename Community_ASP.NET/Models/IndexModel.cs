using Community_ASP.NET.Areas.Identity.Data;
using Community_ASP.NET.Data;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Community_ASP.NET.Models
{
    public class IndexModel
    {
        private readonly Community_ASPNETContext _context;

        public IndexModel(Community_ASPNETContext context)
        {
            _context = context;
        }

        public Community_ASPNETUser GetUser()
        {
            var user = _context.Users.Find("2ffc89d2-59fa-4c3f-8059-2e6b43a9289c"); // await _context.Users.Find(1);

            Console.WriteLine("\n\n\n\n\n\n", user.UserName);

            return user;
        }
    }
}
