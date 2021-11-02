using System.ComponentModel.DataAnnotations;

namespace School.WEB.ViewModels.Account
{
    public class RegisterModel
    {
        [Required(ErrorMessage ="No email specified")]
        public string Email { get; set; }
         
        [Required(ErrorMessage = "No password specified")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
         
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Invalid password")]
        public string ConfirmPassword { get; set; }
    }
}