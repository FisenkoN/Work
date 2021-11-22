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

        public string ClassName { get; set; }

        public string Image { get; set; }

        public IEnumerable<string> SubjectNames { get; set; }

        public DetailsTeacherViewModel(Teacher teacher, string className)
        {
            Id = teacher.Id;
            FirstName = teacher.FirstName;
            LastName = teacher.LastName;
            Age = teacher.Age;
            Gender = teacher.Gender;
            ClassName = className;
            SubjectNames = teacher.Subjects.Select(s => s.Name);
            Image = teacher.Image;
        }
    }
}