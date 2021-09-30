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
            return Ok(_service.GetStudents());
        }

        // GET: api/Student/5
        [HttpGet("{id:int}")]
        public ActionResult<StudentDto> GetStudent(int? id)
        {
            return Ok(_service.GetStudentForId(id));
        }

        // GET: api/Student/Class/5
        [HttpGet("Class/{id:int}")]
        public ActionResult<ClassDto> GetClass(int? id)
        {
            return Ok(_service.GetClassForId(id));
        }

        // GET: api/Student/GetSubjects
        [HttpGet("GetSubjects")]
        public ActionResult<IEnumerable<SubjectDto>> GetSubjects()
        {
            return Ok(_service.GetSubjects());
        }

        // GET: api/Student/GetClasses
        [HttpGet("GetClasses")]
        public ActionResult<IEnumerable<ClassDto>> GetClasses()
        {
            return Ok(_service.GetClasses());
        }

        // GET: api/Student/GetSubjects/5
        [HttpGet("GetSubjects/{id}")]
        public ActionResult<IEnumerable<SubjectDto>> GetSubjects(int? id)
        {
            return Ok(_service.GetSubjects(id));
        }

        // GET: api/Student/ShowClassmates/5
        [HttpGet("ShowClassmates/{id}")]
        public ActionResult<ICollection<StudentDto>> ShowClassmates(int? id)
        {
            return Ok(_service.GetClassmates(id));
        }

        [HttpGet("ShowClassTeacher/{id}")]
        public ActionResult<TeacherDto> ShowClassTeacher(int? id)
        {
            return Ok(_service.GetMyClassTeacher(id));
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