using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using School.WEB.Models;

namespace School.WEB.ViewModels.Student.Edit
{
    public class EditViewModel
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [StringLength(20, ErrorMessage = "FirstName cannot be longer than 20 characters.")]
        public string FirstName { get; set; }

        [Required]
        [StringLength(20, ErrorMessage = "LastName cannot be longer than 20 characters.")]
        public string LastName { get; set; }

        [Range(5, 18, ErrorMessage = "Age must be more then 5 and less then 18")]
        public int Age { get; set; }

        [Required]
        public Gender Gender { get; set; }
        
        public int? ClassId { get; set; }

        public string Image { get; set; }

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