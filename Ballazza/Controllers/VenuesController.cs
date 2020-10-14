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
        public ActionResult Index()
        {
            return View(db.Venues.ToList());
        }

        [Authorize(Roles = "Administrator")]
        public ActionResult AdminIndex()
        {
            return View();
        }

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

        // GET: Venues/Details/5
        public ActionResult Details(int? id)
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

        // GET: Venues/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Venues/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
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
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "VenueId,VenueName,VenueStreet,VenueSuburb,VenueState,VenuePostcode,VenuePhoneno")] Venue venue)
        {
            if (ModelState.IsValid)
            {
                db.Entry(venue).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(venue);
        }

        // GET: Venues/Delete/5
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
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
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
