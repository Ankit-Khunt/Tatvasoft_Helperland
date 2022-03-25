using Helperland.Models;
using Helperland.ViewModels;
using Helperland.ViewModels.Admin;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Helperland.Controllers
{
    [Authorize(Roles = "1")]
    public class AdminController : Controller
    {
        public HelperlandContext _helperlandContext;
        public AdminController(HelperlandContext helperlandContext)
        {
            _helperlandContext = helperlandContext;
        }
        [HttpGet]
        public IActionResult AdminServiceRequest()
        {
            //IEnumerable<ServiceRequest> serviceRequests = _helperlandContext.ServiceRequest.Include(x => x.ServiceRequestAddress).Include(x=>x.User).Include(x=>x.ServiceProvider).ThenInclude(x=>x.RatingRatingToNavigation).ToList();
            //AdminSPRequestViewModel model=new AdminSPRequestViewModel();
            //model.ServiceRequestId=serviceRequests.Select(x=>x.ServiceRequestId).FirstOrDefault();
            var result = _helperlandContext.ServiceRequest.Include(x => x.ServiceRequestAddress).Include(x => x.User).Include(x => x.ServiceProvider).ThenInclude(x => x.RatingRatingToNavigation).AsQueryable();


            return View(result);
        }

        public PartialViewResult SRTableAdmin()
        {
            //IEnumerable<ServiceRequest> serviceRequests = _helperlandContext.ServiceRequest.Include(x => x.ServiceRequestAddress).Include(x => x.User).Include(x => x.ServiceProvider).ThenInclude(x => x.RatingRatingToNavigation).ToList();
            var result = _helperlandContext.ServiceRequest.Include(x => x.ServiceRequestAddress).Include(x => x.User).Include(x => x.ServiceProvider).ThenInclude(x => x.RatingRatingToNavigation).AsQueryable();

            return PartialView(result);
        }
        [HttpPost]
        public IActionResult AdminServiceRequest(AdminSPRequestViewModel model)
        {
            IEnumerable<ServiceRequest> serviceRequests = _helperlandContext.ServiceRequest.Include(x => x.ServiceRequestAddress).Include(x => x.User).Include(x => x.ServiceProvider).ThenInclude(x => x.RatingRatingToNavigation).ToList();
            var result = _helperlandContext.ServiceRequest.Include(x=>x.ServiceRequestAddress).Include(x => x.User).Include(x => x.ServiceProvider).ThenInclude(x => x.RatingRatingToNavigation).AsQueryable();
            if (model != null)
            {
                if (model.ServiceRequestId.HasValue)
                    result = result.Where(x => x.ServiceRequestId == model.ServiceRequestId);
                if (!string.IsNullOrEmpty(model.FirstName))
                    result = result.Where(x => x.User.FirstName==model.FirstName);
               
            }
            return View(result);
        }

        public PartialViewResult filterApply(int? serviceReqId, string? PostalCodeForm,string? EmailForm,string? selectCustomerForm,string? selectSPForm , int? statusForm,int? HasPetForm,DateTime? fromDateForm,DateTime? toDateFormId)
        {
          
            var result = _helperlandContext.ServiceRequest.Include(x => x.ServiceRequestAddress).Include(x => x.User).Include(x => x.ServiceProvider).ThenInclude(x => x.RatingRatingToNavigation).AsQueryable();
            bool checkPets=false;

            {
                if (serviceReqId.HasValue)
                    result = result.Where(x => x.ServiceRequestId == serviceReqId);
                if (!string.IsNullOrEmpty(PostalCodeForm))
                    result = result.Where( x => x.ServiceRequestAddress.Select(x=>x.PostalCode== PostalCodeForm).FirstOrDefault());
                if (!string.IsNullOrEmpty(EmailForm))
                    result = result.Where(x => x.User.Email == EmailForm);
                if (!string.IsNullOrEmpty(selectCustomerForm))
                    result = result.Where(x => x.User.FirstName == selectCustomerForm);
                if (!string.IsNullOrEmpty(selectSPForm))
                    result = result.Where(x => x.ServiceProvider.FirstName == selectSPForm);
                if (statusForm.HasValue) 
                    result=result.Where(x=>x.Status==statusForm);
                if (HasPetForm.HasValue)
                {
                    if (HasPetForm == 1)
                    {
                        checkPets = true;
                    }
                    else
                    {
                        checkPets = false;
                    }
                    result = result.Where(x => x.HasPets == checkPets);

                }
                    
                if(fromDateForm.HasValue && toDateFormId.HasValue)
                {
                    result = result.Where(x => (x.ServiceStartDate.Date >= fromDateForm && x.ServiceStartDate.Date <= toDateFormId));
                }
                else
                {
                    if (fromDateForm.HasValue)
                        result = result.Where(x => x.ServiceStartDate.Date >= fromDateForm);
                    if (toDateFormId.HasValue)
                        result = result.Where(x => x.ServiceStartDate.Date <= toDateFormId);
                }
               


            }
            return PartialView("SRTableAdmin",result);
        }


        [HttpGet]
        public PartialViewResult EditServiceRequest(int id)
        {
            ServiceRequest? serviceRequest = _helperlandContext.ServiceRequest.Include(x => x.ServiceRequestAddress).FirstOrDefault(x => x.ServiceRequestId == id);
            EditServiceRequestViewModel editRequestViewModel = new EditServiceRequestViewModel();
            editRequestViewModel.ServiceRequestId = serviceRequest.ServiceRequestId;
            editRequestViewModel.ServiceStartDate = serviceRequest.ServiceStartDate.Date;
            editRequestViewModel.ServiceStartTime = serviceRequest.ServiceStartDate.ToString("HH:mm");
            editRequestViewModel.StreetName = serviceRequest.ServiceRequestAddress.ElementAt(0).AddressLine1;
            editRequestViewModel.HouseNumber = serviceRequest.ServiceRequestAddress.ElementAt(0).AddressLine2;
            editRequestViewModel.PostalCode = serviceRequest.ServiceRequestAddress.ElementAt(0).PostalCode;
            editRequestViewModel.City = serviceRequest.ServiceRequestAddress.ElementAt(0).City;
            return PartialView(editRequestViewModel);
        }

        [HttpPost]
        public PartialViewResult EditServiceRequest(EditServiceRequestViewModel model)
        {
            if (ModelState.IsValid)
            {
                string newDateString = model.ServiceStartDate.ToString("dd/MM/yyyy") + " " + model.ServiceStartTime;
                DateTime newDate = DateTime.Parse(newDateString);
                ServiceRequest request = _helperlandContext.ServiceRequest.Include(x => x.ServiceRequestAddress).Include(x => x.User).Include(x => x.ServiceProvider).FirstOrDefault(x => x.ServiceRequestId == model.ServiceRequestId);
                request.ServiceStartDate = newDate;
                request.ServiceRequestAddress.ElementAt(0).AddressLine1 = model.StreetName;
                request.ServiceRequestAddress.ElementAt(0).AddressLine2 = model.HouseNumber;
                request.ServiceRequestAddress.ElementAt(0).PostalCode = model.PostalCode;
                request.ServiceRequestAddress.ElementAt(0).City = model.City;
                if (model.RescheduleReason != null)
                {
                    request.Comments = model.RescheduleReason;
                }
                request.ModifiedDate = DateTime.Now;
                request.ModifiedBy = Int16.Parse(User.Claims.FirstOrDefault(x => x.Type == "userId").Value);
                _helperlandContext.ServiceRequest.Update(request);
                _helperlandContext.SaveChanges();
                //send email to user and service provider if exist
                List<string> emailList = new List<string>();
                emailList.Add(request.User.Email);
                if (request.ServiceProvider != null)
                {
                    emailList.Add(request.ServiceProvider.Email);
                }
                string Subject = "Admin has to inform you something !!";
                string Body = "Admin has reschedule the request " + request.ServiceRequestId + ", new date is " + newDate + ".";
                EmailManager.SendEmail(emailList, Subject, Body);
                var message = "You have successfully rescheduled the request.";
                ViewBag.Alert = "<div class='alert alert-success alert-dismissible fade show' role='alert'>" + message + "<button type= 'button' class='btn-close' data-bs-dismiss='alert' aria-label='Close'></button></div>";
                return PartialView(model);
            }
            else
            {
                return PartialView(model);
            }
        }

        [HttpGet]
        public PartialViewResult ServiceDetails(int id)
        {
            ServiceRequest serviceRequest = _helperlandContext.ServiceRequest.Include(x => x.ServiceRequestAddress).Include(x => x.ServiceRequestExtra).FirstOrDefault(x => x.ServiceRequestId == id);
            return PartialView(serviceRequest);
        }

    }
}
