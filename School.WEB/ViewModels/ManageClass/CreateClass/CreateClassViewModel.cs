using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace School.WEB.ViewModels.ManageClass.CreateClass
{
    public class CreateClassViewModel
    {
        [StringLength(10, ErrorMessage = "Name cannot be longer than 10 and shorter than 2 characters", MinimumLength = 2)]
        [Required]
        public string Name { get; set; }

        [Required]
        public int? TeacherId { get; set; }

        [Required]
        public IEnumerable<int> StudentIds { get; set; }
    }
}