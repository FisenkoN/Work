using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace School.WEB.Models
{
    public class Class : EntityBase
    {
        public Class()
        {
            Students = new List<Student>();
        }

        [Required(ErrorMessage = "It's required value!")]
        [StringLength(10, ErrorMessage = "Name cannot be longer than 10 and shorter than 2 characters.", MinimumLength = 2)]
        public string Name { get; set; }

        public int? TeacherId { get; set; }

        [ForeignKey("TeacherId")] 
        public Teacher Teacher { get; set; }

        public ICollection<Student> Students { get; set; }
    }
}