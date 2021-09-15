using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using School.BLL.Dto;
using School.BLL.Services;

namespace School.WEB.Controllers
{
    public class AdminController : Controller
    {
        private readonly AdminService _service;

        public AdminController(MainService service)
        {
            _service = new AdminService(service);
        }

        // GET
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult GetTeachers()
        {
            var teachers = _service.Teachers_GetAll();

            if (!teachers.Any())
                return NoContent();

            return View(teachers);
        }

        public IActionResult GetClasses()
        {
            var classes = _service.Classes_GetAll();

            if (!classes.Any())
                return NoContent();

            return View(classes);
        }

        public IActionResult GetStudents()
        {
            var students = _service.Students_GetAll();

            if (!students.Any())
                return NoContent();

            return View(students);
        }

        public IActionResult GetSubjects()
        {
            var subjects = _service.Subjects_GetAll();

            if (!subjects.Any())
                return NoContent();

            return View(subjects);
        }

        public IActionResult CreateTeacher()
        {
            ViewData["Classes"] = new SelectList(
                _service.GetClassWithOutTeacher(),
                "Id",
                "Name");

            ViewData["Subjects"] = new SelectList(
                _service.Subjects_GetAll(),
                "Id",
                "Name");

            return View();
        }

        [HttpPost]
        public IActionResult CreateTeacher(
            [Bind("FirstName, LastName, ClassId, SubjectIds, Gender, Id, Age")]
            TeacherDto teacherDto)
        {
            _service.Teacher_Create(teacherDto);

            return RedirectToAction("GetTeachers");
        }

        public IActionResult EditTeacher(int? id)
        {
            if (id == null)
                return BadRequest();

            TeacherDto teacher;

            try
            {
                teacher = _service.Teachers_GetForId(id);
            }
            catch (Exception)
            {
                return NotFound();
            }

            var classes = _service.GetClassWithOutTeacher();

            try
            {
                classes = classes.Append(_service.Classes_GetTeacher(teacher.Id));
            }
            catch (Exception)
            {
                Console.WriteLine();
            }

            var subjects = _service.Subjects_GetAll();

            try
            {
                var selectedClass = _service.Classes_GetTeacher(teacher.Id);

                ViewData["Classes"] = new SelectList(classes,
                    "Id",
                    "Name",
                    selectedClass);
            }
            catch (Exception)
            {
                ViewData["Classes"] = new SelectList(classes,
                    "Id",
                    "Name");
            }

            ViewData["Subjects"] = new SelectList(subjects,
                "Id",
                "Name");

            return View(teacher);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditTeacher(int? id,
            [Bind("Id, FirstName, LastName, Age, Gender, ClassId, SubjectIds")]
            TeacherDto teacher)
        {
            if (id != teacher.Id)
                return NotFound();

            _service.Teachers_Edit_FirstName(id,
                teacher.FirstName);

            _service.Teachers_Edit_LastName(id,
                teacher.LastName);

            _service.Teachers_Edit_Age(id,
                teacher.Age);

            _service.Teachers_Edit_Gender(id,
                teacher.Gender);

            _service.Teachers_Edit_Class(id,
                teacher.ClassId);

            _service.Teachers_Edit_Subjects(id,
                teacher.SubjectIds.ToList());

            return RedirectToAction("GetTeachers");
        }

        public IActionResult DeleteTeacher(int? id)
        {
            if (id == null)
                return BadRequest();

            try
            {
                if (_service.Teachers_GetForId(id) == null)
                    return NotFound();
            }
            catch (Exception)
            {
                return NotFound();
            }

            try
            {
                _service.Teacher_Delete(id);
                return RedirectToAction("GetTeachers");
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        public IActionResult DetailsTeacher(int? id)
        {
            if (id == null)
                return BadRequest();

            TeacherDto teacher;

            try
            {
                teacher = _service.Teachers_GetForId(id);
            }
            catch (Exception e)
            {
                return NotFound();
            }

            var subjects = _service.Teachers_GetSubjectsForId(teacher.Id);

            var @class = _service
                             .Classes_GetAll()
                             .FirstOrDefault(c =>
                                 c.TeacherId == teacher.Id)
                             ?.Name ??
                         "no class";

            return View((teacher, @class, subjects));
        }

        public IActionResult CreateStudent()
        {
            ViewData["Classes"] = new SelectList(_service.Classes_GetAll(),
                "Id",
                "Name");

            ViewData["Subjects"] = new SelectList(_service.Subjects_GetAll(),
                "Id",
                "Name");

            return View();
        }

        [HttpPost]
        public IActionResult CreateStudent(
            [Bind("FirstName, LastName, ClassId, SubjectIds, Gender, Id, Age")]
            StudentDto teacherDto)
        {
            try
            {
                _service.Student_Create(teacherDto);
            }
            catch (Exception)
            {
                return BadRequest("You entered wrong value. Every students must have subject");
            }

            return RedirectToAction("GetStudents");
        }

        public IActionResult EditStudent(int? id)
        {
            if (id == null)
                return BadRequest();

            StudentDto student;

            try
            {
                student = _service.Students_GetForId(id);
            }
            catch (Exception)
            {
                return NotFound();
            }

            var classes = _service.Classes_GetAll();

            var subjects = _service.Subjects_GetAll();

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
        public IActionResult EditStudent(int? id,
            [Bind("Id, FirstName, LastName, Age, Gender, ClassId, SubjectIds")]
            StudentDto student)
        {
            if (id != student.Id)
                return NotFound();

            _service.Students_Edit_FirstName(id,
                student.FirstName);

            _service.Students_Edit_LastName(id,
                student.LastName);

            _service.Students_Edit_Age(id,
                student.Age);

            _service.Students_Edit_Gender(id,
                student.Gender);

            _service.Students_Edit_Class(id,
                student.ClassId);

            try
            {
                _service.Students_Edit_Subjects(id,
                    student.SubjectIds.ToList());
            }
            catch (Exception)
            {
                return RedirectToAction("GetStudents");
            }

            return RedirectToAction("GetStudents");
        }

        public IActionResult DeleteStudent(int? id)
        {
            if (id == null)
                BadRequest();

            try
            {
                if (_service.Students_GetForId(id) == null)
                    return NotFound();
            }
            catch (Exception)
            {
                return NotFound();
            }

            try
            {
                _service.Student_Delete(id);
                return RedirectToAction("GetStudents");
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        public IActionResult DetailsStudent(int? id)
        {
            if (id == null)
                return BadRequest();

            StudentDto student;

            try
            {
                student = _service.Students_GetForId(id);
            }
            catch (Exception)
            {
                return NotFound();
            }

            var subjects = _service.Students_GetSubjectsForId(student.Id);

            var @class = student.ClassId != null
                ? _service
                    .Classes_GetForId(student.ClassId)
                    .Name
                : "no class";

            return View((student, @class, subjects));
        }

        public IActionResult CreateClass()
        {
            ViewData["Students"] = new SelectList(_service.Students_GetAll(),
                "Id",
                "FullName");

            ViewData["Teachers"] = new SelectList(_service.GetTeachersWithoutClass(),
                "Id",
                "FullName");

            return View();
        }

        [HttpPost]
        public IActionResult CreateClass(
            [Bind("Id, Name, StudentIds, TeacherId")]
            ClassDto classDto)
        {
            try
            {
                _service.Class_Create(classDto);
            }
            catch (Exception)
            {
                return BadRequest("Every class must have one or more students");
            }

            return RedirectToAction("GetClasses");
        }

        public IActionResult CreateSubject()
        {
            ViewData["Students"] = new SelectList(_service.Students_GetAll(),
                "Id",
                "FullName");

            ViewData["Teachers"] = new SelectList(_service.Teachers_GetAll(),
                "Id",
                "FullName");

            return View();
        }

        [HttpPost]
        public IActionResult CreateSubject(
            [Bind("Id, Name, StudentIds, TeacherIds")]
            SubjectDto subject)
        {
            _service.Subject_Create(subject);

            return RedirectToAction("GetSubjects");
        }

        public IActionResult EditClass(int? id)
        {
            if (id == null)
                return BadRequest();

            ClassDto classDto;

            try
            {
                classDto = _service.Classes_GetForId(id);
            }
            catch (Exception)
            {
                return NotFound();
            }

            var students = _service.Students_GetAll();

            var teachers = _service.GetTeachersWithoutClass();

            try
            {
                teachers = teachers.Append(_service.Teachers_GetForId(classDto.TeacherId));
            }
            catch (Exception)
            {
                Console.WriteLine();
            }

            ViewData["Students"] = new SelectList(students,
                "Id",
                "FullName");

            ViewData["Teachers"] = new SelectList(teachers,
                "Id",
                "FullName");

            return View(classDto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditClass(int? id,
            [Bind("Id, Name, StudentIds, TeacherId")]
            ClassDto classDto)
        {
            if (id != classDto.Id)
                return NotFound();

            _service.Class_Edit_Name(id,
                classDto.Name);

            _service.Class_Edit_Students(id,
                classDto.StudentIds.ToList());

            _service.Class_Edit_Teacher(id,
                classDto.TeacherId);

            return RedirectToAction("GetClasses");
        }

        public IActionResult DeleteClass(int? id)
        {
            if (id == null)
                return BadRequest();

            try
            {
                if (_service.Classes_GetForId(id) == null)
                    return NotFound();
            }
            catch (Exception)
            {
                return NotFound();
            }

            try
            {
                _service.Class_Delete(id);
                return RedirectToAction("GetClasses");
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        public IActionResult DetailsClass(int? id)
        {
            if (id == null)
                return BadRequest();

            ClassDto @class;

            try
            {
                @class = _service.Classes_GetForId(id);
            }
            catch (Exception)
            {
                return NotFound();
            }

            var students = _service.Classes_GetStudentsForId(@class.Id);

            var teacher = @class.TeacherId != null
                ? _service
                    .Teachers_GetForId(@class.TeacherId)
                    ?
                    .FullName
                : "no teacher";

            return View((@class, students, teacher));
        }

        public IActionResult EditSubject(int? id)
        {
            if (id == null)
                return BadRequest();

            SubjectDto subject;

            try
            {
                subject = _service.Subjects_GetForId(id);
            }
            catch (Exception)
            {
                return NotFound();
            }

            var students = _service.Students_GetAll();

            var teachers = _service.Teachers_GetAll();

            ViewData["Students"] = new SelectList(students,
                "Id",
                "FullName");

            ViewData["Teachers"] = new SelectList(teachers,
                "Id",
                "FullName");

            return View(subject);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditSubject(int? id,
            [Bind("Id, Name, StudentIds, TeacherIds")]
            SubjectDto subject)
        {
            if (id != subject.Id)
                return NotFound();

            _service.Subject_Edit_Name(id,
                subject.Name);

            _service.Subjects_Edit_Students(id,
                subject.StudentIds.ToList());

            _service.Subjects_Edit_Teachers(id,
                subject.TeacherIds.ToList());

            return RedirectToAction("GetSubjects");
        }

        public IActionResult DeleteSubject(int? id)
        {
            if (id == null)
                return BadRequest();

            try
            {
                if (_service.Subjects_GetForId(id) == null)
                    return NotFound();
            }
            catch (Exception)
            {
                return NotFound();
            }

            try
            {
                _service.Subject_Delete(id);
                return RedirectToAction("GetSubjects");
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        public IActionResult DetailsSubject(int? id)
        {
            if (id == null)
                return BadRequest();

            SubjectDto subject;

            try
            {
                subject = _service.Subjects_GetForId(id);
            }
            catch (Exception)
            {
                return NotFound();
            }

            var students = _service.Subjects_GetStudentsForId(subject.Id);

            var teachers = _service.Subjects_GetTeachersForId(subject.Id);

            return View((subject, students, teachers));
        }
    }
}