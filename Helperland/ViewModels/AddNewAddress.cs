using Helperland.Models;
using System.ComponentModel.DataAnnotations;

namespace Helperland.ViewModels
{
    public class AddNewAddress
    {
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
