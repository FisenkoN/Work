using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using School.WEB.Extensions;

namespace School.WEB.Models
{
    [Index("Email", IsUnique = true)]
    public class User : EntityBase
    {
        [EmailAddress]
        
        public string Email { get; set; }

        [DataType(DataType.Password)]
        [RegularExpression(RegexPattern.Password, ErrorMessage = "Incorrect password. You mast use minimum eight and maximum 10 characters, at least one uppercase letter, one lowercase letter, one number and one special character")]
        public string Password { get; set; }
    }
}