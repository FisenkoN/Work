using System.Collections.Generic;
using System.Linq;

namespace School.WEB.ViewModels.Student.Index
{
    public class IndexViewModel : OperationResultViewModel
    {
        public IEnumerable<StudentModel> Students { get; set; }

        public IndexViewModel(IEnumerable<Models.Student> students)
        {
            Students = from student in students
                select
                    new StudentModel
                    {
                        Id = student.Id,
                        FullName = student.FirstName + " " + student.LastName,
                        Image = student.Image
                    };
        }
    }
}