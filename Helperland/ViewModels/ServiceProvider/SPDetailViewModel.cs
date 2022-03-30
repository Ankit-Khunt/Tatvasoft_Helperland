using System.ComponentModel.DataAnnotations;

namespace Helperland.ViewModels.ServiceProvider
{
    public class SPDetailViewModel
    {
        public bool IsActive { get; set; }
        public string? UserProfilePicture { get; set; }

        [Required]
        [Display(Name = "First name")]
        public string FirstName { get; set; }
        [Required]
        [Display(Name = "Last name")]
        public string LastName { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Email ID is requierd")]
        [DataType(DataType.EmailAddress)]
        [RegularExpression(@"^([0-9a-zA-Z]([\+\-_\.][0-9a-zA-Z]+)*)+@(([0-9a-zA-Z][-\w]*[0-9a-zA-Z]*\.)+[a-zA-Z0-9]{2,3})$",
         ErrorMessage = "Please provide valid email id")]

        public string Email { get; set; }
        [Required]
        [Display(Name = "Mobile number")]
        [MaxLength(10, ErrorMessage = "Max 10 digit for Mobile Number")]
        [MinLength(10, ErrorMessage = "Need 10 digit for Mobile Number")]
        public string Mobile { get; set; }

        public string BirthDay { get; set; }
        public string BirthMonth { get; set; }
        public string BirthYear { get; set; }

        [Display(Name = "Nationality")]
        public int? Nationality { get; set; }

        [Display(Name = "Gender")]
        public int? Gender { get; set; }

        public int? AddressId { get; set; }

        [Required]
        [Display(Name = "Street name")]
        public string StreetName { get; set; }

        [Required]
        [Display(Name = "House number")]
        public string HouseNumber { get; set; }

        [Required]
        [Display(Name = "Postal code")]
        [MaxLength(6, ErrorMessage = "Postal has 6 digit")]
        [MinLength(6, ErrorMessage = "Postal has 6 digit")]
        public string PostalCode { get; set; }

        [Required]
        [Display(Name = "City")]
        public string City { get; set; }
    }
}
