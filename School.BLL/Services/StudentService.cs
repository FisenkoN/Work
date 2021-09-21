using System.Collections.Generic;
using System.Linq;
using School.BLL.Dto;
using School.BLL.Mapper;
using School.DAL.Entities;
using School.DAL.Interfaces;

namespace School.BLL.Services
{
    public class StudentService
    {
        private readonly IUnitOfWork _unitOfWork;

        private readonly Map _map;

        public StudentService(MainService mainService)
        {
            _unitOfWork = mainService.UnitOfWork();

            _map = new Map(_unitOfWork);
        }

        public IEnumerable<StudentDto> GetStudents() =>
            from s in _unitOfWork.Students
                .GetAll()
            select _map.To(s);

        public ClassDto GetClassForId(int? id) =>
            _map.To(_unitOfWork.Classes.GetOneRelated(id));

        public StudentDto GetStudentForId(int? id) =>
            _map.To(_unitOfWork.Students.GetOneRelated(id));

        public ICollection<StudentDto> GetClassmates(int? id)
        {
            var classId = GetStudentForId(id).ClassId;

            if (classId != null)
                return (from stud in _unitOfWork.Students
                            .GetSome(s =>
                                s.ClassId == classId)
                            .ToList()
                        select _map.To(stud))
                    .ToList();

            return new List<StudentDto>();
        }

        public IEnumerable<SubjectDto> GetSubjects(int? id) =>
            (from subject in _unitOfWork.Subjects
                    .GetAll()
                    .Where(i =>
                        GetStudentForId(id)
                            .SubjectIds
                            .ToList()
                            .Exists(t =>
                                t == i.Id))
                    .ToList()
                select _map.To(subject)).ToList();

        public TeacherDto GetMyClassTeacher(int? id)
        {
            var classId = GetStudentForId(id).ClassId;

            if (classId != null)
                return _map.To(_unitOfWork.Teachers
                    .GetSome(t => t.Id == _unitOfWork.Classes
                        .GetOne(classId)
                        .TeacherId)?
                    .FirstOrDefault());

            return null;
        }

        public void Edit_FirstName(int? id, string firstName)
        {
            var s = _unitOfWork.Students.GetOne(id);

            s.FirstName = firstName;

            _unitOfWork.Students.Update(s);
        }

        public void Edit_LastName(int? id, string firstName)
        {
            var s = _unitOfWork.Students.GetOne(id);

            s.LastName = firstName;

            _unitOfWork.Students.Update(s);
        }

        public void Edit_Age(int? id, int age)
        {
            var s = _unitOfWork.Students.GetOne(id);

            s.Age = age;

            _unitOfWork.Students.Update(s);
        }

        public void Edit_Gender(int? id, GenderDto gender)
        {
            var s = _unitOfWork.Students.GetOne(id);

            s.Gender = (Gender)gender;

            _unitOfWork.Students.Update(s);
        }

        public void Edit_Class(int? id, int? classId)
        {
            var s = _unitOfWork.Students.GetOne(id);

            s.ClassId = classId;

            s.Class = _unitOfWork.Classes.GetOne(classId);

            _unitOfWork.Students.Update(s);
        }

        public void Edit_Subjects(int? id, List<int> subjectIds)
        {
            var s = _unitOfWork.Students.GetOneRelated(id);

            s.Subjects.Clear();

            _unitOfWork.Students.Update(s);

            s = _unitOfWork.Students.GetOneRelated(id);

            foreach (var subjectId in subjectIds)
                s.Subjects.Add(_unitOfWork.Subjects.GetOne(subjectId));

            _unitOfWork.Students.Update(s);
        }

        public IEnumerable<ClassDto> GetClasses()
        {
            return from c in _unitOfWork.Classes
                    .GetAll()
                select _map.To(c);
        }

        public IEnumerable<SubjectDto> GetSubjects()
        {
            return from s in _unitOfWork.Subjects
                    .GetAll()
                select _map.To(s);
        }

        public void Edit_Image(int? id, string studentImage)
        {
            var s = _unitOfWork.Students.GetOne(id);

            s.Image = studentImage;

            _unitOfWork.Students.Update(s);
        }
    }
}