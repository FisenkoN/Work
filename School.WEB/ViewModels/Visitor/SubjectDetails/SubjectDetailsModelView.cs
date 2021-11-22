using System.Collections.Generic;
using System.Linq;
using School.WEB.Models;

namespace School.WEB.ViewModels.Visitor.SubjectDetails
{
    public class SubjectDetailsModelView
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public IEnumerable<string> TeacherNames { get; set; }

        public IEnumerable<string> StudentNames { get; set; }

        public SubjectDetailsModelView(Subject subject)
        {
            Id = subject.Id;
            Name = subject.Name;
            TeacherNames = subject.Teachers.Select(t => t.FullName);
            StudentNames = subject.Students.Select(s => s.FullName);
        }
    }
}