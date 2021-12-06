using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using School.WEB.Extensions;
using School.WEB.Models;

namespace School.WEB.ViewModels.ManageTeacher.EditCreateTeacher
{
    public class EditCreateTeacherViewModel : OperationResultViewModel
    {
        public int Id { get; set; }

        [Required]
        [DisplayName("First name")]
        [StringLength(20)]
        [MinLength(2 ,ErrorMessage = "First name cannot be shorter than 2 characters" )]
        public string FirstName { get; set; }

        [Required]
        [DisplayName("Last name")]
        [StringLength(20)]
        [MinLength(2 ,ErrorMessage = "Last name cannot be shorter than 2 characters" )]
        public string LastName { get; set; }

        [RegularExpression(RegexPattern.Url, ErrorMessage = "Incorrect image url")]
        [DisplayName("Image")]
        public string Image { get; set; }

        [Range(18, 80, ErrorMessage = "Age must be more then 18 and less then 80")]
        [DisplayName("Age")]
        public int Age { get; set; }

        [Required]
        [DisplayName("Gender")]
        public Gender Gender { get; set; }

        [DisplayName("My class")]
        public int? ClassId { get; set; }

        [DisplayName("Subject")]
        public int? SubjectId { get; set; }
        
        [DisplayName("Classes")]
        public IEnumerable<int> ClassIds { get; set; }

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
            SubjectId = teacher.SubjectId;
            ClassIds = teacher.Classes.Select(s => s.Id);
        }
    }
}