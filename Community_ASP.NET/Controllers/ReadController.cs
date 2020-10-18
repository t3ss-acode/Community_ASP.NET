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
            var userList = MessageBL.GetUsersOfMessages(_userManager.GetUserId(User));

            return View(userList);
        }

        // GET: ReadController Messages page
        public ActionResult Messages(string id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var messageList = MessageBL.GetMessages(_userManager.GetUserId(User), id);

            return View(messageList);
        }

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Message(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }
            //det isRead in message
            var messageList = MessageBL.GetMessage(id);

            return View(messageList);
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

        // GET: ReadController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ReadController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
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
