using System;
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
using Microsoft.Extensions.Configuration;
using static Helperland.ViewModels.BookServiceViewModel;

namespace Helperland.Controllers
{
    public class BookServiceController : Controller
    {
        public HelperlandContext _helperlandContext;

        readonly SqlConnection con;
      
        readonly IConfigurationRoot configuration;
        SqlCommand com = new SqlCommand();
        SqlDataReader dr;
        List<UserAddress> _userAddresses=new List<UserAddress>();  //fatch from database
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
            var userid = HttpContext.Session.GetString("User_Id");
            int userId = Int16.Parse(userid);
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
            com.CommandText = "SELECT ZipcodeValue,CityName,Mobile FROM Zipcode INNER JOIN City ON Zipcode.CityId = City.Id LEFT JOIN UserAddress ON Zipcode.ZipcodeValue = UserAddress.PostalCode where UserId = '"+ userId+"'";
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
            
            Zipcode zipcodeValue = _helperlandContext.Zipcode.Where(x => x.ZipcodeValue == model.ZipcodeValue).FirstOrDefault();
            if (zipcodeValue == null)
            {
                ViewBag.Message = false;
                ViewBag.PostalError = "This field is required";
                return View("~/Views/BookService/BookServicePage.cshtml");
            }
            HttpContext.Session.SetString("_zipCode", zipcodeValue.ZipcodeValue);
            ViewBag.Message = true;
            return View("~/Views/BookService/BookServicePage.cshtml");
        }

       

        [HttpPost]
        public async Task<IActionResult> UserAddressGet(BookServiceViewModel model)
        {
            if (ModelState.IsValid)
            {
              
                var userid = HttpContext.Session.GetString("User_Id");
                int userId = Int16.Parse(userid);
                //User user = _helperlandContext.User.Where(user.UserId==uu).FirstOrDefault();
                User user = new User();
               
              

                UserAddress userAddress = new UserAddress();


                userAddress.AddressLine1 = model.AddressLine1;
                userAddress.AddressLine2 = model.AddressLine2;
                userAddress.UserId = userId;
                userAddress.PostalCode = model.PostalCodeAddresss;
                userAddress.City = model.CityAddress;
                userAddress.Mobile = model.MobileAddress;
                userAddress.IsDefault = false;
                userAddress.IsDeleted = false;
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

        public IActionResult temp()
        {
            return View();
        }

    }
}