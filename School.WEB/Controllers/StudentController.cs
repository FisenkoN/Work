using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using School.BLL.Dto;
using School.BLL.Services;

namespace School.WEB.Controllers
{
    public class StudentController : Controller
    {
        private readonly StudentService _service;

        public StudentController(MainService service)
        {
            _service = new StudentService(service);
        }

        // GET
        public IActionResult Index()
        {
            return View(_service.GetStudents());
        }

        public IActionResult StudentDetails(int? id)
        {
            if (id == null)
                return BadRequest();

            StudentDto student;

            try
            {
                student = _service.GetStudentForId(id);
            }
            catch (Exception)
            {
                return NotFound();
            }

            var @class = student.ClassId != null
                ? _service.GetClassForId(student.ClassId)
                    ?.Name
                : "no class";

            var subjects = _service.GetSubjects(student.Id);

            var subjectNames = subjects.Select(s => s.Name);

            return View(new Tuple<StudentDto, string, IEnumerable<string>>(student,
                @class,
                subjectNames));
        }

        public IActionResult Edit(int? id)
        {
            if (id == null)
                return BadRequest();

            StudentDto student;

            try
            {
                student = _service.GetStudentForId(id);
            }
            catch (Exception)
            {
                return NotFound();
            }

            var classes = _service.GetClasses();

            var subjects = _service.GetSubjects();

            ViewData["Classes"] = new SelectList(classes,
                "Id",
                "Name");

            ViewData["Subjects"] = new SelectList(subjects,
                "Id",
                "Name");

            return View(student);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int? id,
            [Bind("Id, FirstName, LastName, Age, Image, Gender, ClassId, SubjectIds")]
            StudentDto student)
        {
            if (id != student.Id)
                return NotFound();

            _service.Edit_FirstName(id,
                student.FirstName);

            _service.Edit_LastName(id,
                student.LastName);

            _service.Edit_Age(id,
                student.Age);

            _service.Edit_Gender(id,
                student.Gender);

            _service.Edit_Class(id,
                student.ClassId);
            
            _service.Edit_Image(id,
                student.Image);

            _service.Edit_Subjects(id,
                student.SubjectIds.ToList());

            return RedirectToAction(nameof(Index));
        }

        public IActionResult ShowClassmates(int? id)
        {
            if (id == null)
                return BadRequest();

            ICollection<StudentDto> classmates;

            try
            {
                classmates = _service.GetClassmates(id);
            }
            catch (Exception)
            {
                return NoContent();
            }

            if (classmates.Count == 0)
                return NoContent();

            return View(classmates);
        }

        public IActionResult ShowClassTeacher(int? id)
        {
            if (id == null)
                return BadRequest();

            TeacherDto teacher;

            try
            {
                teacher = _service.GetMyClassTeacher(id);
            }
            catch (Exception)
            {
                return NoContent();
            }

            if (teacher == null)
                return NoContent();

            return View(teacher);
        }
    }
}