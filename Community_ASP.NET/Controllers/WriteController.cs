using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
//using System.Web.Mvc;
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
        //private List<SelectListItem> userlist;

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
                var reciverList = (from N in usrList
                                   where N.Email.StartsWith(prefix)
                                   select new { N.Email });
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
            return View();
        }

        // POST: WriteController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind("MessageInfo")] MessageViewModel mvm)
        {
            try
            {
                var message = mvm.MessageInfo;
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
            catch (Exception e)
            {
                TempData["sErrMsg"] = e.Message;
                return View();
                //return Redirect("~/");
            }

        }
    }
}
