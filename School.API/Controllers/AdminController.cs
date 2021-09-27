using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using School.BLL.Dto;
using School.BLL.Services;

namespace School.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly AdminService _service;

        public AdminController(MainService service)
        {
            _service = new AdminService(service);
        }

        // GET: api/Admin/Class
        [HttpGet("Class")]
        public ActionResult<IEnumerable<ClassDto>> GetClasses() =>
            new(_service.Classes_GetAll());
        
        // GET: api/Admin/ClassWithoutTeacher
        [HttpGet("ClassWithoutTeacher")]
        public ActionResult<IEnumerable<ClassDto>> GetClassWithoutTeacher() =>
            new(_service.GetClassWithOutTeacher());

        // GET: api/Admin/Student
        [HttpGet("Student")]
        public ActionResult<IEnumerable<StudentDto>> GetStudents() =>
            new(_service.Students_GetAll());

        // GET: api/Admin/Subject
        [HttpGet("Subject")]
        public ActionResult<IEnumerable<SubjectDto>> GetSubjects() =>
            new(_service.Subjects_GetAll());

        // GET: api/Admin/Teacher
        [HttpGet("Teacher")]
        public ActionResult<IEnumerable<TeacherDto>> GetTeachers() =>
            new(_service.Teachers_GetAll());
        
        // GET: api/Admin/TeachersWithoutClass
        [HttpGet("TeachersWithoutClass")]
        public ActionResult<IEnumerable<TeacherDto>> GetTeachersWithoutClass() =>
            new(_service.GetTeachersWithoutClass());

        // GET: api/Admin/Class/5
        [HttpGet("Class/{id}")]
        public ActionResult<ClassDto> GetClass(int id)
        {
            var @class = _service.Classes_GetForId(id);

            return @class == null
                ? NotFound()
                : @class;
        }
        
        // GET: api/Admin/Classes_GetTeacher/5
        [HttpGet("Classes_GetTeacher/{id}")]
        public ActionResult<ClassDto> Classes_GetTeacher(int id)
        {
            var @class = _service.Classes_GetTeacher(id);

            return @class == null
                ? NotFound()
                : @class;
        }
        
        // GET: api/Admin/Classes_GetTeacher/5
        [HttpGet("Classes_GetStudentsForId/{id}")]
        public ActionResult<IEnumerable<string>> Classes_GetStudentsForId(int id)
        {
            var students = _service.Classes_GetStudentsForId(id);

            return students == null
                ? NotFound()
                : Ok(students);
        }

        // GET: api/Admin/Student/5
        [HttpGet("Student/{id}")]
        public ActionResult<StudentDto> GetStudent(int id)
        {
            var student = _service.Students_GetForId(id);

            return student == null
                ? NotFound()
                : student;
        }
        
        // GET: api/Admin/Student/5
        [HttpGet("Students_GetSubjectsForId/{id}")]
        public ActionResult<IEnumerable<string>> Students_GetSubjectsForId(int id)
        {
            var subjects = _service.Students_GetSubjectsForId(id);

            return subjects == null
                ? NotFound()
                : Ok(subjects);
        }

        // GET: api/Admin/Subject/5
        [HttpGet("Subject/{id}")]
        public ActionResult<SubjectDto> GetSubject(int id)
        {
            var subject = _service.Subjects_GetForId(id);

            return subject == null
                ? NotFound()
                : subject;
        }
        
        // GET: api/Admin/Subject/GetStudentsForId/5
        [HttpGet("Subject/GetStudentsForId/{id}")]
        public ActionResult<IEnumerable<string>> Subjects_GetStudentsForId(int id)
        {
            var students = _service.Subjects_GetStudentsForId(id);

            return students == null
                ? NotFound()
                : Ok(students);
        }
        
        // GET: api/Admin/Subject/GetStudentsForId/5
        [HttpGet("Subject/GetTeachersForId/{id}")]
        public ActionResult<IEnumerable<string>> Subjects_GetTeachersForId(int id)
        {
            var teachers = _service.Subjects_GetTeachersForId(id);

            return teachers == null
                ? NotFound()
                : Ok(teachers);
        }

        // GET: api/Admin/Teacher/5
        [HttpGet("Teacher/{id}")]
        public ActionResult<TeacherDto> GetTeacher(int id)
        {
            var teacher = _service.Teachers_GetForId(id);

            return teacher == null
                ? NotFound()
                : teacher;
        }
        
        // GET: api/Admin/Teacher/5
        [HttpGet("Teacher/GetSubjectsForId/{id}")]
        public ActionResult<IEnumerable<string>> GetSubjectsForId(int id)
        {
            var teacher = _service.Teachers_GetSubjectsForId(id);

            return teacher == null
                ? NotFound()
                : Ok(teacher);
        }

        // PUT: api/Admin/Class/5
        [HttpPut("Class/{id}")]
        public IActionResult PutClass(int id,
            ClassDto @class)
        {
            if (id != @class.Id)
                return BadRequest();

            try
            {
                _service.Class_Edit_Name(id,
                    @class.Name);
                
                _service.Class_Edit_Students(id,
                    @class.StudentIds.ToList());
                
                _service.Class_Edit_Teacher(id,
                    @class.TeacherId);
            }
            catch (Exception)
            {
                if (!ClassExists(id))
                    return NotFound();
            }

            return NoContent();
        }

        // PUT: api/Admin/Student/5
        [HttpPut("Student/{id}")]
        public IActionResult PutStudent(int id,
            StudentDto student)
        {
            if (id != student.Id)
                return BadRequest();

            try
            {
                _service.Students_Edit_FirstName(id,
                    student.FirstName);
                
                _service.Students_Edit_LastName(id,
                    student.LastName);
                
                _service.Students_Edit_Age(id,
                    student.Age);
                
                _service.Students_Edit_Gender(id,
                    student.Gender);
                
                _service.Students_Edit_Image(id,
                    student.Image);
                
                _service.Students_Edit_Subjects(id,
                    student.SubjectIds.ToList());
                
                _service.Students_Edit_Class(id,
                    student.ClassId);
            }
            catch (Exception)
            {
                if (!StudentExists(id))
                    return NotFound();
            }

            return NoContent();
        }

        // PUT: api/Admin/Subject/5
        [HttpPut("Subject/{id}")]
        public IActionResult PutSubject(int id,
            SubjectDto subject)
        {
            if (id != subject.Id)
                return BadRequest();

            try
            {
                _service.Subject_Edit_Name(id,
                    subject.Name);

                _service.Subjects_Edit_Students(id,
                    subject.StudentIds.ToList());
                    
                _service.Subjects_Edit_Teachers(id,
                    subject.TeacherIds.ToList());
            }
            catch (Exception)
            {
                if (!SubjectExists(id))
                    return NotFound();
            }

            return NoContent();
        }

        // PUT: api/Admin/Teacher/5
        [HttpPut("Teacher/{id}")]
        public IActionResult PutTeacher(int id,
            TeacherDto teacher)
        {
            if (id != teacher.Id)
                return BadRequest();

            try
            {
                _service.Teachers_Edit_FirstName(id,
                    teacher.FirstName);
                
                _service.Teachers_Edit_LastName(id,
                    teacher.LastName);
                
                _service.Teachers_Edit_Age(id,
                    teacher.Age);
                
                _service.Teachers_Edit_Gender(id,
                    teacher.Gender);
                
                _service.Teachers_Edit_Image(id,
                    teacher.Image);
                
                _service.Teachers_Edit_Subjects(id,
                    teacher.SubjectIds.ToList());
                
                _service.Teachers_Edit_Class(id,
                    teacher.ClassId);
            }
            catch (Exception)
            {
                if (!TeacherExists(id))
                    return NotFound();
            }

            return NoContent();
        }

        // POST: api/Admin/Class
        [HttpPost("Class")]
        public ActionResult<ClassDto> PostClass(ClassDto @class)
        {
            _service.Class_Create(@class);

            return CreatedAtAction("GetClass",
                new
                {
                    id = @class.Id
                },
                @class);
        }

        // POST: api/Admin/Student
        [HttpPost("Student")]
        public ActionResult<StudentDto> PostStudent(StudentDto student)
        {
            _service.Student_Create(student);

            return CreatedAtAction("GetStudent",
                new
                {
                    id = student.Id
                },
                student);
        }

        // POST: api/Admin/Subject
        [HttpPost("Subject")]
        public ActionResult<SubjectDto> PostSubject(SubjectDto subject)
        {
            _service.Subject_Create(subject);

            return CreatedAtAction("GetSubject",
                new
                {
                    id = subject.Id
                },
                subject);
        }

        // POST: api/Admin/Teacher
        [HttpPost("Teacher")]
        public ActionResult<TeacherDto> PostTeacher(TeacherDto teacher)
        {
            _service.Teacher_Create(teacher);

            return CreatedAtAction("GetTeacher",
                new
                {
                    id = teacher.Id
                },
                teacher);
        }

        // DELETE: api/Admin/Class/5
        [HttpDelete("Class/{id}")]
        public IActionResult DeleteClass(int id)
        {
            var @class = _service.Classes_GetForId(id);
            
            if (@class == null)
                return NotFound();

            _service.Class_Delete(id);

            return NoContent();
        }

        // DELETE: api/Admin/Student/5
        [HttpDelete("Student/{id}")]
        public IActionResult DeleteStudent(int id)
        {
            var student = _service.Students_GetForId(id);
            
            if (student == null)
                return NotFound();

            _service.Student_Delete(id);

            return NoContent();
        }

        // DELETE: api/Admin/Subject/5
        [HttpDelete("Subject/{id}")]
        public IActionResult DeleteSubject(int id)
        {
            var subject = _service.Subjects_GetForId(id);
            
            if (subject == null)
                return NotFound();

            _service.Subject_Delete(id);

            return NoContent();
        }

        // DELETE: api/Admin/Teacher/5
        [HttpDelete("Teacher/{id}")]
        public IActionResult DeleteTeacher(int id)
        {
            var teacher = _service.Teachers_GetForId(id);
            
            if (teacher == null)
                return NotFound();

            _service.Teacher_Delete(id);

            return NoContent();
        }

        private bool ClassExists(int id)
        {
            return _service.Classes_GetAll()
                .Any(e =>
                    e.Id == id);
        }

        private bool StudentExists(int id)
        {
            return _service.Students_GetAll()
                .Any(e => 
                    e.Id == id);
        }

        private bool SubjectExists(int id)
        {
            return _service.Subjects_GetAll()
                .Any(e => 
                    e.Id == id);
        }

        private bool TeacherExists(int id)
        {
            return _service.Teachers_GetAll()
                .Any(e =>
                    e.Id == id);
        }
    }
}