using System.Collections.Generic;
using System.Linq;
using School.BLL.Dto;
using School.BLL.Mapper;
using School.DAL.Entities;
using School.DAL.Interfaces;

namespace School.BLL.Services
{
    public class AdminService
    {
        private readonly Map _map;

        private readonly IUnitOfWork _unitOfWork;

        public AdminService(MainService service)
        {
            _unitOfWork = service.UnitOfWork();

            _map = new Map(_unitOfWork);
        }

        public IEnumerable<StudentDto> Students_GetAll()
        {
            return from s in _unitOfWork
                    .Students
                    .GetAll()
                select _map.To(s);
        }

        public IEnumerable<TeacherDto> Teachers_GetAll()
        {
            return from s in _unitOfWork
                    .Teachers
                    .GetAll()
                select _map.To(s);
        }

        public IEnumerable<ClassDto> Classes_GetAll()
        {
            return from s in _unitOfWork
                    .Classes
                    .GetAll()
                select _map.To(s);
        }

        public ClassDto Classes_GetTeacher(int? teacherId)
        {
            return _map.To(_unitOfWork.Classes
                .GetRelatedData()
                .FirstOrDefault(c =>
                    c.TeacherId == teacherId));
        }

        public IEnumerable<SubjectDto> Subjects_GetAll()
        {
            return from s in _unitOfWork
                    .Subjects
                    .GetAll()
                select _map.To(s);
        }

        public StudentDto Students_GetForId(int? id)
        {
            return _map.To(_unitOfWork
                .Students
                .GetOneRelated(id));
        }

        public ClassDto Classes_GetForId(int? id)
        {
            return _map.To(_unitOfWork
                .Classes
                .GetOneRelated(id));
        }

        public TeacherDto Teachers_GetForId(int? id)
        {
            return _map.To(_unitOfWork
                .Teachers
                .GetOneRelated(id));
        }

        public SubjectDto Subjects_GetForId(int? id)
        {
            return _map.To(_unitOfWork
                .Subjects
                .GetOneRelated(id));
        }

        public IEnumerable<string> Students_GetSubjectsForId(int? id)
        {
            return _unitOfWork
                .Students
                .GetOneRelated(id)
                .Subjects
                .Select(s => s.Name);
        }

        public IEnumerable<string> Teachers_GetSubjectsForId(int? id)
        {
            return _unitOfWork
                .Teachers
                .GetOneRelated(id)
                .Subjects
                .Select(s => s.Name);
        }

        public IEnumerable<string> Subjects_GetStudentsForId(int? id)
        {
            return _unitOfWork
                .Subjects
                .GetOneRelated(id)
                .Students
                .Select(s => s.FirstName + " " + s.LastName);
        }

        public IEnumerable<string> Classes_GetStudentsForId(int? id)
        {
            return _unitOfWork
                .Classes
                .GetOneRelated(id)
                .Students
                .Select(s => s.FirstName + " " + s.LastName);
        }

        public IEnumerable<string> Subjects_GetTeachersForId(int? id)
        {
            return _unitOfWork
                .Subjects
                .GetOneRelated(id)
                .Teachers
                .Select(s => s.FirstName + " " + s.LastName);
        }

        public void Student_Delete(int? id)
        {
            if (id != null)
                _unitOfWork
                    .Students
                    .Delete(id.Value);
        }

        public void Subject_Delete(int? id)
        {
            if (id != null)
                _unitOfWork
                    .Subjects
                    .Delete(id.Value);
        }

        public void Class_Delete(int? id)
        {
            if (id != null)
                _unitOfWork
                    .Classes
                    .Delete(id.Value);
        }

        public void Teacher_Delete(int? id)
        {
            if (id != null)
                _unitOfWork
                    .Teachers
                    .Delete(id.Value);
        }

        public void Student_Create(StudentDto studentDto)
        {
            _unitOfWork
                .Students
                .Add(_map.To(studentDto));
        }

        public void Teacher_Create(TeacherDto teacherDto)
        {
            _unitOfWork
                .Teachers
                .Add(_map.To(teacherDto));
        }

        public void Class_Create(ClassDto classDto)
        {
            _unitOfWork
                .Classes
                .Add(_map.To(classDto));
        }

        public void Subject_Edit_Name(int? id, string name)
        {
            var subject = _unitOfWork
                .Subjects
                .GetOne(id);

            subject.Name = name;

            _unitOfWork
                .Subjects
                .Update(subject);
        }

        public void Subject_Create(SubjectDto subject)
        {
            _unitOfWork
                .Subjects
                .Add(_map.To(subject));
        }

        public void Students_Edit_FirstName(int? id, string firstName)
        {
            var student = _unitOfWork
                .Students
                .GetOne(id);

            student.FirstName = firstName;

            _unitOfWork
                .Students
                .Update(student);
        }

        public void Students_Edit_LastName(int? id, string lastName)
        {
            var student = _unitOfWork
                .Students
                .GetOne(id);

            student.LastName = lastName;

            _unitOfWork
                .Students
                .Update(student);
        }

        public void Students_Edit_Age(int? id, int age)
        {
            var student = _unitOfWork
                .Students
                .GetOne(id);

            student.Age = age;

            _unitOfWork
                .Students
                .Update(student);
        }

        public void Students_Edit_Gender(int? id, GenderDto gender)
        {
            var student = _unitOfWork
                .Students
                .GetOne(id);

            student.Gender = (Gender)gender;

            _unitOfWork
                .Students
                .Update(student);
        }

        public void Students_Edit_Class(int? id, int? classId)
        {
            var student = _unitOfWork
                .Students
                .GetOne(id);

            student.ClassId = classId;

            student.Class = _unitOfWork
                .Classes
                .GetOne(classId);

            _unitOfWork
                .Students
                .Update(student);
        }

        public IEnumerable<ClassDto> GetClassWithOutTeacher()
        {
            return from c in _unitOfWork
                    .Classes
                    .GetRelatedData()
                    .Where(c =>
                        c.TeacherId == null && c.Teacher == null)
                select _map.To(c);
        }

        public void Teachers_Edit_FirstName(int? id, string firstName)
        {
            var teacher = _unitOfWork
                .Teachers
                .GetOne(id);

            teacher.FirstName = firstName;

            _unitOfWork
                .Teachers
                .Update(teacher);
        }

        public void Teachers_Edit_LastName(int? id, string last)
        {
            var teacher = _unitOfWork
                .Teachers
                .GetOne(id);

            teacher.LastName = last;

            _unitOfWork
                .Teachers
                .Update(teacher);
        }

        public void Teachers_Edit_Age(int? id, int age)
        {
            var teacher = _unitOfWork
                .Teachers
                .GetOne(id);

            teacher.Age = age;

            _unitOfWork
                .Teachers
                .Update(teacher);
        }

        public void Teachers_Edit_Gender(int? id, GenderDto gender)
        {
            var teacher = _unitOfWork
                .Teachers
                .GetOne(id);

            teacher.Gender = (Gender)gender;

            _unitOfWork
                .Teachers
                .Update(teacher);
        }

        public void Teachers_Edit_Class(int? id, int? classId)
        {
            var teacher = _unitOfWork
                .Teachers
                .GetOne(id);

            teacher.ClassId = classId;

            teacher.Class = _unitOfWork
                .Classes
                .GetOne(classId);

            _unitOfWork
                .Teachers
                .Update(teacher);
        }

        public IEnumerable<TeacherDto> GetTeachersWithoutClass()
        {
            return from t in _unitOfWork
                    .Teachers
                    .GetAll()
                    .Except(
                        _unitOfWork
                            .Teachers
                            .GetAll()
                            .Where(t =>
                                _unitOfWork
                                    .Classes
                                    .GetRelatedData()
                                    .ToList()
                                    .Exists(c =>
                                        c.TeacherId == t.Id)))
                select _map.To(t);
        }


        public void Class_Edit_Name(int? id, string name)
        {
            var c = _unitOfWork
                .Classes
                .GetOne(id);

            c.Name = name;

            _unitOfWork
                .Classes
                .Update(c);
        }

        public void Class_Edit_Teacher(int? id, int? teacherId)
        {
            var c = _unitOfWork
                .Classes
                .GetOne(id);

            c.TeacherId = teacherId;

            c.Teacher = _unitOfWork
                .Teachers
                .GetOne(teacherId);

            _unitOfWork
                .Classes
                .Update(c);
        }

        public ClassDto GetClassForName(string name)
        {
            return _map.To(_unitOfWork
                .Classes
                .GetAll()
                .FirstOrDefault(c => c.Name == name));
        }


        public void Class_Edit_Students(int? id, List<int> students)
        {
            var @class = _unitOfWork
                .Classes
                .GetOneRelated(id);

            @class.Students.Clear();

            _unitOfWork
                .Classes
                .Update(@class);

            foreach (var i in students)
                Students_Edit_Class(i, id);
        }

        public void Teachers_Edit_Subjects(int? id, List<int> subjects)
        {
            var teacher = _unitOfWork
                .Teachers
                .GetOneRelated(id);

            teacher.Subjects.Clear();

            _unitOfWork
                .Teachers
                .Update(teacher);

            teacher = _unitOfWork
                .Teachers
                .GetOneRelated(id);

            foreach (var t in subjects)
                teacher.Subjects
                    .Add(_unitOfWork
                        .Subjects
                        .GetOne(t));

            _unitOfWork
                .Teachers
                .Update(teacher);
        }

        public void Students_Edit_Subjects(int? id, List<int> subjects)
        {
            var student = _unitOfWork
                .Students
                .GetOneRelated(id);

            student.Subjects.Clear();

            _unitOfWork
                .Students
                .Update(student);

            student = _unitOfWork
                .Students
                .GetOneRelated(id);

            foreach (var t in subjects)
                student.Subjects
                    .Add(_unitOfWork
                        .Subjects
                        .GetOne(t));

            _unitOfWork
                .Students
                .Update(student);
        }

        public void Subjects_Edit_Students(int? id, List<int> students)
        {
            var subject = _unitOfWork
                .Subjects
                .GetOneRelated(id);

            subject.Students.Clear();

            _unitOfWork
                .Subjects
                .Update(subject);

            subject = _unitOfWork
                .Subjects
                .GetOneRelated(id);

            foreach (var i in students)
                subject.Students
                    .Add(_unitOfWork
                        .Students
                        .GetOne(i));

            _unitOfWork
                .Subjects
                .Update(subject);
        }

        public void Subjects_Edit_Teachers(int? id, List<int> teachers)
        {
            var subject = _unitOfWork
                .Subjects
                .GetOneRelated(id);

            subject.Students.Clear();

            _unitOfWork
                .Subjects
                .Update(subject);

            subject = _unitOfWork
                .Subjects
                .GetOneRelated(id);

            foreach (var i in teachers)
                subject
                    .Teachers
                    .Add(_unitOfWork
                        .Teachers
                        .GetOne(i));

            _unitOfWork
                .Subjects
                .Update(subject);
        }
    }
}