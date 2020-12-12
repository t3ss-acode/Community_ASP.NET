using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Community_ASP.NET.Models;
using Community_ASP.NET.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Community_ASP.NET.Controllers
{
    [Authorize]
    public class GroupController : Controller
    {
        private readonly UserManager<Community_ASPNETUser> _userManager;

        public GroupController(UserManager<Community_ASPNETUser> userManager)
        {
            _userManager = userManager;
        }

        // GET: GroupController index page. Show all groups and the ones you can join (not already apart of)
        public ActionResult Index()
        {
            try
            {
                return View(GroupBL.GetGroupsToDisplay(_userManager.GetUserId(User)));
            }
            catch (Exception e)
            {
                TempData["sErrMsg"] = e.Message;
                return View();
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
        public ActionResult Create([Bind("Name")] GroupInfo group)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    GroupBL.AddGroup(group);
                      
                    //Display confirmation that a group was created and when
                    TempData["custdetails"] = string.Format("The group  \"{0}\"was created, {1}", group.Name, DateTime.Now.ToString("HH:mm MM/dd/yyyy"));
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

            return RedirectToAction(nameof(Index));
        }


        // GET: GroupController/join
        public ActionResult Join(int id)
        {
            try
            {
                if (id == 0)
                {
                    return NotFound();
                }
                var group = GroupBL.GetGroup(id);
                
                return View(group);
            }
            catch
            {
                return Redirect("~/");
            }
        }


        // POST: WriteController/Create
        [HttpPost, ActionName("Join")]
        [ValidateAntiForgeryToken]
        public ActionResult JoinConfirm([Bind("Id")] GroupInfo group)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var userGroup = new UserGroup();
                    userGroup.GroupId = group.Id;
                    userGroup.UserId = _userManager.GetUserId(User);
                    UserGroupBL.AddUserGroup(userGroup);

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

            return RedirectToAction(nameof(Index));
        }

        // GET: GroupController/join
        public ActionResult Leave(int id)
        {
            try
            {
                if (id == 0)
                {
                    return NotFound();
                }
                var group = GroupBL.GetGroup(id);

                return View(group);
            }
            catch
            {
                return Redirect("~/");
            }
        }


        // POST: WriteController/Create
        [HttpPost, ActionName("Leave")]
        [ValidateAntiForgeryToken]
        public ActionResult LeaveConfirm([Bind("Id")] GroupInfo group)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var userGroup = new UserGroup();
                    userGroup.GroupId = group.Id;
                    userGroup.UserId = _userManager.GetUserId(User);
                    UserGroupBL.RemoveUserGroup(userGroup);

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

            return RedirectToAction(nameof(Index));
        }

        public PartialViewResult ShowError(string sErrorMessage)
        {
            return PartialView("ErrorMessageView");
        }
    }
}
