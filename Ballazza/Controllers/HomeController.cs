using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Ballazza.Controllers
{   
    [RequireHttps]
    public class HomeController : Controller
    {
        [Authorize(Roles = "Administrator")]
        public ActionResult AdminIndex() {
            return View();
        }

        [AllowAnonymous]
        public ActionResult Chat()
        {
            if (User.Identity.GetUserId() == null)
            {
                ViewBag.user = "anonymous";
            }
            else {
                if (User.IsInRole("Administrator")) {
                    ViewBag.user = "admin";
                }
                else {
                    ViewBag.user = User.Identity.GetUserName();
                }
            }
            return View();
        }

        [AllowAnonymous]
        public ActionResult Index()
        {
            if (TempData["userInfo"] != null) { 
                ViewBag.UserInfo= TempData["userInfo"].ToString();
            }
            return View();
        }

        [AllowAnonymous]
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        [AllowAnonymous]
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}