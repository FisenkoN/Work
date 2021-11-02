using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace School.WEB.ViewModels.Admin.CreateClass
{
    public class CreateClassViewModel
    {
        [StringLength(10, ErrorMessage = "Name cannot be longer than 10 characters.")]
        [Required]
        public string Name { get; set; }

        [Required]
        public int? TeacherId { get; set; }

        [Required]
        public IEnumerable<int> StudentIds { get; set; }
    }
}