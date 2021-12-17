using System.Collections.Generic;
using System.Linq;
using School.WEB.Models;

namespace School.WEB.ViewModels.ManageTeacher.GetTeachers
{
    public class GetTeachersViewModel : OperationResultViewModel
    {
        public IEnumerable<TeacherModel> Teachers { get; set; }

        public GetTeachersViewModel(IEnumerable<Teacher> teachers)
        {
            Teachers = from teacher in teachers
                select new TeacherModel
                {
                    Id = teacher.Id,
                    Age = teacher.Age,
                    Active = teacher.Active,
                    FirstName = teacher.FirstName,
                    LastName = teacher.LastName,
                    Image = teacher.Image,
                    Gender = teacher.Gender
                };
        }
    }
}