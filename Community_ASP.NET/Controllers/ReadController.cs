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

            return View(userList);
            }
            catch
            {

                return View();
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
            }catch
            {
                return View();
            }

        }

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Message(int id)
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
                return View();
            }

        }

        // GET: ReadController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ReadController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ReadController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ReadController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
