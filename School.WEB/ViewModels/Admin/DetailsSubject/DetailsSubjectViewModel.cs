using System.Collections.Generic;
using School.WEB.Models;

namespace School.WEB.ViewModels.Admin.DetailsSubject
{
    public class DetailsSubjectViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public IEnumerable<string> TeacherNames { get; set; }
        
        public IEnumerable<string> StudentNames { get; set; }

        public DetailsSubjectViewModel(Subject s, IEnumerable<string> tNames, IEnumerable<string> SNames)
        {
            Id = s.Id;
            Name = s.Name;
            TeacherNames = tNames;
            StudentNames = SNames;
        }
    }
}