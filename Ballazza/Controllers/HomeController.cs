using Ballazza.Models;
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
        private BallazzaEntities db = new BallazzaEntities();


        //GET: /Home/Index
        //Display the administrator's home page
        [Authorize(Roles = "Administrator")]
        public ActionResult AdminIndex() {
            var ratings = Math.Floor(db.Ratings.Average(r => r.RatingValue) * 100) / 100;
            var workshops = db.Workshops.Count();
            ViewBag.Message = TempData["message"];
            ViewBag.TotalRating = ratings;
            ViewBag.TotalWorkshops = workshops;
            return View();
        }


        //GET: /Home/Chat
        //Display the chat page

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


        //GET: /Home/Index
        //Display the user's home page
        [AllowAnonymous]
        public ActionResult Index()
        {
            if (TempData["userInfo"] != null)
            {
                ViewBag.UserInfo = TempData["userInfo"].ToString();
            }
            else {
                if (User.Identity.IsAuthenticated) {
                    ViewBag.DefaultUserInfo = "Hello User!";
                }
            }
            return View();
        }
    }
}