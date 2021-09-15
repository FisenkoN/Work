using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace School.DAL.Entities
{
    public class Subject : EntityBase
    {
        [Required]
        [MaxLength(30)] 
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