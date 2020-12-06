using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Community_ASP.NET.DAL;
using Community_ASP.NET.Models;
using Community_ASP.NET.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Community_ASP.NET.Controllers
{
    [Authorize]
    public class ReadController : Controller
    {
        private readonly UserManager<Community_ASPNETUser> _userManager;

        public ReadController(UserManager<Community_ASPNETUser> userManager)
        {
            _userManager = userManager;
        }


        // GET: ReadController Index page
        public ActionResult Index()
        {
            try
            {
                var userList = MessageBL.GetUsersOfMessages(_userManager.GetUserId(User));
                var user = UserBL.GetUser(_userManager.GetUserId(User));

                var userAndSenders = new UserAndSendersInfo();
                userAndSenders.user = user;
                userAndSenders.senders = userList;

                return View(userAndSenders);
            }
            catch (Exception e)
            {
                TempData["sErrMsg"] = e.Message;
                return View();
                //return Redirect("~/");
            }
        }

        // GET: ReadController Messages page
        public ActionResult Messages(string id)
        {
            try
            {
                if (id == null)
                {
                    return NotFound();
                }

                var messages = MessageBL.GetMessages(_userManager.GetUserId(User), id);

                if (messages == null)
                {
                    return NotFound();
                }

                return View(messages);
            }
            catch (Exception e)
            {
                TempData["sErrMsg"] = e.Message;
                return View();
                //return Redirect("~/");
            }

        }

        public ActionResult Message(int id)
        {
            try
            {
                if (id == 0)
                {
                    return NotFound();
                }
                var message = MessageBL.GetMessage(id);

                return View(message);
            }
            catch (Exception e)
            {
                TempData["sErrMsg"] = e.Message;
                return View();
                //return Redirect("~/");
            }

        }

        // GET: ReadController/Delete
        public ActionResult Delete(int id)
        {
            try
            {
                if (id == 0)
                {
                    return NotFound();
                }
                //det isRead in message
                var message = MessageBL.GetMessage(id);

                return View(message);
            }
            catch
            {
                return Redirect("~/");
            }
        }

        // POST: ReadController/Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirm(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }
            try
            {
                MessageBL.RemoveMessage(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
