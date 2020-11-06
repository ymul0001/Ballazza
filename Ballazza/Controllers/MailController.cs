using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;

namespace Ballazza.Controllers
{

    public class MailController : Controller
    {
        private ApplicationUserManager _userManager;

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? Request.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        SmtpClient smtp = new SmtpClient
        {
            Host = "smtp.gmail.com",
            DeliveryMethod = SmtpDeliveryMethod.Network,
            Port = 587,
            UseDefaultCredentials = false,
            Credentials = new System.Net.NetworkCredential("omhaohao@gmail.com", "3qtngHSa"),
            EnableSsl = true
        };
        
        //POST: /Mail/SendBookingNotificationEmail
        //send notification emails to the customers if they have successfully booked a workshop
        [HttpPost]
        public void SendBookingNotificationEmail(string ReceiverEmail, int WorkshopId, System.DateTime BookingTime)
        {
            try {              
                MailMessage email = new MailMessage();
                email.From = new MailAddress("omhaohao@gmail.com");
                email.To.Add(ReceiverEmail);
                email.Subject = "#Workshop id: " + WorkshopId + " Booking confirmation";
                var body = "<p>Dear customer {0}:</p><p>This email is to confirm that you have booked into our workshops on {1}</p><p>Please contact our customer support if you have further questions</p><br/><br/><p>Regards,</p><p>Ballazza Team</p>";
                email.Body = string.Format(body, ReceiverEmail, BookingTime);
                email.IsBodyHtml = true;
                smtp.Send(email);
            }
            catch (Exception ex)
            {
                throw ex;
            };
        }

        //return the Advertisement's email page
        //GET: /Mail/Index
        [Authorize(Roles ="Administrator")]
        public ActionResult Index() {
            var listOfUsers = UserManager.Users.Select(u => u.Email);
            ViewBag.Users = new SelectList(listOfUsers);
            return View();
        }

        //Send bulk advertisements emails
        //POST: /Mail/SendBulkEmail
        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public ActionResult SendBulkEmail(Ballazza.Models.MailModel objModelMail, HttpPostedFileBase fileUploader)

        {
            if (ModelState.IsValid)
            {
                string from = "omhaohao@gmail.com";
                string[] emails = objModelMail.To.Split(';');
                foreach (var email in emails)
                {
                    using (MailMessage mail = new MailMessage(from, email))
                    {
                        mail.Subject = objModelMail.Subject;
                        mail.Body = objModelMail.Body;
                        if (fileUploader != null)
                        {
                            string fileName = Path.GetFileName(fileUploader.FileName);
                            mail.Attachments.Add(new Attachment(fileUploader.InputStream, fileName));
                        }
                        mail.IsBodyHtml = false;
                        smtp.Send(mail);
                    }
                }
                TempData["message"] = "Sent";
                return RedirectToAction("AdminIndex","Home");
            }
            else
            {
                return View();
            }
        }
    }
}