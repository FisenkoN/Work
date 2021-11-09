using System.ComponentModel.DataAnnotations;

namespace School.WEB.Models
{
    public class User : EntityBase
    {
        [EmailAddress]
        public string Email { get; set; }

        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}