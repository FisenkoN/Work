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
        
        public int? RoleId { get; set; }
        
        public Role Role { get; set; }

        public int? AdminId { get; set; }

        public Admin Admin { get; set; }

        public int? TeacherId { get; set; }
        
        public Teacher Teacher { get; set; }

        public int? StudentId { get; set; }

        public Student Student { get; set; }
    }
}