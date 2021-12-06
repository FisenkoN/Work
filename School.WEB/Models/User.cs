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
        public string Password { get; set; }
    }
}