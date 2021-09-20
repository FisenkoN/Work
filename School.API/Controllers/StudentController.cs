using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using School.BLL.Dto;
using School.BLL.Services;

namespace School.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly StudentService _service;

        public StudentController(MainService service)
        {
            _service = new StudentService(service);
        }

        // GET: api/Student/
        [HttpGet]
        public ActionResult<IEnumerable<StudentDto>> GetStudents()
        {
            return new ActionResult<IEnumerable<StudentDto>>(_service.GetStudents());
        }

        // GET: api/Student/5
        [HttpGet("{id}")]
        public ActionResult<StudentDto> GetStudent(int? id)
        {
            return _service.GetStudentForId(id);
        }

        // GET: api/Student/ShowClassmates/5
        [HttpGet("ShowClassmates/{id}")]
        public ActionResult<IEnumerable<StudentDto>> ShowClassmates(int? id)
        {
            return new ActionResult<IEnumerable<StudentDto>>(_service.GetClassmates(id));
        }

        [HttpGet("ShowClassTeacher/{id}")]
        public ActionResult<TeacherDto> ShowClassTeacher(int? id)
        {
            return new ActionResult<TeacherDto>(_service.GetMyClassTeacher(id));
        }

        // PUT: api/Student/5
        [HttpPut("{id}")]
        public IActionResult PutStudent(int id,
            StudentDto student)
        {
            if (id != student.Id)
                return BadRequest();

            _service.Edit_FirstName(id,
                student.FirstName);
            _service.Edit_LastName(id,
                student.LastName);
            _service.Edit_Age(id,
                student.Age);
            _service.Edit_Gender(id,
                student.Gender);
            _service.Edit_Image(id,
                student.Image);
            _service.Edit_Subjects(id,
                student.SubjectIds.ToList());
            _service.Edit_Class(id,
                student.ClassId);

            return NoContent();
        }
    }
}