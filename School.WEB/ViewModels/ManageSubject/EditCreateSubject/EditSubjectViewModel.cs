using System.ComponentModel.DataAnnotations;
using School.WEB.Models;

namespace School.WEB.ViewModels.ManageSubject.EditCreateSubject
{
    public class EditSubjectViewModel
    {
        [Required]
        public int Id { get; set; }

        [Required] 
        [StringLength(30, ErrorMessage = "LastName cannot be longer than 30 and shorter than 3 characters", MinimumLength = 3)]
        public string Name { get; set; }

        public EditSubjectViewModel()
        {
        }

        public EditSubjectViewModel(Subject subject)
        {
            Id = subject.Id;
            Name = subject.Name;
        }
    }
}