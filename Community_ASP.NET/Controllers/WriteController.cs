using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Community_ASP.NET.Models;
using Community_ASP.NET.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

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

        [HttpPost]
        public JsonResult AutoComplete(string prefix)
        {
            try
            {
                List<UserInfo> usrList = (List<UserInfo>)UserBL.GetUsers();
                List<GroupInfo> grpList = (List<GroupInfo>)GroupBL.GetGroups();
                prefix = prefix.ToLower();
                var reciverUsrList = (from N in usrList
                                   where N.Email.ToLower().StartsWith(prefix)
                                   select new { Recipient = N.Email });
                var reciverGrpList = (from N in grpList
                                      where N.Name.ToLower().StartsWith(prefix)
                                      select new { Recipient = N.Name });
                var reciverList = reciverUsrList.Concat(reciverGrpList);
                return Json(reciverList);
            }
            catch (Exception e)
            {
                TempData["sErrMsg"] = e.Message;
                return Json("Something unintended happend, try again.");
                //return Redirect("~/");
            }
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
        public ActionResult Create([Bind("MessageInfo")] MessageViewModel mvm)
        {
            System.Diagnostics.Debug.WriteLine("in post create");
            try
            {
                var message = mvm.MessageInfo;
                message.SenderId = _userManager.GetUserId(User);
                try
                {
                    System.Diagnostics.Debug.WriteLine("Before modelState valid");
                    if (ModelState.IsValid)
                    {
                        MessageBL.AddMessage(message);
                        //Display confirmation that a message was sent. To who and when
                        TempData["custdetails"] = string.Format("Message sent to \"{0}\", {1}", message.ReceiverId, DateTime.Now.ToString("HH:mm MM/dd/yyyy"));
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
