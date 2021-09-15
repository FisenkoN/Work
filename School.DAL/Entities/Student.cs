using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace School.DAL.Entities
{
    public class Student : EntityBase
    {
        public Student()
        {
            Subjects = new List<Subject>();
        }

        [Required]
        [StringLength(20, ErrorMessage = "FirstName cannot be longer than 20 characters.")]
        public string FirstName { get; set; }

        [Required]
        [StringLength(20, ErrorMessage = "LastName cannot be longer than 20 characters.")]
        public string LastName { get; set; }

        [Range(5, 18, ErrorMessage = "Age must be more then 5 and less then 18")]
        public int Age { get; set; }

        [Required] public Gender Gender { get; set; }

        public int? ClassId { get; set; }

        [ForeignKey("ClassId")] public Class Class { get; set; }

        public ICollection<Subject> Subjects { get; set; }
    }
}