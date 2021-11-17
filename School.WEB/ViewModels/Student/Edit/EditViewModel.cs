using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using School.WEB.Extensions;
using School.WEB.Models;

namespace School.WEB.ViewModels.Student.Edit
{
    public class EditViewModel : OperationResultViewModel
    {
        [Required]
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

        [Range(5, 18, ErrorMessage = "Age must be more then 5 and less then 18")]
        [DisplayName("Age")]
        public int Age { get; set; }

        [Required]
        [DisplayName("Gender")]
        public Gender Gender { get; set; }
        
        [DisplayName("Class")]
        public int? ClassId { get; set; }

        [RegularExpression(RegexPattern.Url, ErrorMessage = "Incorrect image url")]
        [DisplayName("Image")]
        public string Image { get; set; }

        [DisplayName("Subjects")]
        public IEnumerable<int> SubjectIds { get; set; }

        public EditViewModel()
        {
            
        }

        public EditViewModel(Models.Student student)
        {
            Id = student.Id;
            Age = student.Age;
            ClassId = student.ClassId;
            FirstName = student.FirstName;
            LastName = student.LastName;
            Image = student.Image;
            Gender = student.Gender;
            SubjectIds = student.Subjects.Select(s => s.Id);
        }
    }
}