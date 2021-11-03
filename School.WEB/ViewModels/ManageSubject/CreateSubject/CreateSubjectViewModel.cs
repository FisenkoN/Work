using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace School.WEB.ViewModels.ManageSubject.CreateSubject
{
    public class CreateSubjectViewModel
    {
        [Required] 
        [StringLength(30, ErrorMessage = "LastName cannot be longer than 30 and shorter than 3 characters", MinimumLength = 3)]
        public string Name { get; set; }

        public IEnumerable<int> StudentIds { get; set; }

        public IEnumerable<int> TeacherIds { get; set; }
    }
}