using Helperland.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Helperland.ViewModels
{
    public class BookServiceViewModel
    {
        //zipcode
        public int PostalId { get; set; }
        public string ZipcodeValue { get; set; }
        public int CityId { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        
        public string Email { get; set; }

        //public City City { get; set; }

        //state
        public int State_Id { get; set; }
        public string CityName { get; set; }
        public int StateId { get; set; }

        public string MobileNum { get; set; }

        //public State State { get; set; }
        public ICollection<Zipcode> Zipcode { get; set; }

        //address
        

        // Test for card

        public string CardNumber { get; set; }
        

      
        public int UserId { get; set; }
        public int ServiceId { get; set; }
        public DateTime ServiceStartDate { get; set; }
        public string ZipCode { get; set; }
        public decimal? ServiceHourlyRate { get; set; }
        public double ServiceHours { get; set; }
        public double? ExtraHours { get; set; }
        public decimal SubTotal { get; set; }
        public decimal? Discount { get; set; }
        public decimal TotalCost { get; set; }
        public string Comments { get; set; }
        public string PaymentTransactionRefNo { get; set; }
        public bool PaymentDue { get; set; }
        public int? ServiceProviderId { get; set; }
        public DateTime? SpacceptedDate { get; set; }
        public bool HasPets { get; set; }
        public int? Status { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public decimal? RefundedAmount { get; set; }
        public decimal Distance { get; set; }
        public bool? HasIssue { get; set; }
        public bool? PaymentDone { get; set; }
        public Guid? RecordVersion { get; set; }

        public User ServiceProvider { get; set; }
        public User User { get; set; }
        public ICollection<Rating> Rating { get; set; }
        public ICollection<ServiceRequestAddress> ServiceRequestAddress { get; set; }
        public ICollection<ServiceRequestExtra> ServiceRequestExtra { get; set; }


    }
}
