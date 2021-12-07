using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using School.WEB.Models;

namespace School.WEB.ViewModels.ManageClass.EditCreateClass
{
    public class EditCreateClassViewModel : OperationResultViewModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(5)]
        [MinLength(2, ErrorMessage = "Name cannot be shorter than 2 characters" )]
        [DisplayName("Name")]
        public string Name { get; set; }

        [DisplayName("Students")]
        public IEnumerable<int> StudentIds { get; set; }
        
        [DisplayName("Teachers")]
        public IEnumerable<int> TeacherIds { get; set; }

        [DisplayName("Class teacher")]
        public int? TeacherId { get; set; }

        public EditCreateClassViewModel()
        {
        }

        public EditCreateClassViewModel(Class @class)
        {
            Id = @class.Id;
            Name = @class.Name;
            TeacherIds = @class.Teachers.Select(t => t.Id);
            StudentIds = @class.Students.Select(s => s.Id);
            TeacherId = @class.TeacherId;
        }
    }
}