using System.Collections.Generic;

namespace School.WEB.ViewModels.Visitor
{
    public class SubjectDetailsModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public IEnumerable<string> TeacherNames { get; set; }

        public IEnumerable<string> StudentNames { get; set; }
    }
}