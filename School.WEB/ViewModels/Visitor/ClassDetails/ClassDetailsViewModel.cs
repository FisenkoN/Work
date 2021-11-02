using System.Collections.Generic;
using System.Linq;
using School.WEB.Models;

namespace School.WEB.ViewModels.Visitor.ClassDetails
{
    public class ClassDetailsViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string TeacherName { get; set; }

        public IEnumerable<string> StudentNames { get; set; }

        public ClassDetailsViewModel(Class @class)
        {
            Id = @class.Id;
            Name = @class.Name;
            TeacherName = @class.Teacher.FirstName + " " + @class.Teacher.LastName;
            StudentNames = @class.Students.Select(s => s.FirstName + " " + s.LastName);
        }
    }
}