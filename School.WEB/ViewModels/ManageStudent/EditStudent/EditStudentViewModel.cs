using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using School.WEB.Models;

namespace School.WEB.ViewModels.ManageStudent.EditStudent
{
    public class EditStudentViewModel
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [StringLength(20, ErrorMessage = "FirstName cannot be longer than 20 and shorter than 2 characters", MinimumLength = 2)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(20, ErrorMessage = "LastName cannot be longer than 20 and shorter than 2 characters", MinimumLength = 2)]
        public string LastName { get; set; }

        [RegularExpression(@"^(?:http(s)?:\/\/)?[\w.-]+(?:\.[\w\.-]+)+[\w\-\._~:/?#[\]@!\$&'\(\)\*\+,;=.]+$", ErrorMessage = "Incorrect image url")]
        public string Image { get; set; }

        [Range(5, 18, ErrorMessage = "Age must be more then 18 and less then 80")]
        public int Age { get; set; }

        [Required]
        public Gender Gender { get; set; }

        public int? ClassId { get; set; }

        public IEnumerable<int> SubjectIds { get; set; }

        public EditStudentViewModel()
        {
            
        }

        public EditStudentViewModel(Models.Student student)
        {
            Id = student.Id;
            FirstName = student.FirstName;
            LastName = student.LastName;
            Age = student.Age;
            Image = student.Image;
            Gender = student.Gender;
            ClassId = student.ClassId;
            SubjectIds = student.Subjects.Select(s => s.Id);
        }
    }
}