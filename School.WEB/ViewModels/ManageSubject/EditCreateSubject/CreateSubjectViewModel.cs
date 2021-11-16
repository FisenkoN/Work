using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace School.WEB.ViewModels.ManageSubject.EditCreateSubject
{
    public class CreateSubjectViewModel : OperationResultViewModel
    {
        [Required] 
        [StringLength(30, ErrorMessage = "LastName cannot be longer than 30 and shorter than 3 characters", MinimumLength = 3)]
        [DisplayName("Name")]
        public string Name { get; set; }

        public IEnumerable<int> StudentIds { get; set; }

        public IEnumerable<int> TeacherIds { get; set; }
    }
}