using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Community_ASP.NET.Models;
using Community_ASP.NET.DAL;
using Community_ASP.NET.Areas.Identity.Data;

namespace Community_ASP.NET.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            //Add
            //var message = new Message { SenderId = 3, ReciverId = 4, Title = "Test", Body = "This is a test message." };
            //var messageG = new Message { SenderId = 3, ReciverId = 5, Title = "Test Group", Body = "This is a test group message." };
            //MessageDAL.AddMessageToDB(message);
            //MessageDAL.AddMessageToDB(messageG);

            //Get
            /*var allMessages = MessageDAL.GetMessages();
            var userMessages = MessageDAL.GetUserMessages("4");
            foreach (var m in allMessages)
                print(m);
            foreach (var m in userMessages)
                print(m);
            */
            //Update

            //Delete 

            return View();
        }

        private void print(Message m)
        {
            if(m.Reciver != null)
                Debug.WriteLine("Id: " + m.Id + " Sender Id: " + m.Sender.Id + " Reciver Id" + m.Reciver.Id + " Title: " + m.Title + " Body: " + m.Body);
           
        }

        private void print(User u)
        {
            Debug.WriteLine("Id: " + u.Id + " Username: " + u.UserName + " Password: " + u.PasswordHash + " Email: " + u.Email);
            if(u.UserGroups != null)
                foreach (var g in u.UserGroups)
                    Debug.WriteLine("GroupId: "+g.Group.Id+" GroupName: "+g.Group.Name);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
