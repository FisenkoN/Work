using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace School.WEB.Models
{
    public class Subject : EntityBase
    {
        [Required] 
        [StringLength(30, ErrorMessage = "Name cannot be longer than 30 and shorter than 3 characters", MinimumLength = 3)]
        public string Name { get; set; }

        public ICollection<Teacher> Teachers { get; set; }

        public ICollection<Student> Students { get; set; }
        
        public Subject()
        {
            Teachers = new List<Teacher>();

            Students = new List<Student>();
        }
    }
}