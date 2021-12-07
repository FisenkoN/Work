using System.Collections.Generic;
using System.Linq;
using School.WEB.Models;
using School.WEB.ViewModels.ManageClass.DetailsClass;

namespace School.WEB.ViewModels.Visitor.SubjectDetails
{
    public class SubjectDetailsModelView
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public IEnumerable<TeacherModel> Teachers { get; set; }

        public SubjectDetailsModelView(Subject subject)
        {
            Id = subject.Id;
            Name = subject.Name;
            Teachers = from teacherModel in subject.Teachers
                select new TeacherModel
                {
                    Id = teacherModel.Id,
                    FullName = teacherModel.FullName
                };
        }
    }
}