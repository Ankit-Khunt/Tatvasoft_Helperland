using Helperland.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace Helperland.ViewModels.ServiceProvider
{
    public class NewServiceRequestViewModel
    {
        public User user { get; set; }
        public ServiceRequest serviceRequestViewModel { get; set; }
        public ServiceRequestAddress addressViewModel { get; set; }

        public string ServiceArea { get; set; }

        public List<SelectListItem> ServiceAreaItems { get; } = new List<SelectListItem>
    {
        new SelectListItem { Value = "1", Text = "25" },
        new SelectListItem { Value = "2", Text = "30" },
        new SelectListItem { Value = "3", Text = "40"  },
    };


    }
}
