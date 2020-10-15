using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Community_ASP.NET.Areas.Identity.Data;
using Community_ASP.NET.Data;
using Community_ASP.NET.Models;
using Community_ASP.NET.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Community_ASP.NET.Controllers
{
    [Authorize]
    public class IndexController : Controller
    {
        
        private readonly UserManager<Community_ASPNETUser> _userManager;

        public IndexController(UserManager<Community_ASPNETUser> userManager)
        {
            _userManager = userManager;
        }
        

        // GET: IndexController
        
        public ActionResult Index()
        {
            var userInfoList = new List<UserInfo>();


            var user = UserBL.GetUser(_userManager.GetUserId(User));
            userInfoList.Add(user);


            //userInfoList.Add(new UserInfo("test name", "test@kth.se", DateTime.Now, 4, 5, 20));
            return View(userInfoList);
        }
        


        // GET: IndexController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: IndexController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: IndexController/Create
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

        // GET: IndexController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: IndexController/Edit/5
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

        // GET: IndexController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: IndexController/Delete/5
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
