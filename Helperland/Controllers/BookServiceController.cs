﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;   //install package
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Helperland.Models;
using Helperland.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using static Helperland.ViewModels.BookServiceViewModel;

namespace Helperland.Controllers
{
    [Authorize(Roles = "3")]
    public class BookServiceController : Controller
    {
        public HelperlandContext _helperlandContext;
       
        readonly SqlConnection con;
      
        readonly IConfigurationRoot configuration;
        SqlCommand com = new SqlCommand();
        SqlDataReader dr;
        List<UserAddress> _userAddresses=new List<UserAddress>();  //fatch from database
        List<ServiceRequest> _serviceRequests=new List<ServiceRequest>();
        public BookServiceController(HelperlandContext helperlandContext,IHostingEnvironment env)
        {
            
            _helperlandContext = helperlandContext;
            con=new SqlConnection();
            configuration = new ConfigurationBuilder().SetBasePath(env.ContentRootPath).AddJsonFile("appsettings.json").Build();
            con.ConnectionString = configuration["ConnectionString"];    
        }
        public List<UserAddress> fatchUserAddress()
        {
            if (_userAddresses.Count > 0)
            {
                _userAddresses.Clear();
            }
            string currentUser = HttpContext.Session.GetString("CurrentUser");
            User user = JsonConvert.DeserializeObject<User>(currentUser);
            var userid = HttpContext.Session.GetString("User_Id");
            int userId = user.UserId;
            con.Open();
            com.Connection = con;
            com.CommandText = "select AddressLine1,AddressLine2,Mobile,City,PostalCode from UserAddress where UserId='" + userId+"'";
            dr = com.ExecuteReader();
            while (dr.Read())
            {
                _userAddresses.Add(new UserAddress()
                {
                    AddressLine1 = dr["AddressLine1"].ToString(),
                    AddressLine2 = dr["AddressLine2"].ToString(),
                    PostalCode = dr["PostalCode"].ToString(),
                    City=dr["City"].ToString(),
                    Mobile = dr["Mobile"].ToString()
                });
            }
            con.Close();
            return _userAddresses;
        }

        public List<UserAddress> fatchUserAddressNewAddress()
        {
            if (_userAddresses.Count > 0)
            {
                _userAddresses.Clear();
            }
            var userid = HttpContext.Session.GetString("User_Id");
            int userId = Int16.Parse(userid);
            var userZip = HttpContext.Session.GetString("_zipCode");
            
            con.Open();
            com.Connection = con;
           // com.CommandText = "SELECT ZipcodeValue,CityName,Mobile FROM Zipcode INNER JOIN City ON Zipcode.CityId = City.Id LEFT JOIN UserAddress ON Zipcode.ZipcodeValue = UserAddress.PostalCode where UserId = '"+ userId+"'";
            com.CommandText = "SELECT ZipcodeValue,CityName,Mobile FROM (Zipcode INNER JOIN City ON Zipcode.CityId = City.Id),[User]  where  UserId='" + userId + "'";

            dr = com.ExecuteReader();
            while (dr.Read())
            {
                _userAddresses.Add(new UserAddress()
                {
                    Mobile= dr["Mobile"].ToString(),
                    City = dr["CityName"].ToString(),
                    //PostalCode= dr["ZipcodeValue"].ToString()
                    PostalCode=userZip
                });
            }
            con.Close();
            return _userAddresses;
        }



        [HttpPost]
        
        public IActionResult ZipCodeManager(BookServiceViewModel model)
        {
            if (ModelState.IsValid)
            {
                Zipcode zipcodeValue = _helperlandContext.Zipcode.Where(x => x.ZipcodeValue == model.ZipcodeValue).FirstOrDefault();
                if (zipcodeValue == null)
                {
                    ViewBag.Message = false;
                    ViewBag.PostalError = "We are not providing service in this area." +
                        "We’ll notify you if any helper would start working near your area.";
                    return View("~/Views/BookService/BookServicePage.cshtml");
                }
              
                HttpContext.Session.SetString("_zipCode", zipcodeValue.ZipcodeValue);
                ViewBag.Message = true;
                return View("~/Views/BookService/BookServicePage.cshtml");
            }
            else
            {
                return View("~/Views/BookService/BookServicePage.cshtml");
            }
            
        }

       public async Task<IActionResult> CheckCardDetail(BookServiceViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.CardNumber == "1111 1111 1111 1111")
                {
                    return Ok("Ok Card Number is Valid");
                }
                else
                {
                    return BadRequest("Enter Valid Card Number");
                }

            }

            return BadRequest("Enter Valid Card Number");
            
        }


        [HttpPost]
        public async Task<IActionResult> UserAddressGet(AddNewAddress model)
        {
            if (ModelState.IsValid)
            {
              
                var userid = HttpContext.Session.GetString("User_Id");
                var userEmail = HttpContext.Session.GetString("User_Email");
                int userId = Int16.Parse(userid);
                //User user = _helperlandContext.User.Where(user.UserId==uu).FirstOrDefault();
               // User user = new User();
                User user = _helperlandContext.User.Where(x => x.UserId == userId).FirstOrDefault();  
                IEnumerable<UserAddress> userAddressFatch=_helperlandContext.UserAddress.Where(x=>x.UserId == userId).ToList();

                UserAddress userAddress = new UserAddress();


                userAddress.AddressLine1 = model.AddressLine1;
                userAddress.AddressLine2 = model.AddressLine2;
                userAddress.UserId = userId;
                userAddress.PostalCode = model.PostalCodeAddresss;
                userAddress.City = model.CityAddress;
                userAddress.Mobile = model.MobileAddress;
                if(userAddressFatch.Any(x=>x.IsDefault==true))
                {
                    userAddress.IsDefault = false;
                   
                }
                else
                {
                    userAddress.IsDefault = true;
                    user.ZipCode = model.PostalCodeAddresss;
                    _helperlandContext.User.Update(user);
                }
                
                userAddress.IsDeleted = false;
                userAddress.Email = userEmail;
                
                _helperlandContext.UserAddress.Add(userAddress);
                _helperlandContext.SaveChanges();


                return Ok( "Form Data received!");
            }

            return BadRequest("Enter required fields");
            
        }
        //public IActionResult UserAddressDisplay()
        //{
        //    UserAddress userAddress = _helperlandContext.UserAddress.Find(HttpContext.Session.GetString("User_Id"));
        //    BookServiceViewModel userAddressDisplayViewModel = new BookServiceViewModel()
        //    {
                    

        //    };
        //    return View();
        //}
       
        [Authorize]

        public IActionResult BookServicePage()
        {
           if (HttpContext.Session.GetString("User_Id") != null)
            {
                ViewBag.UserId = HttpContext.Session.GetString("User_Id");
                return View();
            }

            //if(user == null)
            //{
            //    ViewBag.BookserviceId = "not correct detail";
            //    return View("Home", "Index");
            //}
            return View();
        }

        public async Task<IActionResult> ServiceRequestSuccessful(BookServiceViewModel model)
        {

            if (ModelState.IsValid)
            {
                var userid = HttpContext.Session.GetString("User_Id");
                int userId = Int16.Parse(userid);
                bool paymetDone = true;
                bool paymetDue = false;
                var distance = 10;
                int? spID = model.selectedFSPId;
                var zipCode=HttpContext.Session.GetString("_zipCode");
                ServiceRequest serviceRequest = new ServiceRequest();  
                

                serviceRequest.Comments = model.Comments;
                serviceRequest.UserId = userId;
                serviceRequest.CreatedDate = DateTime.Now;
                serviceRequest.Discount = model.Discount;
                
                serviceRequest.ExtraHours = model.ExtraHours;
                serviceRequest.HasPets = model.HasPets;
                serviceRequest.PaymentDone = paymetDone;
                serviceRequest.PaymentDue = paymetDue;
                serviceRequest.ServiceHourlyRate = model.ServiceHourlyRate;
                serviceRequest.ServiceHours = model.ServiceHours;
                serviceRequest.ServiceStartDate = model.ServiceStartDate;
                serviceRequest.ServiceId = userId;
                serviceRequest.ZipCode = zipCode;
                serviceRequest.SubTotal = model.SubTotal;
                serviceRequest.TotalCost= model.TotalCost;
                serviceRequest.ModifiedDate = DateTime.Now;
                serviceRequest.Distance = distance;
                serviceRequest.RecordVersion = Guid.NewGuid();
                serviceRequest.ServiceRequestExtra = model.ServiceRequestExtra;
                if(spID==null || spID == 0)
                {
                    serviceRequest.Status = ValuesData.SERVICE_PENDING;
                    _helperlandContext.ServiceRequest.Add(serviceRequest);
                    _helperlandContext.SaveChanges();
                    int customerId = Int16.Parse(User.Claims.FirstOrDefault(x => x.Type == "userId").Value);
                    List<int> blockedSPIds = _helperlandContext.FavoriteAndBlocked.Where(x => x.UserId == customerId && x.IsBlocked == true).Select(x => x.TargetUserId).Distinct().ToList();
                    var emailList = _helperlandContext.User.Where(user => user.UserTypeId == 2 && user.ZipCode == zipCode && !blockedSPIds.Any(a => a == user.UserId)).Select(user => user.Email).ToList();
                    string subject = "New Service Request arrived!!Hurry Up..";
                    string body = "new service request arrived which is in your area and id of the service is " + serviceRequest.ServiceRequestId;
                    EmailManager.SendEmail(emailList, subject, body);
                }
                else
                {
                    serviceRequest.Status = ValuesData.SERVICE_ACCEPTED;
                    serviceRequest.ServiceProviderId = spID;
                    _helperlandContext.ServiceRequest.Add(serviceRequest);
                    var isError = _helperlandContext.SaveChanges();
                    var emailList = _helperlandContext.User.Where(user => user.UserId == spID).Select(user => user.Email).ToList();
                    string subject = "New Service Request arrived!!Hurry Up..";
                    string body = "A service request " + serviceRequest.ServiceRequestId + " has been directly assigned to you.";
                    EmailManager.SendEmail(emailList, subject, body);
                }
                
               
                return Ok("ServiceRquest Form Data received!");


            }
            return BadRequest("Enter required fields");

           
            }
       
       
        public async Task<IActionResult> serviceAddressSend(BookServiceViewModel model)
        {
            if (ModelState.IsValid)
            {
                var serviceRequest_ID = _helperlandContext.ServiceRequest.FromSqlRaw(" SELECT * FROM ServiceRequest WHERE ServiceRequestId=(select Max(ServiceRequestId) from ServiceRequest )").FirstOrDefault();
                var serviceIdReqInt = serviceRequest_ID.ServiceRequestId;
                var userEmail = HttpContext.Session.GetString("User_Email");
                var zipCode = HttpContext.Session.GetString("_zipCode");
                
                ServiceRequestAddress serviceRequestAddress = new ServiceRequestAddress
                {
                    ServiceRequestId = serviceRequest_ID.ServiceRequestId,
                    AddressLine1 = model.AddressLine1,
                    AddressLine2 = model.AddressLine2,
                    City = model.CityName,
                    PostalCode = model.ZipcodeValue,
                    Mobile = model.MobileNum,
                    Email = userEmail

                };
             //HttpContext.Session.SetString("ServiceRequestID", serviceIdReqInt.ToString());
            _helperlandContext.ServiceRequestAddress.Add(serviceRequestAddress);
            _helperlandContext.SaveChanges();

            return Ok("Send Final ServiceRequest finaly");


        }
            return BadRequest("Enter ServiceRequest required fields");

        }

        public  List<ServiceRequest> AlertServiceReqID()
        {
            if (
                _serviceRequests.Count > 0)
            {
                _serviceRequests.Clear();
            }
            var serviceRequest_ID = _helperlandContext.ServiceRequest.FromSqlRaw(" SELECT * FROM ServiceRequest WHERE ServiceRequestId=(select Max(ServiceRequestId) from ServiceRequest )").FirstOrDefault();

            var userid = HttpContext.Session.GetString("User_Id");
            int userId = Int16.Parse(userid);
            var userZip = HttpContext.Session.GetString("_zipCode");

            con.Open();
            com.Connection = con;
            com.CommandText = "SELECT ServiceRequestId FROM ServiceRequest WHERE ServiceRequestId=(select Max(ServiceRequestId) from ServiceRequest )";
            dr = com.ExecuteReader();
            while (dr.Read())
            {
                _serviceRequests.Add(new ServiceRequest()
                {
                    //ServiceRequestId = serviceRequest_ID.ServiceRequestId
                    ServiceRequestId =(int) dr["ServiceRequestId"]  
                    //Mobile = dr["Mobile"].ToString(),
                    //City = dr["CityName"].ToString(),
                    //PostalCode= dr["ZipcodeValue"].ToString()
                    //PostalCode = userZip
                }) ;
            }
            con.Close();
            return _serviceRequests;
        }

        public async Task<IActionResult> ExtraServiceSend(BookServiceViewModel model)
        {
            if (ModelState.IsValid)
            {
                var serviceRequest_ID = _helperlandContext.ServiceRequest.FromSqlRaw(" SELECT * FROM ServiceRequest WHERE ServiceRequestId=(select Max(ServiceRequestId) from ServiceRequest )").FirstOrDefault();
                var serviceIdReqInt = serviceRequest_ID.ServiceRequestId;
               
                ServiceRequestExtra serviceRequestExtra = new ServiceRequestExtra();
                if (model.ExtraHours > 0)
                {
                    serviceRequestExtra.ServiceRequestId = serviceRequest_ID.ServiceRequestId;
                    serviceRequestExtra.ServiceExtraId = serviceRequest_ID.ServiceRequestId;

                }
               
                //HttpContext.Session.SetString("ServiceRequestID", serviceIdReqInt.ToString());
                _helperlandContext.ServiceRequestExtra.Add(serviceRequestExtra);
                _helperlandContext.SaveChanges();

                return Ok("Send Final ServiceRequest finaly");


            }
            return BadRequest("Enter ServiceRequest required fields");

        }

        public PartialViewResult FavSPBook()
        {
             int customerId = Int16.Parse(User.Claims.FirstOrDefault(x => x.Type == "userId").Value);
            IEnumerable<User> favoriteAndBlocked = _helperlandContext.FavoriteAndBlocked.Where(x => x.UserId == customerId && x.IsFavorite == true).Select(x=>x.TargetUser).ToList();
            //IEnumerable<FavoriteAndBlocked>  favoriteAndBlocked =_helperlandContext.FavoriteAndBlocked.Where(x=>x.UserId == customerId && x.IsFavorite==true).Select(x => x.TargetUser).ToList();
            return PartialView(favoriteAndBlocked);
        }



    }
}