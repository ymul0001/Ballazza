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

        //GET: /Workshops/Index
        //Return the Workshop's index view
        [AllowAnonymous]
        public ActionResult Index()
        {
            return View();
        }

        //GET: /Workshops/AdminIndex
        //Return the Workshop's admin index view
        [Authorize(Roles = "Administrator")]
        public ActionResult AdminIndex()
        {
            return View();
        }


        //GET: Workshops/GetWorkshopList
        //get the list of workshops to be displayed in the workshop page 
        [AllowAnonymous]
        public ActionResult GetWorkshopList() {
            var workshopsList = db.Workshops.Include(w => w.Venue);
            if (User.Identity.IsAuthenticated)
            {
                var currentUser = User.Identity.GetUserId();
                var existedWorkshopsList = db.Workshops.Where(w => db.Bookings.Where(data => data.Id == currentUser).Any(b => b.WorkshopId == w.WorkshopId));
                if (existedWorkshopsList.Any())
                {
                    var dateClashWorkshopsList = db.Workshops.Where(w => db.Bookings.Where(data => data.Id == currentUser).Any(b => b.WorkshopId != w.WorkshopId && b.Workshop.WorkshopStartDate == w.WorkshopStartDate));
                    var dateFitWorkshopsList = workshopsList.Except(existedWorkshopsList).Except(dateClashWorkshopsList);
                    var bookedList = (from obj in existedWorkshopsList
                                        select new
                                        {
                                            Id = 9999,
                                            Name = obj.Venue.VenueName,
                                            AgeGroup = obj.WorkshopAgeGroup,
                                            StartDate = obj.WorkshopStartDate,
                                            EndDate = obj.WorkshopEndDate,
                                            Quota = obj.WorkshopQuota,
                                        });
                    var fitList = (from obj in dateFitWorkshopsList
                                       select new
                                       {
                                           Id = obj.WorkshopId,
                                           Name = obj.Venue.VenueName,
                                           AgeGroup = obj.WorkshopAgeGroup,
                                           StartDate = obj.WorkshopStartDate,
                                           EndDate = obj.WorkshopEndDate,
                                           Quota = obj.WorkshopQuota,
                                       });

                    var clashList = (from obj in dateClashWorkshopsList
                                     select new
                                     {
                                         Id = 999999,
                                         Name = obj.Venue.VenueName,
                                         AgeGroup = obj.WorkshopAgeGroup,
                                         StartDate = obj.WorkshopStartDate,
                                         EndDate = obj.WorkshopEndDate,
                                         Quota = obj.WorkshopQuota,
                                     });
                    return Json(
                    new
                    {
                        data = bookedList.Concat(clashList).Concat(fitList)
                    }, JsonRequestBehavior.AllowGet) ; ;
                }

            }
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

        // GET: Workshops/Create
        [Authorize(Roles = "Administrator")]
        public ActionResult Create()
        {
            ViewBag.VenueId = new SelectList(db.Venues, "VenueId", "VenueName");
            return RedirectToAction("/Workshops/AdminIndex");
        }


        // POST: Workshops/Create
        // Create a workshop data
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
        // Get details of a workshop data that is insisted to be updated
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
        // Update the details for the workshop
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
        // Get details of a workshop to be deleted
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
        //Delete a workshop
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
