using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Helperland.Models;
using Helperland.ViewModels;
using Helperland.ViewModels.ServiceProvider;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Helperland.Controllers
{
    [Authorize(Roles = "2")]
    public class ServiceProviderController : Controller
    {
        public HelperlandContext _helperlandContext;
       
        public ServiceProviderController(HelperlandContext helperlandContext)
        {
            _helperlandContext = helperlandContext;
        }
        // GET: /<controller>/
        [AllowAnonymous]
        [HttpGet]
        public IActionResult Register_ServiceProvider()
        {
            return View();
        }
        [AllowAnonymous]
        //Email is alredy reg or not
        public bool IsEmailExists(string email)
        {
            var IsCheck = _helperlandContext.User.Where(e => e.Email == email).FirstOrDefault();
            return IsCheck != null;
        }
        [AllowAnonymous]
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
                        Password = model.Password,
                        Mobile = model.Mobile,
                        CreatedDate = DateTime.Now,
                        ModifiedDate = DateTime.Now,
                        UserTypeId = 2,
                        IsApproved = false,
                        IsActive =false,

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
            //bool isBlocked = false;
            //int spId = Int16.Parse(User.Claims.FirstOrDefault(x => x.Type == "userId").Value);

            //IEnumerable<FavoriteAndBlocked> blockedCustomerList = _helperlandContext.FavoriteAndBlocked.Where(x => x.UserId == spId && x.IsBlocked == true).Distinct().ToList();

            //List<User> userList = _helperlandContext.User.ToList();
            //List<ServiceRequest> serviceRequestsList = _helperlandContext.ServiceRequest.ToList();
            //List<ServiceRequestAddress> serviceRequestAddressesList = _helperlandContext.ServiceRequestAddress.ToList();
            //List<FavoriteAndBlocked> favoriteAndBlockeds = _helperlandContext.FavoriteAndBlocked.ToList();
            //var resultNewServiceRequests = from u in userList
            //                               join sr in serviceRequestsList on u.UserId equals sr.UserId into table1
            //                               from sr in table1.ToList()
            //                               join sa in serviceRequestAddressesList on sr.ServiceRequestId equals sa.ServiceRequestId into table2
            //                               from sa in table2.ToList()
            //                               where sr.Status == ValuesData.SERVICE_PENDING && !blockedCustomerList.Any(x => x.TargetUserId == u.UserId)
            //                               select new NewServiceRequestViewModel { addressViewModel = sa, user = u, serviceRequestViewModel = sr };




            ViewBag.Hamburger = "serviceProvider";
            return View();
        }



        public PartialViewResult NewServiceRequestTable(Boolean? checkVal)
        {
            bool isBlocked = false;
            int spId = Int16.Parse(User.Claims.FirstOrDefault(x => x.Type == "userId").Value);

            IEnumerable<FavoriteAndBlocked> blockedCustomerList = _helperlandContext.FavoriteAndBlocked.Where(x => x.UserId == spId && x.IsBlocked == true).Distinct().ToList();
            IEnumerable<int> blockedByCustomerList = _helperlandContext.FavoriteAndBlocked.Where(x => x.TargetUserId == spId && x.IsBlocked == true).Select(x => x.UserId).Distinct().ToList();
            List<User> userList = _helperlandContext.User.ToList();
            List<ServiceRequest> serviceRequestsList = _helperlandContext.ServiceRequest.ToList();
            List<ServiceRequestAddress> serviceRequestAddressesList = _helperlandContext.ServiceRequestAddress.ToList();

            var resultNewServiceRequests = from u in userList
                                           join sr in serviceRequestsList on u.UserId equals sr.UserId into table1
                                           from sr in table1.ToList()
                                           join sa in serviceRequestAddressesList on sr.ServiceRequestId equals sa.ServiceRequestId into table2
                                           from sa in table2.ToList()
                                           where sr.Status == ValuesData.SERVICE_PENDING && !blockedCustomerList.Any(x => x.TargetUserId == u.UserId) && !blockedByCustomerList.Any(x=>x==u.UserId)

                                           select new NewServiceRequestViewModel { addressViewModel = sa, user = u, serviceRequestViewModel = sr };
            if (checkVal.HasValue)
            {
                resultNewServiceRequests=resultNewServiceRequests.Where(x=>x.serviceRequestViewModel.HasPets==checkVal || x.serviceRequestViewModel.HasPets == false);
            }
            //var resultNewServiceRequests = from sr in serviceRequestsList
            //                               join u in userList on sr.UserId equals u.UserId into table1
            //                               from u in table1.DefaultIfEmpty()
            //                               join sa in serviceRequestAddressesList on sr.ServiceRequestId equals sa.ServiceRequestId into table2
            //                               from sa in table2.DefaultIfEmpty()
            //                               where sr.Status == ValuesData.SERVICE_PENDING
            //                               select new NewServiceRequestViewModel { addressViewModel = sa, user = u, serviceRequestViewModel = sr };


            return PartialView(resultNewServiceRequests);
        }


        public PartialViewResult ServiceRequestDetail(int Id, bool? UPService )
        {
            ServiceRequest serviceRequests = _helperlandContext.ServiceRequest.Include(x => x.ServiceRequestAddress).Include(x => x.ServiceRequestExtra).Where(x => x.ServiceRequestId == Id).FirstOrDefault();
            if (UPService == true)
            {
                ViewBag.UPService = true;
            }
            
            HttpContext.Session.SetString("CurrentServiceRecordVersion",serviceRequests.RecordVersion.ToString());
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
            string recordVersion = HttpContext.Session.GetString("CurrentServiceRecordVersion");
            //if service is not accepted by any service provider yet then reschedule the request
            if (serviceRequest.ServiceProvider == null && serviceRequest.Status == ValuesData.SERVICE_PENDING && recordVersion==serviceRequest.RecordVersion.ToString())
            {
                isConflict = false;
                var newStartTime = newStartDate;
                var newEndTime = newStartTime.AddHours(serviceRequest.ServiceHours);
               // var userId = Int32.Parse(User.FindFirstValue(ClaimTypes."userId"));
                int userId = Int16.Parse(User.Claims.FirstOrDefault(x => x.Type == "userId").Value);


                //fetch all the request from database which are not completed yet with same service provider id 
                IEnumerable<ServiceRequest> serviceRequests = _helperlandContext.ServiceRequest.Where(x => x.ServiceProviderId == userId && x.Status == ValuesData.SERVICE_ACCEPTED && x.ServiceRequestId != Id).ToList();
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

                    partialPopUP.Message = "Another service request " + conflictServiceId + " has already been assigned which has time overlap with this service request. You can’t pick this one!";
                    partialPopUP.ImgSrc = "/images/big-error.jpg";

                    return Json(partialPopUP);
                }
                string provider = HttpContext.Session.GetString("CurrentUser");
                User user = JsonConvert.DeserializeObject<User>(provider);
                //serviceRequest.ServiceStartDate = newStartDate;
                serviceRequest.ModifiedDate = DateTime.Now;
                serviceRequest.ServiceProviderId = user.UserId;
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

        public PartialViewResult PopUpFun(string Message, string ImgSrc)
        {
            PartialPopUP partialPopUP = new PartialPopUP();
            partialPopUP.Message = Message;
            partialPopUP.ImgSrc = ImgSrc;

            return PartialView(partialPopUP);
        }

        public IActionResult SPServiceHistory()
        {
            int id = Int16.Parse(User.Claims.FirstOrDefault(x => x.Type == "userId").Value);
            IEnumerable<ServiceRequest> serviceRequests = _helperlandContext.ServiceRequest.Include(x => x.ServiceRequestAddress).Include(x => x.User).Where(x => (x.Status == ValuesData.SERVICE_COMPLETED || x.Status == ValuesData.SERVICE_REFUNDED) && x.ServiceProviderId == id).ToList();
            ViewBag.Hamburger = "serviceProvider";
            return View(serviceRequests);
        }

        public PartialViewResult SPServiceHistoryStaus(int Id)
        {
            int id = Int16.Parse(User.Claims.FirstOrDefault(x => x.Type == "userId").Value);
            if (Id == 0) {
                IEnumerable<ServiceRequest> serviceRequests = _helperlandContext.ServiceRequest.Include(x => x.ServiceRequestAddress).Include(x => x.User).Where(x => (x.Status == ValuesData.SERVICE_COMPLETED || x.Status == ValuesData.SERVICE_REFUNDED) && x.ServiceProviderId == id).ToList();
                ViewBag.Hamburger = "serviceProvider";
                return PartialView(serviceRequests);
            }
            else
            {
                IEnumerable<ServiceRequest> serviceRequests = _helperlandContext.ServiceRequest.Include(x => x.ServiceRequestAddress).Include(x => x.User).Where(x => x.Status == Id && x.ServiceProviderId == id).ToList();
                ViewBag.Hamburger = "serviceProvider";
                return PartialView(serviceRequests);
            }


        }

        public IActionResult SPUpcomingService()
        {
            int id = Int16.Parse(User.Claims.FirstOrDefault(x => x.Type == "userId").Value);
            IEnumerable<ServiceRequest> serviceRequests = _helperlandContext.ServiceRequest.Include(x => x.ServiceRequestAddress).Include(x => x.User).Where(x => x.Status == ValuesData.SERVICE_ACCEPTED && x.ServiceProviderId==id).ToList();
            ViewBag.Hamburger = "serviceProvider";
            return View(serviceRequests);

        }

        public PartialViewResult SPUpcomingServiceTable()
        {
            int id = Int16.Parse(User.Claims.FirstOrDefault(x => x.Type == "userId").Value);
            IEnumerable<ServiceRequest> serviceRequests = _helperlandContext.ServiceRequest.Include(x => x.ServiceRequestAddress).Include(x => x.User).Where(x => x.Status == ValuesData.SERVICE_ACCEPTED && x.ServiceProviderId == id).ToList();
            ViewBag.Hamburger = "serviceProvider";
            return PartialView(serviceRequests);

        }

        public JsonResult CancleServiceSP(int Id)
        {
            PartialPopUP partialPopUP = new PartialPopUP();

            //fetch service details from the database with given service request id
            ServiceRequest serviceRequest = _helperlandContext.ServiceRequest.Include(x => x.User).FirstOrDefault(x => x.ServiceRequestId == Id);
            int spID = Int16.Parse(User.Claims.FirstOrDefault(x => x.Type == "userId").Value);
            

            var day = serviceRequest.ServiceStartDate.ToString("dd-MM-yyyy");
            var time = serviceRequest.ServiceStartDate.ToString("HH:mm:ss");
            var actual = day + " " + time;
            DateTime newStartDate = DateTime.Parse(actual);
            var currentDate = DateTime.Today;
            var newStartTime = newStartDate;
            var newEndTime = newStartTime.AddHours(serviceRequest.ServiceHours);

            if ((((newStartTime < DateTime.Now) && (newEndTime > DateTime.Now))) && currentDate==DateTime.Today)
            {
                partialPopUP.Message = "Your Service Request is not Cancled";
                partialPopUP.ImgSrc = "/images/big-error.jpg";
                return Json(partialPopUP);
            }
            string provider = HttpContext.Session.GetString("CurrentUser");
            User user = JsonConvert.DeserializeObject<User>(provider);
            //serviceRequest.ServiceStartDate = newStartDate;
            serviceRequest.ModifiedDate = DateTime.Now;

            serviceRequest.Status = ValuesData.SERVICE_PENDING;
            serviceRequest.ServiceProviderId = null;
            serviceRequest.ModifiedBy = spID;
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
            string body = "Service Request " + serviceRequest.ServiceRequestId + " Cancled by the Service Provider  ";
            EmailManager.SendEmail(emailList, subject, body);
            //return and show success message to customer


            return Json(partialPopUP);


            //otherwise check time conflicts


        }

        public JsonResult CompletedServiceSP(int Id)
        {
            int id = Int16.Parse(User.Claims.FirstOrDefault(x => x.Type == "userId").Value);
            PartialPopUP partialPopUP = new PartialPopUP();

            //fetch service details from the database with given service request id
            ServiceRequest serviceRequest = _helperlandContext.ServiceRequest.Include(x => x.User).FirstOrDefault(x => x.ServiceRequestId == Id);

            //serviceRequest.ServiceStartDate = newStartDate;
            serviceRequest.ModifiedDate = DateTime.Now;

            serviceRequest.Status = ValuesData.SERVICE_COMPLETED;
            serviceRequest.ModifiedBy = id;
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
            int id = Int16.Parse(User.Claims.FirstOrDefault(x => x.Type == "userId").Value);
            IEnumerable<Rating> rating = _helperlandContext.Rating.Include(x => x.ServiceRequest).Include(x => x.RatingFromNavigation).Where(x=>x.RatingTo==id).ToList();
            ViewBag.Hamburger = "serviceProvider";
            return View(rating);
        }

        public PartialViewResult SPMyRatingTable()
        {
            int id = Int16.Parse(User.Claims.FirstOrDefault(x => x.Type == "userId").Value);
            IEnumerable<Rating> rating = _helperlandContext.Rating.Include(x => x.ServiceRequest).Include(x => x.RatingFromNavigation).Where(x => x.RatingTo == id).ToList();
            return PartialView("MyRatingTablePar", rating);
        }

        public IActionResult BlockCustomer()
        {
            string provider = HttpContext.Session.GetString("CurrentUser");
                User user = JsonConvert.DeserializeObject<User>(provider);
            BlockCustomerViewModel blockCustomerViewModel = new BlockCustomerViewModel();
            blockCustomerViewModel.allCustomers = _helperlandContext.ServiceRequest.Include(x => x.User).Where(x => x.Status == ValuesData.SERVICE_COMPLETED && x.ServiceProviderId == user.UserId).Select(x => x.User).Distinct().AsEnumerable();
            blockCustomerViewModel.blockedCustomers = _helperlandContext.FavoriteAndBlocked.Include(x => x.TargetUser).Where(x => x.UserId == user.UserId && x.IsBlocked == true).Select(x => x.TargetUser).ToList();

            ViewBag.Hamburger = "serviceProvider";
            return View(blockCustomerViewModel);
        }

        [HttpPost]
        public JsonResult BlockCustomer(int customerId)
        {

            int SerProID = Int16.Parse(User.Claims.FirstOrDefault(x => x.Type == "userId").Value);
            FavoriteAndBlocked favoriteAndBlocked = _helperlandContext.FavoriteAndBlocked.FirstOrDefault(x => x.UserId == SerProID && x.TargetUserId == customerId);
            if (favoriteAndBlocked == null)
            {
                favoriteAndBlocked = new FavoriteAndBlocked();
                favoriteAndBlocked.TargetUserId = customerId;
                favoriteAndBlocked.UserId = SerProID;
                favoriteAndBlocked.IsBlocked = true;
                favoriteAndBlocked.IsFavorite = false;
                _helperlandContext.FavoriteAndBlocked.Add(favoriteAndBlocked);
                _helperlandContext.SaveChanges();
                return Json("ok");
            }
            else
            {
                favoriteAndBlocked.IsBlocked = true;
                favoriteAndBlocked.IsFavorite = false;
                _helperlandContext.FavoriteAndBlocked.Update(favoriteAndBlocked);
                _helperlandContext.SaveChanges();
                return Json("ok");
            }
        }

        [HttpPost]
        public JsonResult unblockCustomer(int customerId)
        {
            int spID = Int16.Parse(User.Claims.FirstOrDefault(x => x.Type == "userId").Value);
            FavoriteAndBlocked favoriteAndBlocked = _helperlandContext.FavoriteAndBlocked.FirstOrDefault(x => x.UserId == spID && x.TargetUserId == customerId);
            favoriteAndBlocked.IsBlocked = false;
            favoriteAndBlocked.IsFavorite = true;
            _helperlandContext.FavoriteAndBlocked.Update(favoriteAndBlocked);
            _helperlandContext.SaveChanges();
            return Json("ok");
        }

        public IActionResult SPAccount()
        {

            ViewBag.Hamburger = "serviceProvider";
            return View();
        }

        public PartialViewResult SPDetail()
        {
            //get details of logged in service provider
            int id = Int16.Parse(User.Claims.FirstOrDefault(x => x.Type == "userId").Value);
            User user = _helperlandContext.User.Include(x => x.UserAddress.Where(x => x.IsDefault == true)).FirstOrDefault(x => x.UserId == id);
            SPDetailViewModel model = new SPDetailViewModel();
            model.IsActive = user.IsActive;
            model.UserProfilePicture = user.UserProfilePicture;
            model.FirstName = user.FirstName;
            model.LastName = user.LastName;
            model.Email = user.Email;
            model.Mobile = user.Mobile;
            model.Nationality = user.NationalityId;
            model.Gender = user.Gender;
            if (user.DateOfBirth != null)
            {
                var DOB = user.DateOfBirth.Value.ToString("dd/MMMM/yyyy").Split("-");
                model.BirthDay = DOB[0];
                model.BirthMonth = DOB[1];
                model.BirthYear = DOB[2];
            }
            if (user.UserAddress.Count == 1)
            {
                model.AddressId = user.UserAddress.ElementAt(0).AddressId;
                model.StreetName = user.UserAddress.ElementAt(0).AddressLine1;
                model.HouseNumber = user.UserAddress.ElementAt(0).AddressLine2;
                model.City = user.UserAddress.ElementAt(0).City;
                model.PostalCode = user.ZipCode;
            }
            //return partialview with all the details
            return PartialView(model);
        }

        [HttpPost]
        public PartialViewResult SPDetail(SPDetailViewModel model)
        {
            if (ModelState.IsValid)
            {

                int id = Int16.Parse(User.Claims.FirstOrDefault(x => x.Type == "userId").Value);
                User user = _helperlandContext.User.Include(x => x.UserAddress.Where(x => x.IsDefault == true)).FirstOrDefault(x => x.UserId == id);
                IEnumerable<UserAddress> userAddressFatch = _helperlandContext.UserAddress.Where(x => x.UserId == id).ToList();
                UserAddress userAddress = new UserAddress();
                string DOB = model.BirthDay + "-" + model.BirthMonth + "-" + model.BirthYear;
                user.DateOfBirth = DateTime.Parse(DOB);
                user.UserProfilePicture = model.UserProfilePicture;
                user.FirstName = model.FirstName;
                user.LastName = model.LastName;
                user.Email = model.Email;
                user.Mobile = model.Mobile;
                user.NationalityId = model.Nationality;
                user.Gender = model.Gender;
                if (user.UserAddress.Count == 1)
                {
                    user.UserAddress.ElementAt(0).AddressLine1 = model.StreetName;
                    user.UserAddress.ElementAt(0).AddressLine2 = model.HouseNumber;
                    user.UserAddress.ElementAt(0).City = model.City;
                    user.UserAddress.ElementAt(0).PostalCode = model.PostalCode;
                    user.ZipCode = model.PostalCode;
                }
                else
                {
                    userAddress.AddressLine1 = model.StreetName;
                    userAddress.AddressLine2 = model.HouseNumber;
                    userAddress.City = model.City;
                    userAddress.PostalCode = model.PostalCode;
                    userAddress.PostalCode = model.PostalCode;
                    user.ZipCode = model.PostalCode;
                    userAddress.IsDefault = true;
                    userAddress.UserId = id;
                    userAddress.IsDeleted = false;
                    if (userAddressFatch.Any(x => x.IsDefault == true))
                    {
                        userAddress.IsDefault = false;

                    }
                    else
                    {
                        userAddress.IsDefault = true;
                        user.ZipCode = model.PostalCode;
                        _helperlandContext.User.Update(user);
                    }
                    _helperlandContext.UserAddress.Add(userAddress);

                }

                //update details in the database
                _helperlandContext.User.Update(user);
                _helperlandContext.SaveChanges();
                user.UserAddress = null;
                //update user details in session
                HttpContext.Session.SetString("CurrentUser", JsonConvert.SerializeObject(user));
                var message = "Details Updated successfully..";
                ViewBag.Alert = "<div class='alert alert-success alert-dismissible fade show' role='alert'>" + message + "<button type= 'button' class='btn-close' data-bs-dismiss='alert' aria-label='Close'></button></div>";
                return PartialView(model);
            }
            else
            {
                return PartialView(model);
            }
        }

        [HttpPost]
        public PartialViewResult SPChangePassword(SPChangePasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                //fetch user details from session
                string currentUser = HttpContext.Session.GetString("CurrentUser");
                User user = JsonConvert.DeserializeObject<User>(currentUser);
                //update user's password into the database if current password is correctly entered,otherwise return partialview with error message
                if (user.Password.Equals(model.CurrentPassword))
                {
                    user.Password = model.ConfirmPassword;
                    _helperlandContext.User.Update(user);
                    _helperlandContext.SaveChanges();
                    HttpContext.Session.SetString("CurrentUser", JsonConvert.SerializeObject(user));
                    model = null;
                    var message = "Password changed successfully..";
                    ViewBag.Alert = "<div class='alert alert-success alert-dismissible fade show' role='alert'>" + message + "<button type= 'button' class='btn-close' data-bs-dismiss='alert' aria-label='Close'></button></div>";
                    return PartialView(model);
                }
                else
                {
                    var message = "Incorrect old password";
                    ViewBag.Alert = "<div class='alert alert-danger alert-dismissible fade show' role='alert'>" + message + "<button type= 'button' class='btn-close' data-bs-dismiss='alert' aria-label='Close'></button></div>";
                    return PartialView(model);
                }
            }
            else
            {
                return PartialView(model);
            }
        }

        public IActionResult ServiceSchedual()
        {
            ViewBag.Hamburger = "serviceProvider";
            return View(); 
            
        }
        public JsonResult ServiceSchedualCalander()
        {

            int id = Int16.Parse(User.Claims.FirstOrDefault(x => x.Type == "userId").Value);
            var requestList = _helperlandContext.ServiceRequest.Where(x => x.ServiceProviderId == id && (x.Status == ValuesData.SERVICE_COMPLETED || x.Status == ValuesData.SERVICE_ACCEPTED)).ToList();
            List<Object> objects = new List<Object>();
            foreach (var request in requestList)
            {
                var color = "";
                if (request.Status == ValuesData.SERVICE_ACCEPTED)
                {
                    color = "#1d7a8c";
                }
                else
                {
                    color = "#67b644";
                }
                var v = new { title = request.ServiceStartDate.ToString("HH:mm") + " - " + request.ServiceStartDate.AddHours(request.ServiceHours).ToString("HH:mm"), color = color, start = request.ServiceStartDate, id = request.ServiceRequestId };
                objects.Add(v);
            }
            return Json(new { data = objects });


        }



    }
}
