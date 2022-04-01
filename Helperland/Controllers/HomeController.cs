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
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace Helperland.Controllers
{
    
    public class HomeController : Controller
    {
        public HelperlandContext _helperlandContext;
        private IWebHostEnvironment _hostingEnvironment;

        public string UploadFileName { get; private set; }
        public string FileName { get; private set; }

        public HomeController(HelperlandContext helperlandContext, IWebHostEnvironment environment)
        {
            _helperlandContext = helperlandContext;
            _hostingEnvironment = environment;
        }
        // GET: /<controller>/
       
        [HttpGet]
        public IActionResult Index()
        {
            var userName=HttpContext.Session.GetString("User_Name");

            var logout=HttpContext.Session.GetString("Logout");
            if (logout == "true")
            {
                ViewBag.openLogOutModel = true;
                HttpContext.Session.SetString("Logout","");
            }
            ViewBag.UserName=userName; 
                return View();
            }
        public IActionResult AccessDenied()
        {
            return View();
        }
        public IActionResult AccesManager(string ReturnUrl= "/")
        {
            LoginViewModel objLoginModel = new LoginViewModel();
            objLoginModel.ReturnUrl = ReturnUrl;
            ViewBag.openLoginModel = true;
           
            return View("~/Views/Home/Index.cshtml");

        }
        
        public IActionResult Faq()
        {
            return View();
        }
       
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
                string uniqueFileName = null;
                if (model.File != null)
                {
                    UploadFileName = model.File.FileName;
                    FileName = uniqueFileName;
                    var path = Path.Combine(_hostingEnvironment.WebRootPath, "ContactUsAttechment");
                    uniqueFileName = Guid.NewGuid().ToString() + "_" + model.File.FileName;
                    string filePath = Path.Combine(path, uniqueFileName);
                    model.File.CopyTo(new FileStream(filePath, FileMode.Create));
                }
                ContactUs newContat = new ContactUs
                {
                    Name = model.FirstName + " "+ model.LastName,
                    Email = model.Email,
                    Message = model.Message,
                    Subject = model.Subject,
                    PhoneNumber=model.PhoneNumber,
                    CreatedOn = DateTime.Now,
                    UploadFileName = UploadFileName,
                    FileName = uniqueFileName,
                };
                _helperlandContext.Add(newContat);
                _helperlandContext.SaveChanges();
                ViewBag.ContactClear = true;
                SendEmailToUser(model.Email,model.Message,model.Subject,model.PhoneNumber,model.FirstName);
                ViewBag.Alert = "<div class='alert alert-success alert-dismissible fade show' role='alert'>Thank you for Contact Us<button type= 'button' class='btn-close' data-bs-dismiss='alert' aria-label='Close'></button></div>";
                return View();
            }
            return View();
        }
        public void SendEmailToUser(string emailId,string message,string sub,string phonenum,string firstname)
        {
            //var GenarateUserVerificationLink = "/Register/UserVerification/" + activationCode;
            //var link = Request.Url.AbsoluteUri.Replace(Request.Url.PathAndQuery, GenarateUserVerificationLink);
            
            var fromMail = new MailAddress("ankitkhunt2001@gmail.com", "Ankit"); // your email    
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

        public PartialViewResult LogOutModal()
        {
            return PartialView();
        }


    }
}
