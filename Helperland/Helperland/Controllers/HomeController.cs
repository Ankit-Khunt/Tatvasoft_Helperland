using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Helperland.Models;
using Helperland.ViewModels;
using Microsoft.AspNetCore.Mvc;


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
        public IActionResult Index()
        {
            return View();
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
                return RedirectToAction("index");
            }
            return View();
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
