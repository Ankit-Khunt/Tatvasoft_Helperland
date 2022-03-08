using System;

namespace Helperland.ViewModels
{
    public class CustomeDashbordViewModel
    {
        public int ServiceRequestId { get; set; }
       
        public decimal SubTotal { get; set; }
        public DateTime ServiceStartDate { get; set; }
        public int? ServiceProviderId { get; set; }
        public int UserTypeId { get; set; }

        public float ServiceHours { get; set; } 


    }
}
