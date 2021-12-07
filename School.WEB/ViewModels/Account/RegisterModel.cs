using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace School.WEB.ViewModels.Account
{
    public class RegisterModel : OperationResultViewModel
    {
        [EmailAddress]
        [Required(ErrorMessage ="No email specified")]
        [DisplayName("Email")]
        public string Email { get; set; }
         
        [Required(ErrorMessage = "No password specified")]
        [DataType(DataType.Password)]
        [DisplayName("Password")]
        public string Password { get; set; }
         
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Invalid password")]
        [DisplayName("ConfirmPassword")]
        public string ConfirmPassword { get; set; }
        
        public int? Role { get; set; }
    }
}