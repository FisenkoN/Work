using System.Collections.Generic;
using System.Linq;
using School.WEB.Models;

namespace School.WEB.ViewModels.ManageClass.DetailsClass
{
    public class DetailsClassViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public TeacherModel Teacher { get; set; }

        public IEnumerable<StudentModel> StudentNames { get; set; }
        
        public IEnumerable<TeacherModel> TeacherNames { get; set; }

        public DetailsClassViewModel(Class form)
        {
            Id = form.Id;
            Name = form.Name;
            if (form.TeacherId != null)
            {
                Teacher = new TeacherModel { FullName = form.Teacher.FullName, Id = form.TeacherId.Value };
            }

            StudentNames = from studentModel in form.Students
                select new StudentModel
                {
                    FullName = studentModel.FullName,
                    Id = studentModel.Id
                };
            
            TeacherNames = from teacherModel in form.Teachers
                select new TeacherModel()
                {
                    FullName = teacherModel.FullName,
                    Id = teacherModel.Id
                };
        }
    }
}