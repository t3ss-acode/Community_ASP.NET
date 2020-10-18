using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Community_ASP.NET.Models;
using Community_ASP.NET.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Community_ASP.NET.Controllers
{
    [Authorize]
    public class WriteController : Controller
    {
        private readonly UserManager<Community_ASPNETUser> _userManager;

        public WriteController(UserManager<Community_ASPNETUser> userManager)
        {
            _userManager = userManager;
        }

        // GET: WriteController
        public ActionResult Index()
        {
            return View();
        }

        // GET: WriteController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: WriteController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind("Title, Body")] MessageInfo message)
        {
            try
            {
                //Add receiverId to Bind
                message.ReceiverId = "2ffc89d2-59fa-4c3f-8059-2e6b43a9289c";
                message.SenderId = _userManager.GetUserId(User);
                try
                {
                    if (ModelState.IsValid)
                    {
                        MessageBL.AddMessage(message);
                        return RedirectToAction(nameof(Index));
                    }
                }
                catch (DbUpdateException /* ex */)
                {
                    //Log the error (uncomment ex variable name and write a log.
                    ModelState.AddModelError("", "Unable to save changes. " +
                        "Try again, and if the problem persists " +
                        "see your system administrator.");
                }
                return View(message);
            }
            catch
            {
                return Redirect("~/");
            }

        }
    }
}
