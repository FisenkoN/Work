using System.ComponentModel.DataAnnotations;

namespace School.WEB.ViewModels.Account
{
    public class LoginModel
    {
        [Required(ErrorMessage = "No email specified")]
        public string Email { get; set; }
         
        [Required(ErrorMessage = "No password specified")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}