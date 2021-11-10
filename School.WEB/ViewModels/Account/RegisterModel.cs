using System.ComponentModel.DataAnnotations;
using School.WEB.Extensions;

namespace School.WEB.ViewModels.Account
{
    public class RegisterModel
    {
        [EmailAddress]
        [Required(ErrorMessage ="No email specified")]
        public string Email { get; set; }
         
        [Required(ErrorMessage = "No password specified")]
        [RegularExpression(RegexPattern.Password, ErrorMessage = "Incorrect password. You mast use minimum eight and maximum 10 characters, at least one uppercase letter, one lowercase letter, one number and one special character")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
         
        [DataType(DataType.Password)]
        [RegularExpression(RegexPattern.Password, ErrorMessage = "Incorrect password. You mast use minimum eight and maximum 10 characters, at least one uppercase letter, one lowercase letter, one number and one special character")]
        [Compare("Password", ErrorMessage = "Invalid password")]
        public string ConfirmPassword { get; set; }
    }
}