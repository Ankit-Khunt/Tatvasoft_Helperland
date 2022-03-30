using System.ComponentModel.DataAnnotations;

namespace Helperland.ViewModels.CustomerService
{
    public class EditAddressViewModel
    {
        public int? AddressId { get; set; }

        [Required]
        [Display(Name = "Street name")]
        public string StreetName { get; set; }

        [Required]
        [Display(Name = "House number")]
        public string HouseNumber { get; set; }

        [Required]
        [Display(Name = "Postal code")]
        public string PostalCode { get; set; }

        [Required]
        [Display(Name = "City")]
        public string City { get; set; }

        [Required]
        [Display(Name = "Phone number")]
        [MaxLength(10, ErrorMessage = "Max 10 digit for Mobile Number")]
        [MinLength(10, ErrorMessage = "Need 10 digit for Mobile Number")]
        public string PhoneNumber { get; set; }
    }
}
