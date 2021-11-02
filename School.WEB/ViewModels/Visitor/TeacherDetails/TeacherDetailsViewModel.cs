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
        
        public IEnumerable<string> SubjectNames { get; set; }

        public string ClassName { get; set; }
        
        public TeacherDetailsViewModel(Teacher teacher, string className)
        {

            Id = teacher.Id;
            FirstName = teacher.FirstName;
            Image = teacher.Image;
            LastName = teacher.LastName;
            Gender = teacher.Gender;
            Age = teacher.Age;
            SubjectNames = teacher.Subjects.Select(s => s.Name);
            ClassName = className;
        }
    }
}