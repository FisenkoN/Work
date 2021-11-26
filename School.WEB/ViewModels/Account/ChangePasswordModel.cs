using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using School.WEB.Extensions;

namespace School.WEB.ViewModels.Account
{
    public class ChangePasswordModel : OperationResultViewModel
    {
        [Required(ErrorMessage = "No email specified")]
        [EmailAddress]
        [DisplayName("Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "No password specified")]
        [RegularExpression(RegexPattern.Password, ErrorMessage = "Incorrect password. You mast use minimum eight and maximum 10 characters, at least one uppercase letter, one lowercase letter, one number and one special character")]
        [DataType(DataType.Password)]
        [DisplayName("Old password")]
        public string OldPassword { get; set; }

        [Required(ErrorMessage = "No password specified")]
        [RegularExpression(RegexPattern.Password, ErrorMessage = "Incorrect password. You mast use minimum eight and maximum 10 characters, at least one uppercase letter, one lowercase letter, one number and one special character")]
        [DataType(DataType.Password)]
        [DisplayName("New password")]
        public string NewPassword { get; set; }
        
        [Required(ErrorMessage = "No password specified")]
        [RegularExpression(RegexPattern.Password, ErrorMessage = "Incorrect password. You mast use minimum eight and maximum 10 characters, at least one uppercase letter, one lowercase letter, one number and one special character")]
        [DataType(DataType.Password)]
        [Compare("NewPassword", ErrorMessage = "Invalid password")]
        [DisplayName("Confirm new password")]
        public string NewPasswordAgain { get; set; }
    }
}