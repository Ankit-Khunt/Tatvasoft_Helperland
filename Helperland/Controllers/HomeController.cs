using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.IO;
using System.Net.Mail;
using System.Threading.Tasks;
using Helperland.Models;
using Helperland.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace Helperland.Controllers
{
    
    public class HomeController : Controller
    {
        public HelperlandContext _helperlandContext;
        public HomeController(HelperlandContext helperlandContext)
        {
            _helperlandContext = helperlandContext;
        }
        // GET: /<controller>/
       
        [HttpGet]
        public IActionResult Index()
        {
                return View();
        }
        public IActionResult AccesManager()
        {
            ViewBag.openLoginModel = true;

            return View("~/Views/Home/Index.cshtml");

        }
        
        public IActionResult Faq()
        {
            return View();
        }
        [Authorize] 
        public IActionResult Price()
        {
            return View();
        }
        public IActionResult About()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Contact()
        {
            return View();
        }
       
        [HttpPost]
        public IActionResult Contact(Contact_Us_ViewModel model)
        {
            if (ModelState.IsValid)
            {
                ContactUs newContat = new ContactUs
                {
                    Name = model.FileName + model.LastName,
                    Email = model.Email,
                    Message = model.Message,
                    Subject = model.Subject,
                    PhoneNumber=model.PhoneNumber
                };
                _helperlandContext.Add(newContat);
                _helperlandContext.SaveChanges();
                SendEmailToUser(model.Email,model.Message,model.Subject,model.PhoneNumber,model.FirstName);
                return RedirectToAction("index");
            }
            return View();
        }
        public void SendEmailToUser(string emailId,string message,string sub,string phonenum,string firstname)
        {
            //var GenarateUserVerificationLink = "/Register/UserVerification/" + activationCode;
            //var link = Request.Url.AbsoluteUri.Replace(Request.Url.PathAndQuery, GenarateUserVerificationLink);
            
            var fromMail = new MailAddress("akrokes1923@gmail.com", "Ankit"); // your email    
            var fromEmailpassword = "@nkitKhunt2000"; //  your password     
            var toEmail = new MailAddress(emailId);

            var smtp = new SmtpClient();
            smtp.Host = "smtp.gmail.com";
            smtp.Port = 587;
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new NetworkCredential(fromMail.Address, fromEmailpassword);
            smtp.EnableSsl = true;

            var Message = new MailMessage(fromMail, toEmail);
            Message.Subject = "ContactUS Completed-Demo";
            Message.Body = "<br/> Thank you for Contact us " + firstname +
                           "<br/> Detail of Customer given below" +
                            "<br/>PhoneNumber " + phonenum+
                            "<br/>Subject " + sub +
                            "<br/>Message " + message +
                            "<br/>Email " + emailId ;
            Message.IsBodyHtml = true;
            smtp.Send(Message);
        }
        public IActionResult Service_Provider()
        {
            return View();
        }
        public IActionResult Service_History()
        {
            return View();
        }
        
    }
}
