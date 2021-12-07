using System.Collections.Generic;
using System.Linq;
using School.WEB.Models;

namespace School.WEB.ViewModels.Visitor.ClassDetails
{
    public class ClassDetailsViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public TeacherModel ClassTeacher { get; set; }

        public IEnumerable<string> StudentNames { get; set; }

        public IEnumerable<TeacherModel> Teachers { get; set; }

        public ClassDetailsViewModel(Class @class)
        {
            Id = @class.Id;
            Name = @class.Name;
            if (@class.Teacher != null)
            {
                ClassTeacher = new TeacherModel
                {
                    Id = @class.Teacher.Id,
                    FullName = @class.Teacher.FullName
                };
            }
            StudentNames = @class.Students.Select(s => s.FullName);
            Teachers = from teacherModel in @class.Teachers
                select new TeacherModel
                {
                    Id = teacherModel.Id,
                    FullName = teacherModel.FullName
                };
        }
    }
}