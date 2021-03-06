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
using MySqlX.XDevAPI;
using Newtonsoft.Json;

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
                        UserTypeId = ValuesData.CUSTOMER,
                        IsApproved=true,
                        IsActive=true

                    };
                    var message = "You are Successfuly Register !";
                    ViewBag.ClearForm = true;
                    ViewBag.Alert = "<div class='alert alert-success alert-dismissible fade show' role='alert'>" + message + "<button type= 'button' class='btn-close' data-bs-dismiss='alert' aria-label='Close'></button></div>";
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
            if (ModelState.IsValid)
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
                    if (user.IsApproved == true && user.IsActive == true)
                    {
                        var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme, ClaimTypes.Name, ClaimTypes.Role);

                        identity.AddClaim(new Claim("userId", user.UserId.ToString()));
                        identity.AddClaim(new Claim(ClaimTypes.Email, user.Email));
                        identity.AddClaim(new Claim(ClaimTypes.Name, user.FirstName + " " + user.LastName));
                        identity.AddClaim(new Claim(ClaimTypes.Role, user.UserTypeId.ToString()));
                        identity.AddClaim(new Claim("role", user.UserTypeId.ToString()));

                        var principal = new ClaimsPrincipal(identity);

                        var authProperties = new AuthenticationProperties
                        {
                            AllowRefresh = true,
                            ExpiresUtc = DateTimeOffset.Now.AddMinutes(30),
                            IsPersistent = model.RememberLogin,
                        };

                        HttpContext.Session.SetString("CurrentUser", JsonConvert.SerializeObject(user));


                        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, authProperties);
                       
                        
                        HttpContext.Session.SetString("User_Id", user.UserId.ToString());
                        HttpContext.Session.SetString("User_Email", user.Email);   //session value set
                        HttpContext.Session.SetString("User_Name", user.FirstName + " " + user.LastName);
                        if (model.ReturnUrl == null)
                        {
                            switch (user.UserTypeId)
                            {
                                case 1:
                                    return RedirectToAction("AdminServiceRequest", "Admin");
                                    break;
                                case 2:
                                    return RedirectToAction("NewServiceRequests", "ServiceProvider");
                                    break;
                                case 3:
                                    return RedirectToAction("CustomerDashboard", "CustomerService");
                                    break;
                                default: return RedirectToAction("Index", "Home");
                            }
                            // return RedirectToAction("CustomerDashboard","CustomerService");
                        }
                        else
                        {
                            return LocalRedirect(model.ReturnUrl);
                        }
                    }
                    else
                    {
                        ViewBag.openLoginModel = true;
                        ViewBag.Alert = "<div class='alert alert-danger alert-dismissible fade show' role='alert'>You are Approved Yet for Login<button type= 'button' class='btn-close' data-bs-dismiss='alert' aria-label='Close'></button></div>";

                        return View("~/Views/Home/Index.cshtml");
                    }


                }
            }
            else
            {
                ViewBag.openLoginModel = true;
                return RedirectToAction("Index", "Home");
            }
           


            
           
           
            
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
        public async Task<IActionResult> LogOut()
        {
            HttpContext.Session.SetString("Logout", "true");
            //SignOutAsync is Extension method for SignOut    
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            HttpContext.Session.Clear();
            
            //Redirect to home page    
            return RedirectToAction("Index", "Home");
        }


    }

   
 }