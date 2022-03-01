using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Helperland.ViewModels
{
    public class Contact_Us_ViewModel
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "First Name is requierd")]
        public string FirstName { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "LastName Name is requierd")]
        public string LastName { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Email ID is requierd")]
        [DataType(DataType.EmailAddress)]
        [RegularExpression(@"^([0-9a-zA-Z]([\+\-_\.][0-9a-zA-Z]+)*)+@(([0-9a-zA-Z][-\w]*[0-9a-zA-Z]*\.)+[a-zA-Z0-9]{2,3})$",
        ErrorMessage = "Please provide valid email id")]
        public string Email { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Subject Name is requierd")]
        public string Subject { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Mobile Number is requierd")]
        [MinLength(10, ErrorMessage = "Need min 10 character")]
        public string PhoneNumber { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Message Name is requierd")]
        public string Message { get; set; }


        public IFormFile? File { get; set; }
    }
}
