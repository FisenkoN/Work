using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using School.BLL.Dto;
using School.BLL.Services;

namespace School.WEB.Controllers
{
    [Authorize]
    public class VisitorController : Controller
    {
        private readonly VisitorService _service;

        public VisitorController(MainService mainService)
        {
            _service = new VisitorService(mainService);
        }

        // GET
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult GetClasses()
        {
            return View(_service.GetClasses());
        }

        public IActionResult GetTeachers()
        {
            return View(_service.GetTeachers());
        }

        public IActionResult GetSubjects()
        {
            return View(_service.GetSubjects());
        }

        public IActionResult ClassDetails(int? id)
        {
            if (id == null)
                return BadRequest();

            ClassDto @class;

            try
            {
                @class = _service.GetClass(id);
            }
            catch (Exception)
            {
                return NotFound();
            }

            string teacher;

            try
            {
                teacher = _service.GetTeacher(@class.Id)
                    .FullName;
            }
            catch (Exception)
            {
                teacher = "no teacher";
            }

            var students = _service.GetStudents(@class.Id);

            return View(new Tuple<ClassDto, string, IEnumerable<string>>(
                @class,
                teacher,
                students));
        }

        public IActionResult TeacherDetails(int? id)
        {
            if (id == null)
                return BadRequest();

            TeacherDto teacher;

            try
            {
                teacher = _service.GetTeacher(id);
            }
            catch (Exception)
            {
                return NotFound();
            }

            string @class;

            try
            {
                @class = _service.GetTeachersClass(teacher.Id);
            }
            catch (Exception)
            {
                @class = null;
            }

            var subjects = _service.GetSubjectsForTeacher(teacher.Id);

            return View(new Tuple<TeacherDto, string, IEnumerable<string>>(
                teacher,
                @class,
                subjects));
        }

        public IActionResult SubjectDetails(int? id)
        {
            if (id == null)
                return BadRequest();

            SubjectDto subject;

            try
            {
                subject = _service.GetSubject(id);
            }
            catch (Exception)
            {
                return NotFound();
            }

            var teachers = _service.TeachersForSubjectId(subject.Id);

            var students = _service.StudentsForSubjectId(subject.Id);

            return View(new Tuple<SubjectDto, IEnumerable<string>, IEnumerable<string>>(
                subject,
                teachers,
                students));
        }
    }
}