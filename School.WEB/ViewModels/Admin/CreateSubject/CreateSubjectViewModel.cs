using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace School.WEB.ViewModels.Admin.CreateSubject
{
    public class CreateSubjectViewModel
    {
        [Required] 
        [MaxLength(30)]
        [MinLength(3)]
        public string Name { get; set; }

        public IEnumerable<int> StudentIds { get; set; }

        public IEnumerable<int> TeacherIds { get; set; }
    }
}