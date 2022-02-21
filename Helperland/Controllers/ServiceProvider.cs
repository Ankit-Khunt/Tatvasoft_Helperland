using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Helperland.Models;
using Helperland.ViewModels;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Helperland.Controllers
{
    public class ServiceProvider : Controller
    {
        public HelperlandContext _helperlandContext;
        public ServiceProvider(HelperlandContext helperlandContext)
        {
            _helperlandContext = helperlandContext;
        }
        // GET: /<controller>/
        [HttpGet]
        public IActionResult Register_ServiceProvider()
        {
            return View();
        }
        //Email is alredy reg or not
        public bool IsEmailExists(string email)
        {
            var IsCheck = _helperlandContext.User.Where(e => e.Email == email).FirstOrDefault();
            return IsCheck != null;
        }
        [HttpPost]
        public IActionResult Register_ServiceProvider(Register_ServiceProvider_ViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var IsEmailRegistered = IsEmailExists(model.Email);
                    if (IsEmailRegistered)
                    {
                        ModelState.AddModelError("EmailExists", "Email Is Alreay Existed");
                        return View("Register_ServiceProvider");
                    }
                    User newServiceProvider = new User
                    {

                        FirstName = model.FirstName,
                        Email = model.Email,
                        LastName = model.LastName,
                        Password = Helperland.ViewModels.EncryptPassword.texttoEncrypt(model.Password),  //PasswordEncrypted
                        Mobile = model.Mobile,
                        CreatedDate = DateTime.Now,
                        ModifiedDate = DateTime.Now,
                        UserTypeId = 2
                    };
                    
                     
                    _helperlandContext.User.Add(newServiceProvider);
                    ViewBag.Message = "Success";
                    _helperlandContext.SaveChanges();
                    return View();
                }
                catch (Exception exc)
                {
                    Console.WriteLine("Error: " + exc.Message);

                    ViewBag.Message = "User" + model.FirstName + "Register successful";
                }
            }
            return View();
        }

    }
}
