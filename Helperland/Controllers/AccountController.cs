using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Security.Claims;
using System.Threading.Tasks;
using Helperland.Models;
using Helperland.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
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
                        UserTypeId = 3,
                        IsApproved=true,
                        IsActive=true

                    };
                    _helperlandContext.User.Add(newUser);
                    ViewBag.Message = "User" + model.FirstName + "Register successful";
                    _helperlandContext.SaveChanges();
                    

                    return RedirectToAction("index","home");



                }
                catch (Exception exc)
                {
                    Console.WriteLine("Error: " + exc.Message);

                    ViewBag.Message = "User" + model.FirstName + "Register not successful";
                }
            }
            
            
            return View();
        }
        //[HttpGet]
        //public IActionResult User_Login(string ReturnUrl="/")
        //{
        //    LoginViewModel objLoginModel = new LoginViewModel();
        //    objLoginModel.ReturnUrl = ReturnUrl;
        //    return View(objLoginModel);
        //}
        public bool IsEmailExists(string email)
        {
            var IsCheck = _helperlandContext.User.Where(e => e.Email == email).FirstOrDefault();
            return IsCheck != null;
        }
        [HttpPost]
        public async Task<IActionResult> User_Login(LoginViewModel model)
        {
            //we can use var insted of User
           User user = _helperlandContext.User.Where(x => x.Email == model.Email && x.Password == model.Password).FirstOrDefault();
            var UserHasEmailReg = IsEmailExists(model.Email);
            if (user == null)
            {
                ViewBag.openLoginModel = true;
                ViewBag.Alert = "<div class='alert alert-danger alert-dismissible fade show' role='alert'>Your EmailId and Password are not correct<button type= 'button' class='btn-close' data-bs-dismiss='alert' aria-label='Close'></button></div>";

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

                //var userIdentity = new ClaimsIdentity(userClaims, "User Identity");
                var userIdentity = new ClaimsIdentity(userClaims, CookieAuthenticationDefaults.AuthenticationScheme);

                var userPrincipal = new ClaimsPrincipal( userIdentity );
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, userPrincipal, new AuthenticationProperties()
                 {
                     IsPersistent = model.RememberLogin
                 });
                HttpContext.Session.SetString("User_Id", user.UserId.ToString());
                HttpContext.Session.SetString("User_Email",user.Email);   //session value set
                HttpContext.Session.SetString("User_Name", user.FirstName+" "+user.LastName);
                if (model.ReturnUrl == null)
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    return LocalRedirect(model.ReturnUrl);
                }
                 
            }


            return RedirectToAction("Index", "Home");
           
           
            
        }

        [HttpGet]
        public IActionResult UserDashbord()
        {
            if (HttpContext.Session.GetString("User_Email") != null)
            {
                ViewBag.Nav = true;
                ViewBag.Message = HttpContext.Session.GetString("User_Email");
                return View();
            }
            return View();
        }

        public async Task<IActionResult> LogOut()
        {
            //SignOutAsync is Extension method for SignOut    
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            //Redirect to home page    
            return RedirectToAction("Index", "Home");
        }

        public IActionResult ForgotPassword(ForgotPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {   
                User user = _helperlandContext.User.Where(x => x.Email == model.Email).FirstOrDefault();
                if(user == null)
                {
                    return BadRequest("Email Already Used");
                }
                else
                {
                    string Token = Guid.NewGuid().ToString();
                    var lnkHref = "<a href='" + Url.Action("ResetPassword", "Account", new { email = model.Email, code = Token }, "http") + "'>Reset Password</a>";

                    string subject = "Reset Password Link";
                    string body = "<b>Please find the Password Reset Link. </b><br/>" + lnkHref;
                    List<string> toList = new List<string>();
                    toList.Add(model.Email);
                    SendEmailToUser(body,subject,model.Email);
                    ViewBag.Alert = "<div class='alert alert-success alert-dismissible fade show' role='alert'>we have sent Password reset link on your Email..<button type= 'button' class='btn-close' data-bs-dismiss='alert' aria-label='Close'></button></div>";
                    return Ok();

                }
                    
                
            }
            return BadRequest("Email Already Used");
        }
        public void SendEmailToUser(string body,string subject,string emailId)
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
            Message.Subject = subject;
            Message.Body = body;
            Message.IsBodyHtml = true;
            smtp.Send(Message);
        }
        public IActionResult ResetPassword(string Email)
        {
            ResetPasswordViewModel model = new ResetPasswordViewModel();
            model.Email = Email;
            return View(model);
        }

        [HttpPost]
        public IActionResult ResetPassword(ResetPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                User user = _helperlandContext.User.Where(user => user.Email.Equals(model.Email)).FirstOrDefault();
                user.Password = model.Password;
                user.ModifiedDate = DateTime.Now;
                user.ModifiedBy = user.UserId;
                _helperlandContext.User.Update(user);
                _helperlandContext.SaveChanges();
                return RedirectToAction("index", "home");
            }
            else
            {
                return View(model);
            }
        }


    }

   
 }