using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using School.WEB.Extensions;

namespace School.WEB.ViewModels.Account
{
    public class LoginModel : OperationResultViewModel
    {
        [Required(ErrorMessage = "No email specified")]
        [EmailAddress]
        [DisplayName("Email")]
        public string Email { get; set; }
         
        [Required(ErrorMessage = "No password specified")]
        [RegularExpression(RegexPattern.Password, ErrorMessage = "Incorrect password. You mast use minimum eight and maximum 10 characters, at least one uppercase letter, one lowercase letter, one number and one special character")]
        [DataType(DataType.Password)]
        [DisplayName("Password")]
        public string Password { get; set; }
    }
}