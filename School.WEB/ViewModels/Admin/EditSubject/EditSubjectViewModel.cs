using System.ComponentModel.DataAnnotations;
using School.WEB.Models;

namespace School.WEB.ViewModels.Admin.EditSubject
{
    public class EditSubjectViewModel
    {
        public int Id { get; set; }

        [Required] 
        [MaxLength(30)]
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