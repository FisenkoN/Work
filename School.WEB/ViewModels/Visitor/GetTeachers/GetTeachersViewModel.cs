using System.Collections.Generic;
using System.Linq;
using School.WEB.Models;

namespace School.WEB.ViewModels.Visitor.GetTeachers
{
    public class GetTeachersViewModel
    {
        public IEnumerable<TeacherModel> Teachers { get; set; }

        public GetTeachersViewModel(IEnumerable<Teacher> teachers)
        {
            Teachers = from teacher in teachers
                select
                    new TeacherModel
                    {
                        Id = teacher.Id,
                        Image = teacher.Image,
                        Active = teacher.Active,
                        FirstName = teacher.FirstName,
                        LastName = teacher.LastName,
                        Age = teacher.Age,
                        Gender = teacher.Gender
                    };
        }
    }
}