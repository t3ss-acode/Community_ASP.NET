using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Community_ASP.NET.Data;
using Community_ASP.NET.Models;
using Community_ASP.NET.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

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
            try
            {
                var userInfoList = new List<UserInfo>();

                var user = UserBL.GetUser(_userManager.GetUserId(User));
                userInfoList.Add(user);

                return View(userInfoList);
            }
            catch (Exception e)
            {
                TempData["sErrMsg"] = e.Message;
                return View();
                //return Redirect("~/");
            }
            
        }
        
        public PartialViewResult ShowError(string sErrorMessage)
        {
            return PartialView("ErrorMessageView");
        }
    }
}
