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
        private VisitorService _service;

        public VisitorController(MainService service)
        {
            _service = new VisitorService(service);
        }

        // GET: api/Visitor/Class
        [HttpGet("Class")]
        public ActionResult<IEnumerable<ClassDto>> GetClasses()
        {
            return new ActionResult<IEnumerable<ClassDto>>(_service.GetClasses());
        }

        // GET: api/Visitor/Subject
        [HttpGet("Subject")]
        public ActionResult<IEnumerable<SubjectDto>> GetSubjects()
        {
            return new ActionResult<IEnumerable<SubjectDto>>(_service.GetSubjects());
        }

        // GET: api/Visitor/Teacher
        [HttpGet("Teacher")]
        public ActionResult<IEnumerable<TeacherDto>> GetTeachers()
        {
            return new ActionResult<IEnumerable<TeacherDto>>(_service.GetTeachers());
        }

        // GET: api/Visitor/Class/5
        [HttpGet("Class/{id}")]
        public ActionResult<ClassDto> GetClass(int id)
        {
            var @class = _service.GetClass(id);

            return @class == null
                ? NotFound()
                : @class;
        }

        // GET: api/Visitor/Subject/5
        [HttpGet("Subject/{id}")]
        public ActionResult<SubjectDto> GetSubject(int id)
        {
            var subject = _service.GetSubject(id);

            return subject == null
                ? NotFound()
                : subject;
        }

        // GET: api/Visitor/Teacher/5
        [HttpGet("Teacher/{id}")]
        public ActionResult<TeacherDto> GetTeacher(int id)
        {
            var teacher = _service.GetTeacher(id);

            return teacher == null
                ? NotFound()
                : teacher;
        }
    }
}