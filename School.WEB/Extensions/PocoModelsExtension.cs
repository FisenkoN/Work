using System.Linq;
using School.WEB.Data.Repository;
using School.WEB.Models;
using School.WEB.ViewModels.ManageClass.EditCreateClass;
using School.WEB.ViewModels.ManageStudent.EditCreateStudent;
using School.WEB.ViewModels.ManageSubject.EditCreateSubject;
using School.WEB.ViewModels.ManageTeacher.EditCreateTeacher;

namespace School.WEB.Extensions
{
    public static class PocoModelsExtension
    {
        public static Subject To(
            this Subject subject,
            CreateSubjectViewModel model,
            ITeacherRepository teacherRepository)
        {
            subject.Name = model.Name;
            subject.Teachers = model.TeacherIds != null
                ? teacherRepository
                    .GetAll()
                    .Result
                    .Where(i =>
                        model.TeacherIds
                            .ToList()
                            .Exists(t =>
                                t == i.Id))
                    .ToList()
                : null;

            return subject;
        }

        public static Class To(
            this Class @class,
            EditCreateClassViewModel model,
            IStudentRepository repository, ITeacherRepository teacherRepository)
        {
            @class.Name = model.Name;
            @class.TeacherId = model.TeacherId;
            @class.Students = model.StudentIds != null
                ? repository
                    .GetAll()
                    .Result
                    .Where(i =>
                        model.StudentIds
                            .ToList()
                            .Exists(t =>
                                t == i.Id))
                    .ToList()
                : null;
            @class.Teachers = model.TeacherIds != null
                ? teacherRepository
                    .GetAll()
                    .Result
                    .Where(i =>
                        model.TeacherIds
                            .ToList()
                            .Exists(t =>
                                t == i.Id))
                    .ToList()
                : null;

            return @class;
        }

        public static Student To(
            this Student student,
            EditCreateStudentViewModel model)
        {
            student.Age = model.Age;
            student.User = model.User;
            student.UserId = model.UserId;
            student.ClassId = model.ClassId;
            student.LastName = model.LastName;
            student.Gender = model.Gender;
            student.FirstName = model.FirstName;
            student.Image = model.Image;

            return student;
        }

        public static Teacher To(
            this Teacher teacher,
            EditCreateTeacherViewModel model,
            IClassRepository classRepository)
        {
            teacher.Age = model.Age;
            teacher.ClassId = model.ClassId;
            teacher.SubjectId = model.SubjectId;
            teacher.Classes = model.ClassIds != null
                ? classRepository
            teacher.FirstName = model.FirstName;
            teacher.LastName = model.LastName;
            teacher.Gender = model.Gender;
            teacher.User = model.User;
            teacher.UserId = model.UserId;
            teacher.Image = model.Image;
            teacher.FirstName = model.FirstName;
            teacher.LastName = model.LastName;
            teacher.Gender = model.Gender;
            teacher.Image = model.Image;

            return teacher;
        }
    }
}