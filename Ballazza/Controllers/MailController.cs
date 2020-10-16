using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;

namespace Ballazza.Controllers
{
    public class MailController : Controller
    {
        SmtpClient smtp = new SmtpClient
        {
            Host = "smtp.gmail.com",
            DeliveryMethod = SmtpDeliveryMethod.Network,
            Port = 587,
            UseDefaultCredentials = false,
            Credentials = new System.Net.NetworkCredential("omhaohao@gmail.com", "3qtngHSa"),
            EnableSsl = true
        };
        //Send notification email for successful bookings
        /*
        public void AssignSmtpDetails() {
            
           
        }
        */
        [HttpPost]
        public void SendBookingNotificationEmail(string ReceiverEmail, int WorkshopId, System.DateTime BookingTime)
        {
            try {              
                MailMessage email = new MailMessage();
                email.From = new MailAddress("albertusyonas15@gmail.com");
                email.To.Add(ReceiverEmail);
                email.Subject = "#Workshop id: " + WorkshopId + " Booking confirmation";
                var body = "<p>Dear customer {0}:</p><p>This email is to confirm that you have booked into our workshops on {1}</p><p>Please contact our customer support if you have further questions</p><br/><br/><p>Regards,</p><p>Ballazza Team</p>";
                email.Body = string.Format(body, ReceiverEmail, BookingTime);
                email.IsBodyHtml = true;
                smtp.Send(email);
            }
            catch (Exception ex)
            {
                throw ex;//throw the exception
            };
        }
    }
}