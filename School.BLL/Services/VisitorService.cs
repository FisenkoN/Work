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

        private readonly Map _map;

        public VisitorService(MainService mainService)
        {
            _unitOfWork = mainService.UnitOfWork();

            _map = new Map(_unitOfWork);
        }

        public IEnumerable<ClassDto> GetClasses()
        {
            return from c in _unitOfWork.Classes
                    .GetAll()
                select _map.To(c);
        }

        public string GetTeachersClass(int? teacherId)
        {
            var c = _unitOfWork.Classes
                .GetRelatedData()
                .FirstOrDefault(p => p.TeacherId == teacherId);

            return c?.Name ?? "no class";
        }

        public TeacherDto GetTeacher(int? id)
        {
            return _map.To(_unitOfWork.Teachers.GetOneRelated(id));
        }

        public IEnumerable<string> GetSubjectsForTeacher(int? id)
        {
            return _unitOfWork.Teachers
                .GetOneRelated(id)
                .Subjects
                .Select(s => s.Name);
        }

        public IEnumerable<TeacherDto> GetTeachers()
        {
            return from t in _unitOfWork.Teachers
                    .GetAll()
                select _map.To(t);
        }

        public SubjectDto GetSubject(int? id)
        {
            return _map.To(_unitOfWork.Subjects.GetOneRelated(id));
        }

        public IEnumerable<string> TeachersForSubjectId(int? id)
        {
            return _unitOfWork.Subjects
                .GetOneRelated(id)
                .Teachers
                .Select(s => s.FirstName + " " + s.LastName);
        }

        public IEnumerable<string> StudentsForSubjectId(int? id)
        {
            return _unitOfWork.Subjects
                .GetOneRelated(id)
                .Students
                .Select(s => s.FirstName + " " + s.LastName);
        }

        public ClassDto GetClass(int? id)
        {
            return _map.To(_unitOfWork.Classes.GetOneRelated(id));
        }

        public IEnumerable<SubjectDto> GetSubjects()
        {
            return from s in _unitOfWork.Subjects
                    .GetAll()
                select _map.To(s);
        }

        public IEnumerable<string> GetStudents(int? classId)
        {
            return _unitOfWork.Classes
                .GetOneRelated(classId)
                .Students
                .Select(s => s.FirstName + " " + s.LastName);
        }
    }
}