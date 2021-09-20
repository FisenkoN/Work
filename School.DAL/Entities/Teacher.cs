using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace School.DAL.Entities
{
    public class Teacher : EntityBase
    {
        public Teacher()
        {
            Subjects = new List<Subject>();
        }

        [Required]
        [MaxLength(20)] 
        public string FirstName { get; set; }

        [Required] 
        [MaxLength(20)] 
        public string LastName { get; set; }

        [Range(18, 80)] 
        public int Age { get; set; }

        [Required]
        public Gender Gender { get; set; }

        public int? ClassId { get; set; }

        [ForeignKey("ClassId")]
        public Class Class { get; set; }

        public ICollection<Subject> Subjects { get; set; }

        public string Image { get; set; }
    }
}