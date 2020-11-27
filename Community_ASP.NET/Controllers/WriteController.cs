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
using Microsoft.AspNetCore.Mvc.Rendering;
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
            var list = new List<SelectListItem>();
            foreach (var i in UserBL.GetUsers())
            {
                list.Add(new SelectListItem { Text = i.Email, Value = i.Email });
            }
            ViewBag.UserList = list;
            return View();
        }

        // GET: WriteController/Create
        public ActionResult Create()
        {
            System.Diagnostics.Debug.WriteLine("in get create");
            return View();
        }

        // POST: WriteController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(String UserList, [Bind("Title, Body")] MessageInfo message)
        {
            System.Diagnostics.Debug.WriteLine("in post create");
            try
            {
                message.ReceiverId = UserBL.GetUserIdString(UserList);
                message.SenderId = _userManager.GetUserId(User);
                try
                {
                    System.Diagnostics.Debug.WriteLine("Before modelState valid");
                    if (ModelState.IsValid)
                    {
                        MessageBL.AddMessage(message);
                        var receiverName = UserBL.GetUserWithEmail(UserList).Name;
                        //Display confirmation that a message was sent. To who and when
                        TempData["custdetails"] = string.Format("Message sent to {0}, {1}", receiverName, DateTime.Now.ToString("HH:mm MM/dd/yyyy"));
                        return RedirectToAction(nameof(Index));
                    }
                    System.Diagnostics.Debug.WriteLine("After modelState valid");
                }
                catch (DbUpdateException /* ex */)
                {
                    //Log the error (uncomment ex variable name and write a log.
                    ModelState.AddModelError("", "Unable to save changes. " +
                        "Try again, and if the problem persists " +
                        "see your system administrator.");
                }
                return RedirectToAction(nameof(Index));
            }
            catch (Exception e)
            {
                TempData["sErrMsg"] = e.Message;
                return RedirectToAction(nameof(Index));
                //return Redirect("~/");
            }

        }
    }
}
