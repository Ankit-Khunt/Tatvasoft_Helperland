using System;

namespace Helperland.ViewModels
{
    public class ServiceDetailViewModel
    {
        public int ServiceRequestId { get; set; }
        public decimal TotalCost { get; set; }
        public DateTime ServiceStartDate { get; set; }
        public int? ServiceProviderId { get; set; }
        public int UserTypeId { get; set; }
        public float ServiceHours { get; set; }
        public double ExtraHours { get; set; }
        public bool HasPets { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string PostalCode { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
    }
}
