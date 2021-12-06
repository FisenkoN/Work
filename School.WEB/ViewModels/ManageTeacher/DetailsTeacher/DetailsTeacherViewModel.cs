using System.Collections.Generic;
using System.Linq;
using School.WEB.Models;

namespace School.WEB.ViewModels.ManageTeacher.DetailsTeacher
{
    public class DetailsTeacherViewModel
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int Age { get; set; }

        public Gender Gender { get; set; }

        public ClassModel Class { get; set; }
        
        public IEnumerable<ClassModel> Classes { get; set; }

        public string Image { get; set; }

        public SubjectModel Subject { get; set; }

        public DetailsTeacherViewModel(Teacher teacher, ClassModel @class, SubjectModel subject)
        {
            Id = teacher.Id;
            FirstName = teacher.FirstName;
            LastName = teacher.LastName;
            Age = teacher.Age;
            Gender = teacher.Gender;
            Class = @class;
            Classes = from c in teacher.Classes
                select new ClassModel
                {
                    Id = c.Id,
                    Name = c.Name
                };
            Subject = subject;
            Image = teacher.Image;
        }
    }
}