using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace School.WEB.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        
        [EmailAddress]
        public string Email { get; set; }
        
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}