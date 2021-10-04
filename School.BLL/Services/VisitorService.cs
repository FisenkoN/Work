using System.Collections.Generic;
using System.Linq;
using School.BLL.Dto;
using School.BLL.Mapper;
using School.DAL.Interfaces;

namespace School.BLL.Services
{
    public class VisitorService
    {
        private readonly IUnitOfWork _unitOfWork;

        public VisitorService(MainService mainService)
        {
            _unitOfWork = mainService.UnitOfWork();
        }

        public IEnumerable<ClassDto> GetClasses()
        {
            return from c in _unitOfWork.Classes
                    .GetAll()
                select Map.To(c);
        }

        public string GetTeachersClass(int? teacherId)
        {
            return _unitOfWork.Classes
                       .GetRelatedData()
                       .FirstOrDefault(p =>
                           p.TeacherId == teacherId)
                       ?.Name ??
                   "no class";
        }

        public TeacherDto GetTeacher(int? id)
        {
            return Map.To(_unitOfWork.Teachers.GetOneRelated(id));
        }

        public IEnumerable<string> GetSubjectsForTeacher(int? id)
        {
            return _unitOfWork.Teachers
                .GetOneRelated(id)
                .Subjects
                .Select(s =>
                    s.Name);
        }

        public IEnumerable<TeacherDto> GetTeachers()
        {
            return from t in _unitOfWork.Teachers
                    .GetAll()
                select Map.To(t);
        }

        public SubjectDto GetSubject(int? id)
        {
            return Map.To(_unitOfWork.Subjects.GetOneRelated(id));
        }

        public IEnumerable<string> TeachersForSubjectId(int? id)
        {
            return _unitOfWork.Subjects
                .GetOneRelated(id)
                .Teachers
                .Select(s =>
                    s.FirstName + " " + s.LastName);
        }

        public IEnumerable<string> StudentsForSubjectId(int? id)
        {
            return _unitOfWork.Subjects
                .GetOneRelated(id)
                .Students
                .Select(s =>
                    s.FirstName + " " + s.LastName);
        }

        public ClassDto GetClass(int? id)
        {
            return Map.To(_unitOfWork.Classes.GetOneRelated(id));
        }

        public IEnumerable<SubjectDto> GetSubjects()
        {
            return from s in _unitOfWork.Subjects
                    .GetAll()
                select Map.To(s);
        }

        public IEnumerable<string> GetStudents(int? classId)
        {
            return _unitOfWork.Classes
                .GetOneRelated(classId)
                .Students
                .Select(s =>
                    s.FirstName + " " + s.LastName);
        }
    }
}