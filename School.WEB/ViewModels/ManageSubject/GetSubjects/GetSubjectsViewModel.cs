using System.Collections.Generic;
using System.Linq;
using School.WEB.Models;

namespace School.WEB.ViewModels.ManageSubject.GetSubjects
{
    public class GetSubjectsViewModel
    {
        public IEnumerable<SubjectModel> Subjects { get; set; }

        public GetSubjectsViewModel(IEnumerable<Subject> subjects)
        {
            Subjects = from subject in subjects
                select new SubjectModel
                {
                    Id = subject.Id,
                    Name = subject.Name
                };
        }
    }
}