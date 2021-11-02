using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using School.WEB.Models;

namespace School.WEB.ViewModels.Admin.CreateTeacher
{
    public class CreateTeacherViewModel
    {
        [Required]
        [StringLength(20, ErrorMessage = "FirstName cannot be longer than 20 characters.")]
        public string FirstName { get; set; }

        [Required]
        [StringLength(20, ErrorMessage = "LastName cannot be longer than 20 characters.")]
        public string LastName { get; set; }

        [Required]
        [Range(18, 80, ErrorMessage = "Age must be more then 18 and less then 80")]
        public int Age { get; set; }

        [Required]
        public Gender Gender { get; set; }

        public string Image { get; set; }

        public int? ClassId { get; set; }

        public IEnumerable<int> SubjectIds { get; set; }
    }
}