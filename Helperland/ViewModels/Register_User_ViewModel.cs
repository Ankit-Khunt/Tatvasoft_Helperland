using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Helperland.ViewModels
{
    public class Register_User_ViewModel
    {
        public int UserId { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "First Name is requierd")]
        public string FirstName { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Last Name is requierd")]
        public string LastName { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Email ID is requierd")]
        [DataType(DataType.EmailAddress)]
        [RegularExpression(@"^([0-9a-zA-Z]([\+\-_\.][0-9a-zA-Z]+)*)+@(([0-9a-zA-Z][-\w]*[0-9a-zA-Z]*\.)+[a-zA-Z0-9]{2,3})$",
        ErrorMessage = "Please provide valid email id")]

        public string Email { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Password is requierd")]
        [MinLength(6, ErrorMessage = "Need min 6 character")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [DataType(DataType.Password)]
      
        [Compare("Password", ErrorMessage = "Pssword do not match")]
        public string ConfirmPassword { get; set; }
        [Required]
        public string Mobile { get; set; }
       
    }
}
