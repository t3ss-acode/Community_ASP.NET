using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Community_ASP.NET.Areas.Identity.Data;
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
        public ActionResult Messages()
        {
            var messageList = MessageBL.GetMessages(_userManager.GetUserId(User), "2ffc89d2-59fa-4c3f-8059-2e6b43a9289c");

            return View(messageList);
        }

        // GET: ReadController/Details/5
        public ActionResult Details(int id)
        {
            return View();
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
