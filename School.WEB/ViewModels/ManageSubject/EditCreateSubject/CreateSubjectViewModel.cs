using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace School.WEB.ViewModels.ManageSubject.EditCreateSubject
{
    public class CreateSubjectViewModel : OperationResultViewModel
    {
        [Required]
        [DisplayName("Name")]
        [StringLength(30)]
        [MinLength(3 ,ErrorMessage = "Name cannot be shorter than 3 characters" )]
        public string Name { get; set; }

        [DisplayName("Teachers")]
        public IEnumerable<int> TeacherIds { get; set; }
    }
}