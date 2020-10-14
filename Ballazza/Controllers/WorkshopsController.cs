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

namespace Ballazza.Controllers
{
    [RequireHttps]
    public class WorkshopsController : Controller
    {
        private BallazzaEntities db = new BallazzaEntities();


        //Return the Workshop's index view
        [AllowAnonymous]
        public ActionResult Index()
        {
            return View();
        }

        //Return the Workshop's admin index view
        [Authorize(Roles = "Administrator")]
        public ActionResult AdminIndex()
        {
            return View();
        }


        //GET: Workshops/GetWorkshopList
        [AllowAnonymous]
        public ActionResult GetWorkshopList() {
            var workshopsList = db.Workshops.Include(w => w.Venue).ToList();
            return Json(
                new { 
                data = (from obj in workshopsList select new { 
                    Id = obj.WorkshopId, 
                    Name = obj.Venue.VenueName,
                    AgeGroup = obj.WorkshopAgeGroup,
                    StartDate = obj.WorkshopStartDate,
                    EndDate = obj.WorkshopEndDate,
                    Quota = obj.WorkshopQuota,
                })
            }, JsonRequestBehavior.AllowGet);;
        }

        /*
        // GET: Workshops/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Workshop workshop = db.Workshops.Find(id);
            if (workshop == null)
            {
                return HttpNotFound();
            }
            return View(workshop);
        }
        */


        // GET: Workshops/Create
        [Authorize(Roles = "Administrator")]
        public ActionResult Create()
        {
            ViewBag.VenueId = new SelectList(db.Venues, "VenueId", "VenueName");
            return RedirectToAction("/Workshops/AdminIndex");
        }


        // POST: Workshops/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Administrator")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "WorkshopId,WorkshopAgeGroup,WorkshopStartDate,WorkshopEndDate,WorkshopQuota,VenueId")] Workshop workshop)
        {
            if (ModelState.IsValid)
            {
                db.Workshops.Add(workshop);
                db.SaveChanges();
                return RedirectToAction("AdminIndex");
            }

            ViewBag.VenueId = new SelectList(db.Venues, "VenueId", "VenueName", workshop.VenueId);
            return View(workshop);
        }


        // GET: Workshops/Edit/5
        [Authorize(Roles = "Administrator")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Workshop workshop = db.Workshops.Find(id);
            if (workshop == null)
            {
                return HttpNotFound();
            }
            ViewBag.VenueId = new SelectList(db.Venues, "VenueId", "VenueName", workshop.VenueId);
            return View(workshop);
        }


        // POST: Workshops/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Administrator")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "WorkshopId,WorkshopAgeGroup,WorkshopStartDate,WorkshopEndDate,WorkshopQuota,VenueId")] Workshop workshop)
        {
            if (ModelState.IsValid)
            {
                db.Entry(workshop).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("AdminIndex");
            }
            ViewBag.VenueId = new SelectList(db.Venues, "VenueId", "VenueName", workshop.VenueId);
            return View(workshop);
        }

        // GET: Workshops/Delete/5

        [Authorize(Roles = "Administrator")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Workshop workshop = db.Workshops.Find(id);
            if (workshop == null)
            {
                return HttpNotFound();
            }
            return View(workshop);
        }

        // POST: Workshops/Delete/5

        [Authorize(Roles = "Administrator")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Workshop workshop = db.Workshops.Find(id);
            db.Workshops.Remove(workshop);
            db.SaveChanges();
            return RedirectToAction("Index");
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
