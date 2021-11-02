using System.Linq;
using School.WEB.Data.Repository;
using School.WEB.DTOs;
using School.WEB.Models;

namespace School.WEB.Mapper
{
    public class Map
    { 
        private readonly IStudentRepository _studentRepository;
        private readonly ITeacherRepository _teacherRepository;
        private readonly ISubjectRepository _subjectRepository;

        public Map(IStudentRepository studentRepository, ITeacherRepository teacherRepository, ISubjectRepository subjectRepository)
        {
            _studentRepository = studentRepository;
            _teacherRepository = teacherRepository;
            _subjectRepository = subjectRepository;
        }

        public ClassDto To(Class c)
        {
            return new()
            {
                Id = c.Id,
                Name = c.Name,
                TeacherId = c.TeacherId,
                StudentIds = c.Students
                    .Select(s => s.Id)
            };
        }

        public StudentDto To(Student s)
        {
            return new()
            {
                Id = s.Id,
                Age = s.Age,
                ClassId = s.ClassId,
                FirstName = s.FirstName,
                LastName = s.LastName,
                Gender = (GenderDto)s.Gender,
                Image = s.Image,
                SubjectIds = s.Subjects
                    .Select(subject => subject.Id)
            };
        }

        public TeacherDto To(Teacher t)
        {
            return new()
            {
                Id = t.Id,
                FirstName = t.FirstName,
                LastName = t.LastName,
                Age = t.Age,
                Image = t.Image,
                ClassId = t.ClassId,
                Gender = (GenderDto)t.Gender,
                SubjectIds = t.Subjects?
                    .Select(s => s.Id)
            };
        }

        public SubjectDto To(Subject s)
        {
            return new()
            {
                Id = s.Id,
                Name = s.Name,
                StudentIds = s.Students.Select(z => z.Id),
                TeacherIds = s.Teachers.Select(t => t.Id)
            };
        }

        public Subject To(SubjectDto s)
        {
            return new()
            {
                Id = s.Id,
                Name = s.Name,
                Students = s.StudentIds != null
                    ? _studentRepository
                        .GetAll()
                        .Result
                        .Where(i =>
                            s.StudentIds
                                .ToList()
                                .Exists(t =>
                                    t == i.Id))
                        .ToList()
                    : null,

                Teachers = s.TeacherIds != null
                    ? _teacherRepository
                        .GetAll()
                        .Result
                        .Where(i =>
                            s.TeacherIds
                                .ToList()
                                .Exists(t =>
                                    t == i.Id))
                        .ToList()
                    : null
            };
        }

        public Class To(ClassDto c)
        {
            return new()
            {
                Id = c.Id,
                Name = c.Name,
                TeacherId = c.TeacherId,
                Students = _studentRepository
                    .GetAll()
                    .Result
                    .Where(i =>
                        c.StudentIds
                            .ToList()
                            .Exists(t =>
                                t == i.Id))
                    .ToList()
            };
        }

        public Student To(StudentDto s)
        {
            return new()
            {
                Id = s.Id,
                Age = s.Age,
                ClassId = s.ClassId,
                FirstName = s.FirstName,
                LastName = s.LastName,
                Image = s.Image,
                Gender = (Gender)s.Gender,
                Subjects = _subjectRepository
                    .GetAll()
                    .Result
                    .Where(i =>
                        s.SubjectIds
                            .ToList()
                            .Exists(t =>
                                t == i.Id))
                    .ToList()
            };
        }

        public Teacher To(TeacherDto t)
        {
            return new()
            {
                Id = t.Id,
                Age = t.Age,
                ClassId = t.ClassId,
                FirstName = t.FirstName,
                Image = t.Image,
                Gender = (Gender)t.Gender,
                LastName = t.LastName,
                Subjects = t.SubjectIds != null
                    ? _subjectRepository
                        .GetAll()
                        .Result
                        .Where(i =>
                            t.SubjectIds
                                .ToList()
                                .Exists(s =>
                                    s == i.Id))
                        .ToList()
                    : null
            };
        }
    }
}