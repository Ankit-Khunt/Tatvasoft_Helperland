using System.ComponentModel.DataAnnotations;

namespace Helperland.ViewModels
{
    public class ForgotPasswordViewModel
    {   
       
        [Required(AllowEmptyStrings = false, ErrorMessage = "Email ID is requierd")]
        [DataType(DataType.EmailAddress)]
        [RegularExpression(@"^([0-9a-zA-Z]([\+\-_\.][0-9a-zA-Z]+)*)+@(([0-9a-zA-Z][-\w]*[0-9a-zA-Z]*\.)+[a-zA-Z0-9]{2,3})$",
        ErrorMessage = "Please provide valid email id")]
        public string Email { get; set; }
    }
}
