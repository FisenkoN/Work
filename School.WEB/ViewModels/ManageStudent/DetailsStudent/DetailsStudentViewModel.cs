using System.Collections.Generic;
using System.Linq;
using School.WEB.Models;

namespace School.WEB.ViewModels.ManageStudent.DetailsStudent
{
    public class DetailsStudentViewModel
    {
        public int Id { get; set; }

        public string FirstName { get; set; }
        
        public string LastName { get; set; }

        public int Age { get; set; }

        public Gender Gender { get; set; }

        public string Image { get; set; }

        public string ClassName { get; set; }

        public IEnumerable<string> SubjectNames { get; set; }

        public DetailsStudentViewModel(Models.Student student, string className)
        {
            Id = student.Id;
            FirstName = student.FirstName;
            LastName = student.LastName;
            Age = student.Age;
            Gender = student.Gender;
            ClassName = className;
            SubjectNames = student.Subjects.Select(s => s.Name);
            Image = student.Image;
        }
    }
}