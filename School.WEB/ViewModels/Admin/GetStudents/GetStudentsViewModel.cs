using System.Collections.Generic;
using System.Linq;

namespace School.WEB.ViewModels.Admin.GetStudents
{
    public class GetStudentsViewModel
    {
        public IEnumerable<StudentModel> Students { get; set; }

        public GetStudentsViewModel(IEnumerable<Models.Student> students)
        {
            Students = from student in students
                select new StudentModel
                {
                    Id = student.Id,
                    Age = student.Age,
                    FirstName = student.FirstName,
                    LastName = student.LastName,
                    Gender = student.Gender,
                    Image = student.Image
                };
        }
    }
}