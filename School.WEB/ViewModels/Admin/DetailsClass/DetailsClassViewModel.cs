using System.Collections.Generic;
using System.Linq;
using School.WEB.Models;

namespace School.WEB.ViewModels.Admin.DetailsClass
{
    public class DetailsClassViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string TeacherName { get; set; }

        public IEnumerable<string> StudentNames { get; set; }

        public DetailsClassViewModel(Class form, string teacherName)
        {
            Id = form.Id;
            Name = form.Name;
            TeacherName = teacherName;
            StudentNames = form.Students.Select(s => s.FullName);
        }
    }
}