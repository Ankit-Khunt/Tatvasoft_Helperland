using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Helperland.Models;
using Helperland.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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
            if (HttpContext.Session.GetString("User_Email") != null)
            {
                ViewBag.Nav = true;
            }
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
                    ViewBag.Message = "User" + model.FirstName + "Register successful";
                    _helperlandContext.SaveChanges();
                    

                    return View();



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
        public IActionResult User_Login(LoginViewModel model)
        {
            //we can use var insted of User
           User user = _helperlandContext.User.Where(x => x.Email == model.Email && x.Password == model.Password).FirstOrDefault();
            if (user == null)
            {
                ViewBag.openLoginModel = true;
                return View("~/Views/Home/Index.cshtml");
                
            }
            else
            {
                var userClaims = new List<Claim>()
                {

                    new Claim("UserName", user.FirstName),
                    new Claim(ClaimTypes.Name, user.LastName),
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(ClaimTypes.DateOfBirth, user.Password),
                    new Claim(ClaimTypes.Role,user.UserTypeId.ToString())
                 };
               
                var userIdentity = new ClaimsIdentity(userClaims, "User Identity");
               
                var userPrincipal = new ClaimsPrincipal(new[] { userIdentity });
                HttpContext.SignInAsync(userPrincipal);
                HttpContext.Session.SetString("User_Id", user.UserId.ToString());
                HttpContext.Session.SetString("User_Email",user.Email);   //session value set
                return RedirectToAction("UserDashbord");    
            }


            return RedirectToAction("Index", "Home");
           
           
            
        }

        public IActionResult UserDashbord()
        {
            if (HttpContext.Session.GetString("User_Email") != null)
            {
                ViewBag.Nav = true;
                ViewBag.Message = "Ha moj ha" + HttpContext.Session.GetString("User_Email");
                return View();
            }
            return View();
        }

       
    }
}