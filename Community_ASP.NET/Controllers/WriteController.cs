using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Community_ASP.NET.Controllers
{
    [Authorize]
    public class WriteController : Controller
    {
        // GET: WriteController
        public ActionResult Index()
        {
            return View();
        }

        // GET: WriteController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: WriteController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: WriteController/Create
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

        // GET: WriteController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: WriteController/Edit/5
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

        // GET: WriteController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: WriteController/Delete/5
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
