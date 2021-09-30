using System.Linq;
using School.BLL.Dto;
using School.DAL.Entities;
using School.DAL.Interfaces;

namespace School.BLL.Mapper
{
    public class Map
    {
        private readonly IUnitOfWork _unitOfWork;

        public Map(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public static ClassDto To(Class c)
        {
            return new ClassDto
            {
                Id = c.Id,
                Name = c.Name,
                TeacherId = c.TeacherId,
                CreatedTime = c.CreatedTime,
                LastUpdatedTime = c.LastUpdatedTime,
                StudentIds = c.Students
                    .Select(s => s.Id)
            };
        }

        public static StudentDto To(Student s)
        {
            return new StudentDto
            {
                Id = s.Id,
                Age = s.Age,
                ClassId = s.ClassId,
                FirstName = s.FirstName,
                LastName = s.LastName,
                Gender = (GenderDto)s.Gender,
                CreatedTime = s.CreatedTime,
                LastUpdatedTime = s.LastUpdatedTime,
                Image = s.Image,
                SubjectIds = s.Subjects
                    .Select(subject => subject.Id)
            };
        }

        public static TeacherDto To(Teacher t)
        {
            return new TeacherDto
            {
                Id = t.Id,
                FirstName = t.FirstName,
                LastName = t.LastName,
                Age = t.Age,
                Image = t.Image,
                ClassId = t.ClassId,
                CreatedTime = t.CreatedTime,
                LastUpdatedTime = t.LastUpdatedTime,
                Gender = (GenderDto)t.Gender,
                SubjectIds = t.Subjects?
                    .Select(s => s.Id)
            };
        }

        public static SubjectDto To(Subject s)
        {
            return new SubjectDto
            {
                Id = s.Id,
                Name = s.Name,
                CreatedTime = s.CreatedTime,
                LastUpdatedTime = s.LastUpdatedTime,
                StudentIds = s.Students.Select(z => z.Id),
                TeacherIds = s.Teachers.Select(t => t.Id)
            };
        }

        public Subject To(SubjectDto s)
        {
            return new Subject
            {
                Id = s.Id,
                Name = s.Name,
                CreatedTime = s.CreatedTime,
                LastUpdatedTime = s.LastUpdatedTime,
                Students = s.StudentIds != null
                    ? _unitOfWork.Students.GetAll()
                        .Where(i => s.StudentIds.ToList()
                            .Exists(t => t == i.Id))
                        .ToList()
                    : null,

                Teachers = s.TeacherIds != null
                    ? _unitOfWork.Teachers
                        .GetAll()
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
            return new Class
            {
                Id = c.Id,
                Name = c.Name,
                TeacherId = c.TeacherId,
                CreatedTime = c.CreatedTime,
                LastUpdatedTime = c.LastUpdatedTime,
                Students = _unitOfWork.Students
                    .GetAll()
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
            return new Student
            {
                Id = s.Id,
                Age = s.Age,
                ClassId = s.ClassId,
                FirstName = s.FirstName,
                LastName = s.LastName,
                CreatedTime = s.CreatedTime,
                LastUpdatedTime = s.LastUpdatedTime,
                Image = s.Image,
                Gender = (Gender)s.Gender,
                Subjects = _unitOfWork.Subjects
                    .GetAll()
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
            return new Teacher
            {
                Id = t.Id,
                Age = t.Age,
                ClassId = t.ClassId,
                FirstName = t.FirstName,
                Image = t.Image,
                CreatedTime = t.CreatedTime,
                LastUpdatedTime = t.LastUpdatedTime,
                Gender = (Gender)t.Gender,
                LastName = t.LastName,
                Subjects = t.SubjectIds != null
                    ? _unitOfWork.Subjects
                        .GetAll()
                        .Where(i =>
                            t.SubjectIds
                                .ToList()
                                .Exists(s =>
                                    s == i.Id))
                        .ToList()
                    : null
            };
        }

        public static Blog To(BlogDto blogDto)
        {
            return new Blog
            {
                Id = blogDto.Id,
                Image = blogDto.Image,
                Category = blogDto.Category,
                CreatedTime = blogDto.CreatedTime,
                LastUpdatedTime = blogDto.LastUpdatedTime,
                Name = blogDto.Name,
                Text = blogDto.Text
            };
        }

        public static BlogDto To(Blog blog)
        {
            return new BlogDto
            {
                Id = blog.Id,
                Image = blog.Image,
                CreatedTime = blog.CreatedTime,
                Category = blog.Category,
                LastUpdatedTime = blog.LastUpdatedTime,
                Name = blog.Name,
                Text = blog.Text
            };
        }
    }
}