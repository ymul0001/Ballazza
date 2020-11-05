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
    public class BookingsController : Controller
    {
        private BallazzaEntities db = new BallazzaEntities();

        // GET: Bookings
        //Display the Booking's index view
        [Authorize(Roles = "User")]
        public ActionResult Index()
        {
            var bookings = db.Bookings.Include(b => b.Workshop);
            return View(bookings.ToList());
        }

        //GET: /Bookings/AdminIndex
        //Return the Booking's admin index view
        [Authorize(Roles = "Administrator")]
        public ActionResult AdminIndex()
        {
            return View();
        }


        //GET: Bookings/GetBookingList
        //Get list of bookings to be displayed
        [Authorize(Roles="Administrator,User")]
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

        //GET: Bookings/GetMonthlyBookingReport
        //Get data for the annual booking report
        [Authorize(Roles = "Administrator")]
        public ActionResult GetMonthlyBookingReport() {
            var bookingsByMonth = db.Bookings.GroupBy(b => b.BookingDate.Month).Select(y => new { 
                    Month = y.Key,
                    NumberOfBookings = y.Count()
            });

            return Json(
                    new
                    {
                        data = (from obj in bookingsByMonth.ToList()
                                select new
                                {
                                    Month = obj.Month,
                                    NumberOfBookings = obj.NumberOfBookings
                                })
                    }, JsonRequestBehavior.AllowGet); ; 
        }

        //i.e. GET: /Bookings/GetMonthlyBookingReportByYear?year=2018
        //Get data for the annual booking report by each year
        [Authorize(Roles = "Administrator")]
        public ActionResult GetMonthlyBookingReportByYear(string year)
        {
            var bookingYear = Int32.Parse(year);
            var bookingsByMonth = db.Bookings.Where(data => data.BookingDate.Year == bookingYear ).GroupBy(b => b.BookingDate.Month).Select(y => new {
                Month = y.Key,
                NumberOfBookings = y.Count()
            });
            return Json(
                    new
                    {
                        data = (from obj in bookingsByMonth.ToList()
                                select new
                                {
                                    Month = obj.Month,
                                    NumberOfBookings = obj.NumberOfBookings
                                })
                    }, JsonRequestBehavior.AllowGet); ;
        }

        // GET: Bookings/GetWorkshopDetails/1001
        //Return details of a particular workshop
        [Authorize(Roles = "User")]
        public ActionResult GetWorkshopDetails(int? WorkshopId)
        {
            if (WorkshopId != null)
            {
                Booking model = new Booking
                {
                    Id = User.Identity.GetUserId(),
                    WorkshopId = WorkshopId ?? default,
                    BookingDate = DateTime.Now,
                };
                return View(model);
            }
            else { 
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
        }

        // POST: Bookings/Create
        // Create a booking inside the database
        [Authorize(Roles = "User")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "BookingId, Id, WorkshopId, BookingDate")] Booking booking)
        {
            if (ModelState.IsValid)
            {
                db.Bookings.Add(booking);
                db.SaveChanges();
                new MailController().SendBookingNotificationEmail(User.Identity.GetUserName(), booking.WorkshopId, booking.BookingDate);
            }
            return RedirectToAction("Index");
        }


        // GET: Bookings/Delete/5
        // Get a particular booking details that a user wants to delete
        [Authorize(Roles = "Administrator, User")]
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
        // Delete a particular booking which match a particular booking id
        [Authorize(Roles = "Administrator, User")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Booking booking = db.Bookings.Find(id);
            db.Bookings.Remove(booking);
            db.SaveChanges();
            if (User.IsInRole("Administrator"))
            {
                return RedirectToAction("AdminIndex");
            }
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
