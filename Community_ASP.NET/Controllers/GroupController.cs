using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Community_ASP.NET.Models;
using Community_ASP.NET.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Community_ASP.NET.Controllers
{
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
                return View(GroupBL.GetGroups());
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
            System.Diagnostics.Debug.WriteLine("in get create");
            return View();
        }


        // POST: WriteController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind("Name")] GroupInfo group)
        {
            System.Diagnostics.Debug.WriteLine("in post create");
            try
            {
                System.Diagnostics.Debug.WriteLine("Before modelState valid");
                if (ModelState.IsValid)                    {
                    GroupBL.AddGroup(group);
                      
                    //Display confirmation that a message was sent. To who and when
                    TempData["custdetails"] = string.Format("The group  \"{0}\"was created, {1}", group.Name, DateTime.Now.ToString("HH:mm MM/dd/yyyy"));
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


        // GET: GroupController/join
        public ActionResult Join(int id)
        {
            try
            {
                if (id == 0)
                {
                    return NotFound();
                }
                //det isRead in message
                var group = GroupBL.GetGroup(id);
                System.Diagnostics.Debug.WriteLine(group.Id);
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
            System.Diagnostics.Debug.WriteLine("in post create");
            try
            {
                //Check if theyre already apart of the group
                System.Diagnostics.Debug.WriteLine("Before modelState valid");
                if (ModelState.IsValid)
                {
                    var userGroup = new UserGroup();
                    userGroup.GroupId = group.Id;
                    userGroup.UserId = _userManager.GetUserId(User);
                    UserGroupBL.AddUserGroup(userGroup);


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



    


    }
}
