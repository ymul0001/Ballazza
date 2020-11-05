using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Ballazza.Models;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;

namespace Ballazza.Controllers
{
    public class RatingsController : Controller
    {
        private BallazzaEntities db = new BallazzaEntities();
        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }
        //GET: Ratings
        //Displays the rating page
        [Authorize(Roles ="User")]
        public ActionResult Index()
        {
            return View(db.Ratings.ToList());
        }

        //GET: Ratings/Details/5
        //Display the details of a particular rating record
        [Authorize(Roles = "User")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Rating rating = db.Ratings.Find(id);
            if (rating == null)
            {
                return HttpNotFound();
            }
            return View(rating);
        }

        //GET: Ratings/Create
        //Display the rating creation page with passing a certain userId
        [Authorize(Roles="User")]
        public ActionResult Create()
        {
            ViewBag.id = User.Identity.GetUserId();
            return View();
        }

        //POST: Ratings/Create
        //Create a rating and store it to the database
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "User")]
        public ActionResult Create([Bind(Include = "RatingId,Id,RatingValue,RatingFeedback")] Rating rating)
        {
            if (ModelState.IsValid)
            {
                db.Ratings.Add(rating);
                db.SaveChanges();
                AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
                return RedirectToAction("Index","Home");
            }

            return View(rating);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
