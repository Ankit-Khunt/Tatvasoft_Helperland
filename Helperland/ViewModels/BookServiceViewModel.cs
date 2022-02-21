using Helperland.Models;
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

        public City City { get; set; }

        //state
        public int State_Id { get; set; }
        public string CityName { get; set; }
        public int StateId { get; set; }

        public State State { get; set; }
        public ICollection<Zipcode> Zipcode { get; set; }

        //address
        public int AddressId { get; set; }
        public int UserIdAddress { get; set; }
        [Required]
        public string AddressLine1 { get; set; }
        [Required]
        public string AddressLine2 { get; set; }
        [Required]
        public string CityAddress { get; set; }
        public string StateAddress { get; set; }
        [Required]
        public string PostalCodeAddresss { get; set; }
        [Required]
        public bool IsDefaultAddress { get; set; }
        [Required]
        public bool IsDeletedAddress { get; set; }
        [Required]
        public string MobileAddress { get; set; }
        public string EmailAddress { get; set; }

        public User UserAddress { get; set; }

    }
}
