using Portfolio.Models;
using System;
using System.Net;
using System.Net.Mail;
using System.Web.Mvc;

namespace Portfolio.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Error()
        {
            return View("~/Views/Shared/Error.cshtml");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Contact(ContactViewModel contactInfo)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Error", "Home");
            }//if - modelstate not valid

            // make the email 
            string body = string.Format(
                $"name: {contactInfo.name}<br />"
                + $"email: {contactInfo.email}<br />"
                + $"subject: {contactInfo.subject}<br />"
                + $"message: {contactInfo.message}");

            // create the mailmessage object - System.Net.Mail

            MailMessage msg = new MailMessage(
                "no-reply@jenmcook.com",
                "jenmcook@outlook.com",
                contactInfo.subject, body)
            {
                //set MailMessage objects properties
                IsBodyHtml = true
            };
            msg.CC.Add("JenMCook@outlook.com");

            // SMTP client needs created and config'd
            SmtpClient client = new SmtpClient("mail.jenmcook.com")
            {
                Credentials = new NetworkCredential("no-reply@jenmcook.com", "FakePassword"),
                EnableSsl = false,
                Port = 8889
            };

            //use SMTP client object to sent email
            using (client)
            {
                try
                {
                    client.Send(msg);
                    ViewBag.ErrorMessage = "Your message has been sent. Thank you!";
                    return RedirectToAction("Index", "Home");
                } // try
                catch (Exception ex)
                {
                    //message if failed to send
                    ViewBag.ErrorMessage = "An error has occurred in sending your message.\n"
                        + "Please try again";
                    return RedirectToAction("Error", "Home");
                } //catch
            } // using - cient
        }
    }
}