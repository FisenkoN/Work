using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using School.WEB.Models;

namespace School.WEB.ViewModels.ManageStudent.CreateStudent
{
    public class CreateStudentViewModel
    {
        [Required]
        [StringLength(20, ErrorMessage = "FirstName cannot be longer than 20 and shorter than 2 characters", MinimumLength = 2)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(20, ErrorMessage = "LastName cannot be longer than 20 and shorter than 2 characters", MinimumLength = 2)]
        public string LastName { get; set; }

        [Required]
        [Range(5, 18, ErrorMessage = "Age must be more then 5 and less then 18")]
        public int Age { get; set; }

        [Required]
        public Gender Gender { get; set; }

        [RegularExpression(@"^(?:http(s)?:\/\/)?[\w.-]+(?:\.[\w\.-]+)+[\w\-\._~:/?#[\]@!\$&'\(\)\*\+,;=.]+$", ErrorMessage = "Incorrect image url")]
        public string Image { get; set; }
        
        public int? ClassId { get; set; }

        public IEnumerable<int> SubjectIds { get; set; }
    }
}