using School.WEB.Models;

namespace School.WEB.ViewModels.Student.StudentDetails
{
    public class StudentDetailsViewModel
    {
        public StudentModel Student { get; set; }

        public ClassModel Class { get; set; }

        public StudentDetailsViewModel(Models.Student student, Class @class)
        {
            Student = new StudentModel
            {
                Id = student.Id,
                Age = student.Age,
                FirstName = student.FirstName,
                Gender = student.Gender,
                LastName = student.LastName,
                Image = student.Image
            };

            if (@class != null)
            {
                Class = new ClassModel
                {
                    Id = @class.Id,
                    Name = @class.Name
                };
            }
        }
    }
}