﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Ballazza.Models;
using Microsoft.AspNet.Identity;

namespace Ballazza.Controllers
{
    public class BookingsController : Controller
    {
        private BallazzaEntities db = new BallazzaEntities();

        // GET: Bookings
        [Authorize(Roles = "User")]
        public ActionResult Index()
        {
            var bookings = db.Bookings.Include(b => b.Workshop);
            return View(bookings.ToList());
        }

        //Return the Booking's admin index view
        [Authorize(Roles = "Administrator")]
        public ActionResult AdminIndex()
        {
            return View();
        }

        /*
        // GET: Bookings/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Booking booking = db.Bookings.Find(id);
            if (booking == null)
            {
                return HttpNotFound();
            }
            return View(booking);
        }
        */

        //GET: Bookings/GetBookingList
        [Authorize]
        public ActionResult GetBookingList()
        {
            var bookingList = db.Bookings.Include(w => w.Workshop);
            if (User.IsInRole("User")) {
                var userId = User.Identity.GetUserId();
                bookingList = bookingList.Include(w => w.Workshop).Where(m => m.Id == userId);
            }
            
            return Json(
                new
                {
                    data = (from obj in bookingList.ToList()
                            select new
                            {
                                BookingId = obj.BookingId,
                                Id = obj.Workshop.WorkshopId,
                                Name = obj.Workshop.Venue.VenueName,
                                AgeGroup = obj.Workshop.WorkshopAgeGroup,
                                StartDate = obj.Workshop.WorkshopStartDate,
                                EndDate = obj.Workshop.WorkshopEndDate,
                            })
                }, JsonRequestBehavior.AllowGet); ;
        }

        // GET: Bookings/Delete/5
        [Authorize(Roles = "Administrator")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Booking booking = db.Bookings.Find(id);
            if (booking == null)
            {
                return HttpNotFound();
            }
            return View(booking);
        }

        // POST: Bookings/Delete/5
        [Authorize(Roles = "Administrator")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Booking booking = db.Bookings.Find(id);
            db.Bookings.Remove(booking);
            db.SaveChanges();
            return RedirectToAction("AdminIndex");
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