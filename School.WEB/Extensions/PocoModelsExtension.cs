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
        public static Subject To(this Subject subject, CreateSubjectViewModel model, IStudentRepository studentRepository, ITeacherRepository teacherRepository)
        {
            subject.Name = model.Name;
            subject.Students = model.StudentIds != null
                ? studentRepository
                    .GetAll()
                    .Result
                    .Where(i =>
                        model.StudentIds
                            .ToList()
                            .Exists(t =>
                                t == i.Id))
                    .ToList()
                : null;
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
        
        public static Class To(this Class @class, EditCreateClassViewModel model, IStudentRepository repository)
        {
            @class.Name = model.Name;
            @class.TeacherId = model.TeacherId;
            @class.Students = repository
                .GetAll()
                .Result
                .Where(i =>
                    model.StudentIds
                        .ToList()
                        .Exists(t =>
                            t == i.Id))
                .ToList();

            return @class;
        }

        public static Student To(this Student student, EditCreateStudentViewModel model, ISubjectRepository subjectRepository)
        {
            student.Age = model.Age;
            student.ClassId = model.ClassId;
            student.LastName = model.LastName;
            student.Gender = model.Gender;
            student.FirstName = model.FirstName;
            student.Image = model.Image;
            student.Subjects = subjectRepository
                .GetAll()
                .Result
                .Where(i =>
                    model.SubjectIds
                        .ToList()
                        .Exists(t =>
                            t == i.Id))
                .ToList();

            return student;
        }

        public static Teacher To(this Teacher teacher, EditCreateTeacherViewModel model, ISubjectRepository subjectRepository)
        {
            teacher.Age = model.Age;
            teacher.ClassId = model.ClassId;
            teacher.FirstName = model.FirstName;
            teacher.LastName = model.LastName;
            teacher.Gender = model.Gender;
            teacher.Image = model.Image;
            teacher.Subjects = model.SubjectIds != null
                ? subjectRepository
                    .GetAll()
                    .Result
                    .Where(i =>
                        model.SubjectIds
                            .ToList()
                            .Exists(s =>
                                s == i.Id))
                    .ToList()
                : null;

            return teacher;
        }
    }
}