using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using School.BLL.Dto;
using School.BLL.Services;

namespace School.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VisitorController : ControllerBase
    {
        private readonly VisitorService _service;

        public VisitorController(MainService service)
        {
            _service = new VisitorService(service);
        }

        // GET: api/Visitor/Class
        [HttpGet("Class")]
        public ActionResult<IEnumerable<ClassDto>> GetClasses()
        {
            return Ok(_service.GetClasses());
        }

        // GET: api/Visitor/Subject
        [HttpGet("Subject")]
        public ActionResult<IEnumerable<SubjectDto>> GetSubjects()
        {
            return Ok(_service.GetSubjects());
        }

        // GET: api/Visitor/Teacher
        [HttpGet("Teacher")]
        public ActionResult<IEnumerable<TeacherDto>> GetTeachers()
        {
            return Ok(_service.GetTeachers());
        }

        // GET: api/Visitor/Class/5
        [HttpGet("Class/{id}")]
        public ActionResult<ClassDto> GetClass(int id)
        {
            var @class = _service.GetClass(id);

            return @class == null
                ? NotFound()
                : Ok(@class);
        }

        // GET: api/Visitor/Class/Students/5
        [HttpGet("Class/Students/{id}")]
        public ActionResult<IEnumerable<string>> GetStudentsClass(int id)
        {
            return Ok(_service.GetStudents(id));
        }

        // GET: api/Visitor/Subject/5
        [HttpGet("Subject/{id}")]
        public ActionResult<SubjectDto> GetSubject(int id)
        {
            var subject = _service.GetSubject(id);

            return subject == null
                ? NotFound()
                : Ok(subject);
        }

        // GET: api/Visitor/Teacher/5
        [HttpGet("Teacher/{id}")]
        public ActionResult<TeacherDto> GetTeacher(int id)
        {
            var teacher = _service.GetTeacher(id);

            return teacher == null
                ? NotFound()
                : Ok(teacher);
        }

        // GET: api/Visitor/GetTeacherClass/5
        [HttpGet("GetTeacherClass/{id}")]
        public ActionResult<string> GetTeacherClass(int? id)
        {
            if (id == null)
                return BadRequest();

            return Ok(_service.GetTeachersClass(id));
        }

        // GET: api/Visitor/GetSubjectsForTeacher/5
        [HttpGet("GetSubjectsForTeacher/{id}")]
        public ActionResult<IEnumerable<string>> GetSubjectsForTeacher(int? id)
        {
            return id == null
                ? BadRequest()
                : Ok(_service.GetSubjectsForTeacher(id));
        }

        // GET: api/Visitor/StudentsForSubjectId/5
        [HttpGet("StudentsForSubjectId/{id}")]
        public ActionResult<IEnumerable<string>> StudentsForSubjectId(int? id)
        {
            return id == null
                ? BadRequest()
                : Ok(_service.StudentsForSubjectId(id));
        }

        // GET: api/Visitor/TeachersForSubjectId/5
        [HttpGet("TeachersForSubjectId/{id}")]
        public ActionResult<IEnumerable<string>> TeachersForSubjectId(int? id)
        {
            return id == null
                ? BadRequest()
                : Ok(_service.TeachersForSubjectId(id));
        }
    }
}