using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using School.WEB.Models;

namespace School.WEB.ViewModels.Admin.EditClass
{
    public class EditClassViewModel
    {
        [Required]
        public int Id { get; set; }

        [Required(ErrorMessage = "It's required value!")]
        [StringLength(10, ErrorMessage = "Name cannot be longer than 10 characters.")]
        public string Name { get; set; }

        public IEnumerable<int> StudentIds { get; set; }

        public int? TeacherId { get; set; }

        public EditClassViewModel(Class @class)
        {
            Id = @class.Id;
            Name = @class.Name;
            StudentIds = @class.Students.Select(s => s.Id);
            TeacherId = @class.TeacherId;
        }
    }
}