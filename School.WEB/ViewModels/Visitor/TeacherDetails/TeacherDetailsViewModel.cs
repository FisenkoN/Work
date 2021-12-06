using System.Collections.Generic;
using System.Linq;
using School.WEB.Models;

namespace School.WEB.ViewModels.Visitor.TeacherDetails
{
    public class TeacherDetailsViewModel
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public Gender Gender { get; set; }

        public int Age { get; set; }

        public string Image { get; set; }
        
        public SubjectModel Subject { get; set; }

        public ClassModel Class { get; set; }
        
        public IEnumerable<ClassModel> Classes { get; set; }
        
        public TeacherDetailsViewModel(Teacher teacher, ClassModel @class)
        {
            Id = teacher.Id;
            FirstName = teacher.FirstName;
            Image = teacher.Image;
            LastName = teacher.LastName;
            Gender = teacher.Gender;
            Age = teacher.Age;
            Subject = new SubjectModel
            {
                Id = teacher.Subject.Id,
                Name = teacher.Subject.Name
            };
            Class = @class;
            Classes = from form in teacher.Classes
                select new ClassModel
                {
                    Id = form.Id,
                    Name = form.Name
                };
        }
    }
}