using Helperland.Models;
using Helperland.ViewModels;
using Helperland.ViewModels.CustomerService;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;

using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using Newtonsoft.Json;

namespace Helperland.Controllers
{
    public class CustomerServiceController : Controller
    {
        public HelperlandContext _helperlandContext;

        readonly SqlConnection con;

        readonly IConfigurationRoot configuration;
        SqlCommand com = new SqlCommand();
        SqlDataReader dr;
        List<ServiceRequest> _serviceRequests = new List<ServiceRequest>();
        List<CustomeDashbordViewModel> _customeDashbordViewModel = new List<CustomeDashbordViewModel>();
  
        List<ServiceDetailViewModel> _serviceDetailViewModels = new List<ServiceDetailViewModel>();

        public CustomerServiceController(IHostingEnvironment env,HelperlandContext helperlandContext)
        {
            _helperlandContext = helperlandContext;
            con = new SqlConnection();
            configuration = new ConfigurationBuilder().SetBasePath(env.ContentRootPath).AddJsonFile("appsettings.json").Build();
            con.ConnectionString = configuration["ConnectionString"];
        }


        [HttpGet]
        public IActionResult CustomerDashboard()
        
        {
            fatchCustomerServiceData();
            return View(_customeDashbordViewModel);
        }

      
        public List<CustomeDashbordViewModel> fatchCustomerServiceData()
        {
           
            if (_customeDashbordViewModel.Count > 0)
            {
                _customeDashbordViewModel.Clear();
            }
            
            var userid = HttpContext.Session.GetString("User_Id");
            int userId = Int16.Parse(userid);
            
            ViewBag.UserName= HttpContext.Session.GetString("User_Name");
            con.Open();
            try
            {
                string sql = "SELECT sr.ServiceRequestId,sr.SubTotal,sr.ServiceStartDate,u.UserTypeId,sr.ServiceHours FROM ServiceRequest sr inner Join [User] u on sr.UserId = u.UserId where u.UserId='"+ userid +"' AND (sr.Status =1 OR sr.Status=2)";

                using (var sqlQuery = new SqlCommand(sql, con))
                {
                    sqlQuery.Prepare();
                    using (var sqlQueryResult = sqlQuery.ExecuteReader())
                    {
                        if (sqlQueryResult != null && sqlQueryResult.HasRows == true)
                        {
                           
                            while (sqlQueryResult.Read())
                            {

                                CustomeDashbordViewModel objCustomeDashbordViewModel = new CustomeDashbordViewModel();
                                objCustomeDashbordViewModel.ServiceRequestId = sqlQueryResult.GetInt32(0);
                                objCustomeDashbordViewModel.SubTotal = sqlQueryResult.GetDecimal(1);
                                objCustomeDashbordViewModel.ServiceStartDate = sqlQueryResult.GetDateTime(2);
                                objCustomeDashbordViewModel.UserTypeId = sqlQueryResult.GetInt32(3);
                                objCustomeDashbordViewModel.ServiceHours = Convert.ToSingle(sqlQueryResult["ServiceHours"]);
                                

                                _customeDashbordViewModel.Add(objCustomeDashbordViewModel);
                            }
                        }
                    }
                }

                //return _customeDashbordViewModel;
                con.Close();
                return _customeDashbordViewModel;
            }
            
            catch (Exception exc)
            {
                throw exc;
            }
        }

        [HttpGet]
        public PartialViewResult ServiceDetailPartialView(int id)
        {


            //return Ok();

            ServiceRequest serviceRequest = _helperlandContext.ServiceRequest.Include(x => x.ServiceRequestAddress).Include(x => x.ServiceRequestExtra).FirstOrDefault(x => x.ServiceRequestId == id);
            //returns partialview with service detail
            //fatchServiceDetail(id);
            return PartialView(serviceRequest);
        }

       

        [HttpGet]
        public PartialViewResult RescheduleService(int id)
        {
            RescheduleServiceViewModel model = new RescheduleServiceViewModel();
            model.ServiceRequestId = id;
            //return partialview for service rescheduling
            return PartialView(model);
        }
        [HttpPost]
        public PartialViewResult RescheduleService(RescheduleServiceViewModel model)
        {
            var isConflict = false;
            //fetch service details from the database with given service request id
            ServiceRequest serviceRequest = _helperlandContext.ServiceRequest.Include(x => x.ServiceProvider).FirstOrDefault(x => x.ServiceRequestId == model.ServiceRequestId);
            var day = model.ServiceDate.ToString("dd-MM-yyyy");
            var time = model.ServiceTime.ToString("hh:mm:ss");
            var actual = day + " " + time;
            DateTime newStartDate = DateTime.Parse(actual);

            //if service is not accepted by any service provider yet then reschedule the request
            if (serviceRequest.ServiceProvider == null)
            {
                serviceRequest.ServiceStartDate = newStartDate;
                serviceRequest.ModifiedDate = DateTime.Now;
                _helperlandContext.ServiceRequest.Update(serviceRequest);
                _helperlandContext.SaveChanges();
                //return and show success message to customer
                var message = "Reschedule successfully..";
                ViewBag.Alert = "<div class='alert alert-success alert-dismissible fade show' role='alert'>" + message + "<button type= 'button' class='btn-close' data-bs-dismiss='alert' aria-label='Close'></button></div>";
                return PartialView(model);
            }
            //otherwise check time conflicts
            else
            {
                var newStartTime = newStartDate;
                var newEndTime = newStartTime.AddHours(serviceRequest.ServiceHours);
                //fetch all the request from database which are not completed yet with same service provider id 
                IEnumerable<ServiceRequest> serviceRequests = _helperlandContext.ServiceRequest.Where(x => x.ServiceProviderId == serviceRequest.ServiceProviderId && x.Status == ValuesData.SERVICE_ACCEPTED && x.ServiceRequestId != serviceRequest.ServiceRequestId).ToList();
                foreach (var request in serviceRequests)
                {
                    var oldStartTime = request.ServiceStartDate;
                    var oldEndTime = oldStartTime.AddHours(request.ServiceHours);
                    isConflict = false;
                    //check time conflicts
                    if ((request.ServiceStartDate == newStartDate) || (((newStartTime > oldStartTime) && (newStartTime < oldEndTime)) || ((newEndTime > oldStartTime) && (newEndTime < oldEndTime))))
                    {
                        isConflict = true;
                        break;
                    }
                }
                //if time conflicts then show error message to customer othewise go ahead and reschedule request
                if (isConflict)
                {
                    var message = "Another service request has been assigned to the service provider on " + newStartDate.Date.ToString() + " from " + newStartTime.TimeOfDay.ToString() + " to" + newEndTime.TimeOfDay.ToString() + ". Either choose another date or pick up a different time slot.";
                    ViewBag.Alert = "<div class='alert alert-danger alert-dismissible fade show' role='alert'>" + message + "<button type= 'button' class='btn-close' data-bs-dismiss='alert' aria-label='Close'></button></div>";
                    return PartialView(model);
                }
                else
                {
                    serviceRequest.ServiceStartDate = newStartDate;
                    serviceRequest.ModifiedDate = DateTime.Now;
                    _helperlandContext.ServiceRequest.Update(serviceRequest);
                    _helperlandContext.SaveChanges();
                    //send email to the service provider about service reschedule
                    List<string> emailList = new List<string>();
                    emailList.Add(serviceRequest.ServiceProvider.Email);
                    //helperlandContext.Users.Where(user => user.UserId == serviceRequest.ServiceProviderId).Select(user => user.Email).ToList();
                    string subject = "Request Rescheduled!";
                    string body = "Service Request " + serviceRequest.ServiceRequestId + " has been rescheduled by customer. New date and time are " + newStartDate.ToString();
                    EmailManager.SendEmail(emailList, subject, body);
                    //return and show success message to customer
                    var message = "Reschedule successfully..";
                    ViewBag.Alert = "<div class='alert alert-success alert-dismissible fade show' role='alert'>" + message + "<button type= 'button' class='btn-close' data-bs-dismiss='alert' aria-label='Close'></button></div>";
                    return PartialView(model);
                }
            }
        }

        [HttpGet]
        public PartialViewResult CancelServiceRequest()
        {
            //return partialview for service cancellation
            return PartialView();
        }

        [HttpPost]
        public PartialViewResult CancelServiceRequest(int id, string comment)
        {
            //fetch specific service from database
            ServiceRequest serviceRequest = _helperlandContext.ServiceRequest.Include(x => x.ServiceProvider).FirstOrDefault(x => x.ServiceRequestId == id);
            //set message for cancel request
            serviceRequest.Comments = comment;
            //set status to cancelled service=4
            serviceRequest.Status = ValuesData.SERVICE_CANCELLED;
            serviceRequest.ModifiedDate = DateTime.Now;
            _helperlandContext.ServiceRequest.Update(serviceRequest);
            _helperlandContext.SaveChanges();
            //if request was accepted by any service provider then send email
            if (serviceRequest.ServiceProvider != null)
            {
                List<string> emailList = new List<string>();
                emailList.Add(serviceRequest.ServiceProvider.Email);
                //helperlandContext.Users.Where(user => user.UserId == serviceRequest.ServiceProviderId).Select(user => user.Email).ToList();
                string subject = "Request Cancelled!";
                string body = "Service Request " + serviceRequest.ServiceRequestId + " has been cancelled by customer.";
                EmailManager.SendEmail(emailList, subject, body);
            }
            //return and show success message to customer
            var message = "Request cancelled successfully..";
            ViewBag.Alert = "<div class='alert alert-success alert-dismissible fade show' role='alert'>" + message + "<button type= 'button' class='btn-close' data-bs-dismiss='alert' aria-label='Close'></button></div>";
            return PartialView();
        }

        public IActionResult ServiceHistory()
        {
            //return partialview with all the requests which are completed,cancelled or refunded.
            IEnumerable<ServiceRequest> serviceRequests = _helperlandContext.ServiceRequest.Include(x => x.ServiceProvider).ThenInclude(x => x.RatingRatingToNavigation).Where(x => x.Status != ValuesData.SERVICE_PENDING && x.Status != ValuesData.SERVICE_ACCEPTED).ToList();
            return View(serviceRequests);
        }

        [HttpGet]
        public PartialViewResult EditRating(int id)
        {
            Rating rating=_helperlandContext.Rating.Include(x=>x.RatingToNavigation).FirstOrDefault(x=>x.ServiceRequestId == id);
            RatingViewModel model=new RatingViewModel();
            var serviceProRate = _helperlandContext.Rating.Where(x => x.RatingTo == rating.RatingTo).ToList();
            decimal avgRat=0;
            foreach(var data in serviceProRate)
            {
                avgRat += data.Ratings;
            }
            decimal x = avgRat;
            model.ServiceRequestId =id;
            model.FirstName=rating.RatingToNavigation.FirstName;
            model.LastName=rating.RatingToNavigation.LastName;
            model.Ratings = avgRat; 
            model.OnTimeArrival = rating.OnTimeArrival;
            model.QualityOfService=rating.QualityOfService;
            model.Friendly=rating.Friendly;
            model.UserProfilePicture = rating.RatingToNavigation.UserProfilePicture;
            //IEnumerable<Rating>  rating = _helperlandContext.Rating.Include(x => x.RatingToNavigation).Where(x => x.ServiceRequestId == id).ToList();
           return PartialView(model);
        }
        [HttpPost]
        public JsonResult EditRating( RatingViewModel model)
        {
            Rating rating = _helperlandContext.Rating.FirstOrDefault(x => x.ServiceRequestId == model.ServiceRequestId);
            rating.RatingDate = DateTime.Now;
            rating.Ratings = model.Ratings;
            rating.OnTimeArrival = model.OnTimeArrival;
            rating.Friendly = model.Friendly;
            rating.QualityOfService = model.QualityOfService;
            rating.Comments = model.Comments;
            _helperlandContext.Rating.Update(rating);
            _helperlandContext.SaveChanges();
            return Json(" ");
        }

        public IActionResult MyAccount()
        {
            //returns view for My Account
            return View();
        }

        [HttpGet]
        public PartialViewResult MyDetails()
        {
            //fetch user details from the session
            string currentUser = HttpContext.Session.GetString("CurrentUser");
            User user = JsonConvert.DeserializeObject<User>(currentUser);
            MyDetailsViewModel model = new MyDetailsViewModel();
            model.FirstName = user.FirstName;
            model.LastName = user.LastName;
            model.Email = user.Email;
            model.Mobile = user.Mobile;
            model.LanguageId = user.LanguageId;
            if (user.DateOfBirth != null)
            {
                var DOB = user.DateOfBirth.Value.ToString("dd/MMMM/yyyy").Split("-");
                model.BirthDay = DOB[0];
                model.BirthMonth = DOB[1];
                model.BirthYear = DOB[2];
            }
            //return partialview with details
            return PartialView(model);
        }

        [HttpPost]
        public PartialViewResult MyDetails(MyDetailsViewModel model)
        {
            //fetch user details from session
            string currentUser = HttpContext.Session.GetString("CurrentUser");
            User user = JsonConvert.DeserializeObject<User>(currentUser);
            string DOB = model.BirthDay + "-" + model.BirthMonth + "-" + model.BirthYear;
            user.DateOfBirth = DateTime.Parse(DOB);
            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            user.Mobile = model.Mobile;
            user.LanguageId = model.LanguageId;
            //update details in the database
            _helperlandContext.User.Update(user);
            
            _helperlandContext.SaveChanges();
            //update user details in session
            HttpContext.Session.SetString("CurrentUser", JsonConvert.SerializeObject(user));
            var message = "Details Updated successfully..";
            ViewBag.Alert = "<div class='alert alert-success alert-dismissible fade show' role='alert'>" + message + "<button type= 'button' class='btn-close' data-bs-dismiss='alert' aria-label='Close'></button></div>";
            return PartialView(model);
        }

        [HttpGet]
        public PartialViewResult MyAddresses()
        {
            //return partialview with all the user addresses
            int id = Int16.Parse(User.Claims.FirstOrDefault(x => x.Type == "userId").Value);
            IEnumerable<UserAddress> userAddresses = _helperlandContext.UserAddress.Where(x => x.UserId == id).ToList();
            return PartialView(userAddresses);
        }

        [HttpGet]
        public PartialViewResult EditAddress(int? id)
        {
            //return partialview with details of one specific address
            if (id != null)
            {
                UserAddress userAddress = 
                    _helperlandContext.UserAddress.FirstOrDefault(x => x.AddressId == id);
                EditAddressViewModel model = new EditAddressViewModel();
                model.AddressId = userAddress.AddressId;
                model.StreetName = userAddress.AddressLine1;
                model.HouseNumber = userAddress.AddressLine2;
                model.PostalCode = userAddress.PostalCode;
                model.City = userAddress.City;
                model.PhoneNumber = userAddress.Mobile;
                ViewBag.Title = "Edit Address";
                ViewBag.EditBtn = "Edit Address";
                return PartialView(model);
            }
            else
            {
                ViewBag.EditBtn = "Add Address";
                ViewBag.Title = "Add New Address";
                return PartialView();
            }
        }

        [HttpPost]
        public PartialViewResult EditAddress(EditAddressViewModel model)
        {
            //check validations and update result in database or add new address into database 
            if (ModelState.IsValid)
            {
                if (model.AddressId != null)
                {
                    UserAddress userAddress = 
                        _helperlandContext.UserAddress.FirstOrDefault(x => x.AddressId == model.AddressId);
                    userAddress.AddressLine1 = model.StreetName;
                    userAddress.AddressLine2 = model.HouseNumber;
                    userAddress.PostalCode = model.PostalCode;
                    userAddress.City = model.City;
                    userAddress.Mobile = model.PhoneNumber;
                    
                    _helperlandContext.UserAddress.Update(userAddress);
                    _helperlandContext.SaveChanges();
                    return PartialView();
                }
                else
                {
                    UserAddress userAddress = new UserAddress();
                    userAddress.UserId = Int16.Parse(User.Claims.FirstOrDefault(x => x.Type == "userId").Value);
                    userAddress.AddressLine1 = model.StreetName;
                    userAddress.AddressLine2 = model.HouseNumber;
                    userAddress.PostalCode = model.PostalCode;
                    userAddress.City = model.City;
                    userAddress.Mobile = model.PhoneNumber;
                    _helperlandContext.UserAddress.Add(userAddress);
                    _helperlandContext.SaveChanges();
                    return PartialView();
                }
            }
            else
            {
                return PartialView(model);
            }
        }

        [HttpPost]
        public JsonResult DeleteAddress(int id)
        {
            //delete specific address from database
            UserAddress userAddress = _helperlandContext.UserAddress.FirstOrDefault(x => x.AddressId == id);
            _helperlandContext.UserAddress.Remove(userAddress);
            _helperlandContext.SaveChanges();
            return Json("done");
        }

        [HttpPost]
        public PartialViewResult ChangePassword(ChangePasswordViewModel model)
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






    }
}
