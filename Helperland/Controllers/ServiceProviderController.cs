using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Helperland.Models;
using Helperland.ViewModels;
using Helperland.ViewModels.ServiceProvider;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Helperland.Controllers
{
    public class ServiceProviderController : Controller
    {
        public HelperlandContext _helperlandContext;
        public ServiceProviderController(HelperlandContext helperlandContext)
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
                        //Password = Helperland.ViewModels.EncryptPassword.texttoEncrypt(model.Password),  //PasswordEncrypted
                        Password=model.Password,
                        Mobile = model.Mobile,
                        CreatedDate = DateTime.Now,
                        ModifiedDate = DateTime.Now,
                        UserTypeId = 2,
                        IsApproved=true
                    };
                    
                     
                    _helperlandContext.User.Add(newServiceProvider);
                    ViewBag.Alert = "<div class='alert alert-success alert-dismissible fade show' role='alert'> Request Send Successfuly <button type= 'button' class='btn-close' data-bs-dismiss='alert' aria-label='Close'></button></div>";
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

        [HttpGet]

        public IActionResult NewServiceRequests()
        
        {
            //var serviceRequests = _helperlandContext.ServiceRequest.Include(x => x.User).Where(x=>x.Status == ValuesData.SERVICE_PENDING).FirstOrDefault();
            //var userAddress=_helperlandContext.ServiceRequestAddress.Where(x=>x.ServiceRequestId==serviceRequests.ServiceRequestId).FirstOrDefault();
            List<User> userList = _helperlandContext.User.ToList();
            List<ServiceRequest> serviceRequestsList = _helperlandContext.ServiceRequest.ToList();
            List<ServiceRequestAddress> serviceRequestAddressesList = _helperlandContext.ServiceRequestAddress.ToList();

            var resultNewServiceRequests = from u in userList
                                           join sr in serviceRequestsList on u.UserId equals sr.UserId into table1
                                           from sr in table1.ToList()
                                           join sa in serviceRequestAddressesList on sr.ServiceRequestId equals sa.ServiceRequestId into table2
                                           from sa in table2.ToList()
                                           where sr.Status == ValuesData.SERVICE_PENDING 
                                           select new NewServiceRequestViewModel { addressViewModel = sa, user = u, serviceRequestViewModel = sr };
            //var resultNewServiceRequests = from sr in serviceRequestsList
            //                               join u in userList on sr.UserId equals u.UserId into table1
            //                               from u in table1.DefaultIfEmpty()
            //                               join sa in serviceRequestAddressesList on sr.ServiceRequestId equals sa.ServiceRequestId into table2
            //                               from sa in table2.DefaultIfEmpty()
            //                               where sr.Status == ValuesData.SERVICE_PENDING
            //                               select new NewServiceRequestViewModel { addressViewModel = sa, user = u, serviceRequestViewModel = sr };



            ViewBag.Hamburger = "serviceProvider";
            return View(resultNewServiceRequests);
        }

        public PartialViewResult NewServiceRequestTable(Boolean checkVal)
        {
            
            List<User> userList = _helperlandContext.User.ToList();
            List<ServiceRequest> serviceRequestsList = _helperlandContext.ServiceRequest.ToList();
            List<ServiceRequestAddress> serviceRequestAddressesList = _helperlandContext.ServiceRequestAddress.ToList();

            var resultNewServiceRequests = from u in userList
                                           join sr in serviceRequestsList on u.UserId equals sr.UserId into table1
                                           from sr in table1.ToList()
                                           join sa in serviceRequestAddressesList on sr.ServiceRequestId equals sa.ServiceRequestId into table2
                                           from sa in table2.ToList()
                                           where sr.Status == ValuesData.SERVICE_PENDING && ( sr.HasPets == checkVal || sr.HasPets ==false)
                                          
                                           select new NewServiceRequestViewModel { addressViewModel = sa, user = u, serviceRequestViewModel = sr };
            //var resultNewServiceRequests = from sr in serviceRequestsList
            //                               join u in userList on sr.UserId equals u.UserId into table1
            //                               from u in table1.DefaultIfEmpty()
            //                               join sa in serviceRequestAddressesList on sr.ServiceRequestId equals sa.ServiceRequestId into table2
            //                               from sa in table2.DefaultIfEmpty()
            //                               where sr.Status == ValuesData.SERVICE_PENDING
            //                               select new NewServiceRequestViewModel { addressViewModel = sa, user = u, serviceRequestViewModel = sr };

           
            return PartialView( resultNewServiceRequests);
        }


        public PartialViewResult ServiceRequestDetail(int Id , bool? UPService)
        {
            ServiceRequest serviceRequests = _helperlandContext.ServiceRequest.Include(x => x.ServiceRequestAddress).Include(x => x.ServiceRequestExtra).Where(x => x.ServiceRequestId == Id).FirstOrDefault();
            if (UPService == true)
            {
                ViewBag.UPService = true;
            }
            return PartialView(serviceRequests);
        }

        public JsonResult AcceptBtnFun(int Id)
        {
            PartialPopUP partialPopUP = new PartialPopUP();
            var isConflict = false;
            var conflictServiceId = 0;
            //fetch service details from the database with given service request id
            ServiceRequest serviceRequest = _helperlandContext.ServiceRequest.Include(x => x.User).FirstOrDefault(x => x.ServiceRequestId == Id);
            var day = serviceRequest.ServiceStartDate.ToString("dd-MM-yyyy");
            var time = serviceRequest.ServiceStartDate.ToString("HH:mm:ss");
            var actual = day + " " + time;
            DateTime newStartDate = DateTime.Parse(actual);

            //if service is not accepted by any service provider yet then reschedule the request
            if (serviceRequest.ServiceProvider == null && serviceRequest.Status == ValuesData.SERVICE_PENDING)
            {
                isConflict = false;
                var newStartTime = newStartDate;
                var newEndTime = newStartTime.AddHours(serviceRequest.ServiceHours);
                var userId = Int32.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));



                //fetch all the request from database which are not completed yet with same service provider id 
                IEnumerable <ServiceRequest> serviceRequests = _helperlandContext.ServiceRequest.Where(x => x.ServiceProviderId == userId && x.Status == ValuesData.SERVICE_ACCEPTED  && x.ServiceRequestId != Id).ToList();
                foreach (var request in serviceRequests)
                {
                    var oldStartTime = request.ServiceStartDate;
                    var oldEndTime = oldStartTime.AddHours(request.ServiceHours);
                    
                    //check time conflicts
                    if ((request.ServiceStartDate == newStartDate) || (((newStartTime > oldStartTime) && (newStartTime < oldEndTime)) || ((newEndTime > oldStartTime) && (newEndTime < oldEndTime))))
                    {
                        isConflict = true;
                        conflictServiceId = request.ServiceRequestId;
                        break;
                    }
                    newStartTime = newStartDate.AddHours(1);
                    newEndTime = newStartTime.AddHours(serviceRequest.ServiceHours + 1);
                    if ((request.ServiceStartDate == newStartDate) || (((newStartTime > oldStartTime) && (newStartTime < oldEndTime)) || ((newEndTime > oldStartTime) && (newEndTime < oldEndTime))))
                    {
                        isConflict = true;
                        conflictServiceId = request.ServiceRequestId;
                        break;
                    }
                }

                if (isConflict)
                {
                    
                    partialPopUP.Message= "Another service request"+ conflictServiceId + "has already been assigned which has time overlap with this service request. You can’t pick this one!";
                    partialPopUP.ImgSrc = "/images/big-error.jpg";

                    return Json(partialPopUP);
                }
                string provider = HttpContext.Session.GetString("CurrentUser");
                User user = JsonConvert.DeserializeObject<User>(provider);
                //serviceRequest.ServiceStartDate = newStartDate;
                serviceRequest.ModifiedDate = DateTime.Now;
                serviceRequest.ServiceProviderId=user.UserId;
                serviceRequest.SpacceptedDate = DateTime.Now;
                serviceRequest.Status = ValuesData.SERVICE_ACCEPTED;
                serviceRequest.ModifiedBy = user.UserId;
                _helperlandContext.ServiceRequest.Update(serviceRequest);
                _helperlandContext.SaveChanges();
                //return and show success message to customer
                 partialPopUP.Message = "Thank You For Accepting Service , Good Day";
                 partialPopUP.ImgSrc = "/images/big-right.png";

               
                
                //send email to the service provider about service reschedule
                List<string> emailList = new List<string>();
                emailList.Add(serviceRequest.User.Email);
                //helperlandContext.Users.Where(user => user.UserId == serviceRequest.ServiceProviderId).Select(user => user.Email).ToList();
                string subject = "Request Accepted by Service Provider";
                string body = "Service Request " + serviceRequest.ServiceRequestId + " Accepted by the Service Provider ,Thank you " + newStartDate.ToString();
                EmailManager.SendEmail(emailList, subject, body);
                //return and show success message to customer


                return Json(partialPopUP);

            }
            //otherwise check time conflicts
            else
            {

                partialPopUP.Message = "This Service Is No More Available";
                partialPopUP.ImgSrc = "/images/big-error.jpg";

                return Json(partialPopUP);


            }

           
        }

        public PartialViewResult PopUpFun(string Message,string ImgSrc)
        {
            PartialPopUP partialPopUP = new PartialPopUP();
            partialPopUP.Message = Message;
            partialPopUP.ImgSrc = ImgSrc;

            return PartialView(partialPopUP);
        }

        public IActionResult SPServiceHistory()
        {
            IEnumerable<ServiceRequest> serviceRequests=_helperlandContext.ServiceRequest.Include(x=>x.ServiceRequestAddress).Include(x=>x.User).Where(x=>x.Status==ValuesData.SERVICE_COMPLETED || x.Status==ValuesData.SERVICE_REFUNDED).ToList();
            ViewBag.Hamburger = "serviceProvider";
            return View(serviceRequests);
        }

        public PartialViewResult SPServiceHistoryStaus(int Id)
        {
            if ( Id == 0){
                IEnumerable<ServiceRequest> serviceRequests = _helperlandContext.ServiceRequest.Include(x => x.ServiceRequestAddress).Include(x => x.User).Where(x => x.Status == ValuesData.SERVICE_COMPLETED || x.Status == ValuesData.SERVICE_REFUNDED).ToList();
                ViewBag.Hamburger = "serviceProvider";
                return PartialView(serviceRequests);
            }
            else
            {
                IEnumerable<ServiceRequest> serviceRequests = _helperlandContext.ServiceRequest.Include(x => x.ServiceRequestAddress).Include(x => x.User).Where(x => x.Status == Id).ToList();
                ViewBag.Hamburger = "serviceProvider";
                return PartialView(serviceRequests);
            }

            
        }

        public IActionResult SPUpcomingService()
        {
            IEnumerable<ServiceRequest> serviceRequests = _helperlandContext.ServiceRequest.Include(x => x.ServiceRequestAddress).Include(x => x.User).Where(x => x.Status == ValuesData.SERVICE_ACCEPTED).ToList();
            ViewBag.Hamburger = "serviceProvider";
            return View(serviceRequests);
           
        }

        public PartialViewResult SPUpcomingServiceTable()
        {
            IEnumerable<ServiceRequest> serviceRequests = _helperlandContext.ServiceRequest.Include(x => x.ServiceRequestAddress).Include(x => x.User).Where(x => x.Status == ValuesData.SERVICE_ACCEPTED).ToList();
            ViewBag.Hamburger = "serviceProvider";
            return PartialView(serviceRequests);

        }

        public JsonResult CancleServiceSP(int Id)
        {
            PartialPopUP partialPopUP = new PartialPopUP();
           
            //fetch service details from the database with given service request id
            ServiceRequest serviceRequest = _helperlandContext.ServiceRequest.Include(x => x.User).FirstOrDefault(x => x.ServiceRequestId == Id);
           
                string provider = HttpContext.Session.GetString("CurrentUser");
                User user = JsonConvert.DeserializeObject<User>(provider);
                //serviceRequest.ServiceStartDate = newStartDate;
                serviceRequest.ModifiedDate = DateTime.Now;
               
                serviceRequest.Status = ValuesData.SERVICE_CANCELLED;
                serviceRequest.ModifiedBy = user.UserId;
                _helperlandContext.ServiceRequest.Update(serviceRequest);
                _helperlandContext.SaveChanges();
                //return and show success message to customer
                partialPopUP.Message = "Your Service Request is Cancled";
                partialPopUP.ImgSrc = "/images/big-right.png";



                //send email to the service provider about service reschedule
                List<string> emailList = new List<string>();
                emailList.Add(serviceRequest.User.Email);
                //helperlandContext.Users.Where(user => user.UserId == serviceRequest.ServiceProviderId).Select(user => user.Email).ToList();
                string subject = "Request id Cancled by Service Provider";
                string body = "Service Request " + serviceRequest.ServiceRequestId + " Cancled by the Service Provider  " ;
                EmailManager.SendEmail(emailList, subject, body);
                //return and show success message to customer


                return Json(partialPopUP);

            
            //otherwise check time conflicts
           
            
        }

        public JsonResult CompletedServiceSP(int Id)
        {
            PartialPopUP partialPopUP = new PartialPopUP();

            //fetch service details from the database with given service request id
            ServiceRequest serviceRequest = _helperlandContext.ServiceRequest.Include(x => x.User).FirstOrDefault(x => x.ServiceRequestId == Id);

            string provider = HttpContext.Session.GetString("CurrentUser");
            User user = JsonConvert.DeserializeObject<User>(provider);
            //serviceRequest.ServiceStartDate = newStartDate;
            serviceRequest.ModifiedDate = DateTime.Now;

            serviceRequest.Status = ValuesData.SERVICE_COMPLETED;
            serviceRequest.ModifiedBy = user.UserId;
            _helperlandContext.ServiceRequest.Update(serviceRequest);
            _helperlandContext.SaveChanges();
            //return and show success message to customer
            partialPopUP.Message = "Your Service Request is Completed";
            partialPopUP.ImgSrc = "/images/big-right.png";



            //send email to the service provider about service reschedule
            List<string> emailList = new List<string>();
            emailList.Add(serviceRequest.User.Email);
            //helperlandContext.Users.Where(user => user.UserId == serviceRequest.ServiceProviderId).Select(user => user.Email).ToList();
            //string subject = "Request id Cancled by Service Provider";
            //string body = "Service Request " + serviceRequest.ServiceRequestId + " Cancled by the Service Provider  ";
            //EmailManager.SendEmail(emailList, subject, body);
            //return and show success message to customer


            return Json(partialPopUP);
        }

        public IActionResult SPMyRating()
        {
            IEnumerable<Rating> rating = _helperlandContext.Rating.Include(x => x.ServiceRequest).Include(x => x.RatingFromNavigation).ToList();
            ViewBag.Hamburger = "serviceProvider";
            return View(rating);
        }

        //public PartialViewResult SPMyRatingTable()
        //{
        //    IEnumerable<Rating> rating = _helperlandContext.Rating.Include(x => x.ServiceRequest).Include(x => x.RatingFromNavigation).ToList();
        //    return PartialView("MyRatingTablePar", rating);
        //}

        public IActionResult BlockCustomer()
        {
            string provider = HttpContext.Session.GetString("CurrentUser");
            User user = JsonConvert.DeserializeObject<User>(provider);
            IEnumerable<ServiceRequest> serviceRequests = _helperlandContext.ServiceRequest.Include(x => x.User).Where(x=>x.ServiceProviderId== user.UserId && x.Status==ValuesData.SERVICE_COMPLETED).ToList();
            ViewBag.Hamburger = "serviceProvider";
            return View(serviceRequests);
        }

        public JsonResult BlockCustomerTable()
        {
            string provider = HttpContext.Session.GetString("CurrentUser");
            User user = JsonConvert.DeserializeObject<User>(provider);
            ServiceRequest serviceRequests = _helperlandContext.ServiceRequest.Include(x => x.User).Where(x => x.ServiceProviderId == user.UserId && x.Status == ValuesData.SERVICE_COMPLETED).FirstOrDefault();

            ViewBag.Hamburger = "serviceProvider";
            return Json(serviceRequests);
        }

        public JsonResult BlockCustomerFun(int Id)
        {
            User user=_helperlandContext.User.Where(x=>x.UserId==Id).FirstOrDefault();
            user.Status = ValuesData.CUSTOMER_BLOK;
            _helperlandContext.User.Update(user);
            _helperlandContext.SaveChanges();
            PartialPopUP partialPopUP = new PartialPopUP();
            partialPopUP.Message = "You Blocked " + user.FirstName +user.LastName;
            partialPopUP.ImgSrc = "/images/big-right.png";
            return Json(partialPopUP);
        }
    }
}
