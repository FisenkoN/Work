using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace School.WEB.ViewModels.Account
{
    public class LoginModel : OperationResultViewModel
    {
        [Required(ErrorMessage = "No email specified")]
        [EmailAddress]
        [DisplayName("Email")]
        public string Email { get; set; }
         
        [Required(ErrorMessage = "No password specified")]
        [DataType(DataType.Password)]
        [DisplayName("Password")]
        public string Password { get; set; }
    }
}