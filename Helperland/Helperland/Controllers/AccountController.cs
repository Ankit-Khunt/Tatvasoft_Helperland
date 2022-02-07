using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Helperland.Models;
using Helperland.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Helperland.Controllers
{
    public class AccountController : Controller
    {
        public HelperlandContext _helperlandContext;
        public AccountController(HelperlandContext helperlandContext)
        {
            _helperlandContext = helperlandContext;
        }
       
        [HttpGet]
        public IActionResult User_Register()
        {
            return View();
        }

       

    


[HttpPost]
        public IActionResult User_Register(Register_User_ViewModel model)
        {
           if (ModelState.IsValid)
            {
                try
                {
                    User newUser = new User
                    {
                        FirstName = model.FirstName,
                        Email = model.Email,
                        LastName = model.LastName,
                        Password = model.Password,
                        Mobile = model.Mobile,
                        CreatedDate = DateTime.Now,
                        ModifiedDate = DateTime.Now,
                        UserTypeId = 3
                        
                    };
                    _helperlandContext.User.Add(newUser);
                    ViewBag.Message = "User" + model.FirstName + "Register not successful";
                    _helperlandContext.SaveChanges();
                    return RedirectToAction("index");



                }
                catch (Exception exc)
                {
                    Console.WriteLine("Error: " + exc.Message);

                    ViewBag.Message = "User" + model.FirstName + "Register not successful";
                }
            }
            
            
            return View();
        }
        [HttpGet]
        public IActionResult User_Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult User_Login(User user)
        {

           User LoggedInUser = _helperlandContext.User.Where(x => x.Email == user.Email && x.Password == user.Password).FirstOrDefault();
            if (LoggedInUser == null)
            {
                return View();
            }
            return RedirectToAction("UserDashbord");
        }

        public IActionResult UserDashbord()
        {
            return View();
        }
    }
}