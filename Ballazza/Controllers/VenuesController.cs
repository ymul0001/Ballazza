using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Ballazza.Models;

namespace Ballazza.Controllers
{
    public class VenuesController : Controller
    {
        private BallazzaEntities db = new BallazzaEntities();

        // GET: Venues
        // Display the venues page
        [AllowAnonymous]
        public ActionResult Index()
        {
            return View(db.Venues.ToList());
        }

        // GET: Venues/AdminIndex
        // Display the venues page for the admin
        [Authorize(Roles = "Administrator")]
        public ActionResult AdminIndex()
        {
            return View();
        }

        // GET: Venues/GetVenuesList
        // Get list of venues to be displayed in the page
        [AllowAnonymous]
        public ActionResult GetVenuesList()
        {

            var venuesList = db.Venues.ToList();
            return Json(
                new
                {
                    data = (from obj in venuesList
                            select new
                            {
                                VenueName = obj.VenueName,
                                VenueAddress = obj.VenueStreet + " " + obj.VenueSuburb + " " + obj.VenueState + " " + obj.VenuePostcode
                            })
                }, JsonRequestBehavior.AllowGet); ;
        }


        // GET: Venues/GetVenuesListAdmin
        // Get list of venues to be displayed in the page specifically for admin
        [Authorize(Roles = "Administrator")]
        public ActionResult GetVenuesListAdmin()
        {

            var venuesList = db.Venues.ToList();
            return Json(
                new
                {
                    data = (from obj in venuesList
                            select new
                            {
                                VenueId = obj.VenueId,
                                VenueName = obj.VenueName,
                                VenueStreet = obj.VenueStreet,
                                VenueSuburb = obj.VenueSuburb,
                                VenueState = obj.VenueState,
                                VenuePostcode = obj.VenuePostcode,
                                VenuePhoneno = obj.VenuePhoneno
                            })
                }, JsonRequestBehavior.AllowGet); ;
        }

        // GET: Venues/Create
        // Display the venue's creation page
        [Authorize(Roles = "Administrator")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Venues/Create
        // Create a venue details
        [Authorize(Roles = "Administrator")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "VenueId,VenueName,VenueStreet,VenueSuburb,VenueState,VenuePostcode,VenuePhoneno")] Venue venue)
        {
            if (ModelState.IsValid)
            {
                db.Venues.Add(venue);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(venue);
        }


        // GET: Venues/Edit/5
        // Get the venue edit's page
        [Authorize(Roles = "Administrator")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Venue venue = db.Venues.Find(id);
            if (venue == null)
            {
                return HttpNotFound();
            }
            return View(venue);
        }

        // POST: Venues/Edit/5
        // Update the venue data inside the database
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public ActionResult Edit([Bind(Include = "VenueId,VenueName,VenueStreet,VenueSuburb,VenueState,VenuePostcode,VenuePhoneno")] Venue venue)
        {
            if (ModelState.IsValid)
            {
                db.Entry(venue).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("AdminIndex");
            }
            return View(venue);
        }

        // GET: Venues/Delete/5
        // Get specific venue details before deleting the data
        [Authorize(Roles = "Administrator")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Venue venue = db.Venues.Find(id);
            if (venue == null)
            {
                return HttpNotFound();
            }
            return View();
        }

        // POST: Venues/Delete/5
        // Delete specific venue details from the database
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public ActionResult DeleteConfirmed(int id)
        {
            Venue venue = db.Venues.Find(id);
            db.Venues.Remove(venue);
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
