using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using School.WEB.Extensions;

namespace School.WEB.Models
{
    public class Teacher : EntityBase
    {
        [Required]
        [StringLength(20, ErrorMessage = "FirstName cannot be longer than 20 and shorter than 2 characters", MinimumLength = 2)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(20, ErrorMessage = "LastName cannot be longer than 20 and shorter than 2 characters", MinimumLength = 2)]
        public string LastName { get; set; }

        [Range(18, 80, ErrorMessage = "Age must be more then 18 and less then 80")]
        public int Age { get; set; }

        [Required]
        public Gender Gender { get; set; }

        public int? ClassId { get; set; }

        [ForeignKey("ClassId")]
        public Class Class { get; set; }
        
        public ICollection<Class> Classes { get; set; }

        public string FullName => FirstName + " " + LastName;
        
        [Required] 
        public bool Active { get; set; } = true;

        public int? SubjectId { get; set; }
        
        [ForeignKey("SubjectId")]
        public Subject Subject { get; set; }

        [RegularExpression(RegexPattern.Url, ErrorMessage = "Incorrect image url")]
        public string Image { get; set; }
        
        public Teacher()
        {
            Classes = new List<Class>();
        }
    }
}