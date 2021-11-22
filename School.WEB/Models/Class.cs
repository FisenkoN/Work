using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace School.WEB.Models
{
    public class Class : EntityBase
    {
        [Required]
        [StringLength(5)]
        [MinLength(2, ErrorMessage = "Name cannot be shorter than 2 characters")]
        public string Name { get; set; }

        public int? TeacherId { get; set; }

        [ForeignKey("TeacherId")] 
        public Teacher Teacher { get; set; }

        public ICollection<Student> Students { get; set; }

        public Class()
        {
            Students = new List<Student>();
        }
    }
}