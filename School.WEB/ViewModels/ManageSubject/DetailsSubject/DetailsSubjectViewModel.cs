using System.Collections.Generic;
using System.Linq;
using School.WEB.Models;

namespace School.WEB.ViewModels.ManageSubject.DetailsSubject
{
    public class DetailsSubjectViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public IEnumerable<TeacherModel> Teachers { get; set; }

        public DetailsSubjectViewModel(Subject s)
        {
            Id = s.Id;
            Name = s.Name;
            Teachers = from t in s.Teachers
                select new TeacherModel
                {
                    Id = t.Id,
                    FullName = t.FullName
                };
        }
    }
}