using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using School.WEB.Extensions;
using School.WEB.Models;

namespace School.WEB.ViewModels.ManageTeacher.EditCreateTeacher
{
    public class EditCreateTeacherViewModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(20, ErrorMessage = "FirstName cannot be longer than 20 and shorter than 2 characters", MinimumLength = 2)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(20, ErrorMessage = "LastName cannot be longer than 20 and shorter than 2 characters", MinimumLength = 2)]
        public string LastName { get; set; }

        [RegularExpression(RegexPattern.Url, ErrorMessage = "Incorrect image url")]
        public string Image { get; set; }

        [Range(18, 80, ErrorMessage = "Age must be more then 18 and less then 80")]
        public int Age { get; set; }

        [Required]
        public Gender Gender { get; set; }

        public int? ClassId { get; set; }

        public IEnumerable<int> SubjectIds { get; set; }

        public EditCreateTeacherViewModel()
        {
            
        }

        public EditCreateTeacherViewModel(Teacher teacher)
        {
            Id = teacher.Id;
            FirstName = teacher.FirstName;
            LastName = teacher.LastName;
            Image = teacher.Image;
            Age = teacher.Age;
            Gender = teacher.Gender;
            ClassId = teacher.ClassId;
            SubjectIds = teacher.Subjects.Select(s => s.Id);
        }
    }
}