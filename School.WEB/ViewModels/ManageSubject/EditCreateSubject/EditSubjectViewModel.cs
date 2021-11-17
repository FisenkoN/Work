using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using School.WEB.Models;

namespace School.WEB.ViewModels.ManageSubject.EditCreateSubject
{
    public class EditSubjectViewModel : OperationResultViewModel
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [DisplayName("Name")]
        [StringLength(30)]
        [MinLength(3 ,ErrorMessage = "Name cannot be shorter than 3 characters" )]
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