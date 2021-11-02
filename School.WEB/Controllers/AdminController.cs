using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using School.WEB.Data.Repository;
using School.WEB.Models;
using School.WEB.ViewModels.Admin.CreateClass;
using School.WEB.ViewModels.Admin.CreateStudent;
using School.WEB.ViewModels.Admin.CreateSubject;
using School.WEB.ViewModels.Admin.CreateTeacher;
using School.WEB.ViewModels.Admin.DetailsClass;
using School.WEB.ViewModels.Admin.DetailsStudent;
using School.WEB.ViewModels.Admin.DetailsSubject;
using School.WEB.ViewModels.Admin.DetailsTeacher;
using School.WEB.ViewModels.Admin.EditClass;
using School.WEB.ViewModels.Admin.EditStudent;
using School.WEB.ViewModels.Admin.EditSubject;
using School.WEB.ViewModels.Admin.EditTeacher;
using School.WEB.ViewModels.Admin.GetClasses;
using School.WEB.ViewModels.Admin.GetStudents;
using School.WEB.ViewModels.Admin.GetSubjects;
using School.WEB.ViewModels.Admin.GetTeachers;

namespace School.WEB.Controllers
{
    [Authorize]
    [Route("[controller]")]
    public class AdminController : Controller
    {
        private readonly IStudentRepository _studentRepository;
        private readonly IClassRepository _classRepository;
        private readonly ISubjectRepository _subjectRepository;
        private readonly ITeacherRepository _teacherRepository;

        public AdminController(IStudentRepository studentRepository,
            ITeacherRepository teacherRepository,
            ISubjectRepository subjectRepository,
            IClassRepository classRepository)
        {
            _studentRepository = studentRepository;
            _classRepository = classRepository;
            _subjectRepository = subjectRepository;
            _teacherRepository = teacherRepository;
        }

        [HttpGet("[action]")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetTeachers()
        {
            if (TempData["Message"] != null)
                ViewBag.Message = TempData["Message"]
                    .ToString();

            var teachers = await _teacherRepository.GetAll();

            var model = new GetTeachersViewModel(teachers);

            return View(model);
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetClasses()
        {
            if (TempData["Message"] != null)
                ViewBag.Message = TempData["Message"]
                    .ToString();

            var classes = await _classRepository.GetAll();

            var model = new GetClassesViewModel(classes);

            return View(model);
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetStudents()
        {
            if (TempData["Message"] != null)
                ViewBag.Message = TempData["Message"]
                    .ToString();

            var students = await _studentRepository.GetAll();

            var model = new GetStudentsViewModel(students);

            return View(model);
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetSubjects()
        {
            if (TempData["Message"] != null)
                ViewBag.Message = TempData["Message"]
                    .ToString();

            var subjects = await _subjectRepository.GetAll();

            var model = new GetSubjectsViewModel(subjects);

            return View(model);
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> CreateTeacher()
        {
            var subjects = await _subjectRepository.GetAll();

            var classes = _classRepository.GetRelatedData()
                .Where(c =>
                    c.TeacherId == null && c.Teacher == null);

            ViewData["Classes"] = new SelectList(classes,
                "Id",
                "Name");

            ViewData["Subjects"] = new SelectList(subjects,
                "Id",
                "Name");

            return View();
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> CreateTeacher(
            [Bind("FirstName, Image, LastName, ClassId, SubjectIds, Gender, Id, Age")]
            CreateTeacherViewModel teacher)
        {
            if (!ModelState.IsValid)
            {
                var subjects = await _subjectRepository.GetAll();

                var classes = _classRepository.GetRelatedData()
                    .Where(c =>
                        c.TeacherId == null && c.Teacher == null);

                ViewData["Classes"] = new SelectList(classes,
                    "Id",
                    "Name");

                ViewData["Subjects"] = new SelectList(subjects,
                    "Id",
                    "Name");

                return View(teacher);
            }
            
            await _teacherRepository.Add(new Teacher
            {
                Age = teacher.Age,
                ClassId = teacher.ClassId,
                FirstName = teacher.FirstName,
                LastName = teacher.LastName,
                Gender = teacher.Gender,
                Image = teacher.Image,
                Subjects = teacher.SubjectIds != null
                    ? _subjectRepository
                        .GetAll()
                        .Result
                        .Where(i =>
                            teacher.SubjectIds
                                .ToList()
                                .Exists(s =>
                                    s == i.Id))
                        .ToList()
                    : null
            });

            TempData["Message"] =
                $"Teacher: {teacher.FirstName + " " + teacher.LastName} was created at {DateTime.Now.ToShortTimeString()}";

            return RedirectToAction("GetTeachers");
        }

        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> EditTeacher(int id)
        {
            var teacher = await _teacherRepository.GetOneRelated(id);

            var classes = await _classRepository.GetRelatedData()
                .Where(c =>
                    c.TeacherId == null && c.Teacher == null)
                .ToListAsync();

            try
            {
                var c = _classRepository.GetRelatedData()
                    .FirstOrDefault(c =>
                        c.TeacherId == id);
                
                if(c!=null)
                    classes.Add(c);
            }
            catch (Exception)
            {
                Console.WriteLine();
            }

            var subjects = await _subjectRepository.GetAll();

            try
            {
                var selectedClass = _classRepository.GetRelatedData()
                    .FirstOrDefault(c =>
                        c.TeacherId == id);

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


            var model = new EditTeacherViewModel(teacher);

            return View(model);
        }

        [HttpPost("[action]/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditTeacher(int id,
            [Bind("Id, FirstName, LastName, Image, Age, Gender, ClassId, SubjectIds")]
            EditTeacherViewModel editTeacherViewModel)
        {
            if (!ModelState.IsValid) return View(editTeacherViewModel);
            
            var teacher = await _teacherRepository.GetOne(id);

            teacher.FirstName = editTeacherViewModel.FirstName;
            teacher.LastName = editTeacherViewModel.LastName;
            teacher.Age = editTeacherViewModel.Age;
            teacher.Gender = editTeacherViewModel.Gender;
            teacher.Image = editTeacherViewModel.Image;

            await _teacherRepository.Update(teacher);

            teacher = await _teacherRepository.GetOne(id);

            teacher.ClassId = editTeacherViewModel.ClassId;

            teacher.Class = await _classRepository.GetOne(editTeacherViewModel.ClassId);

            await _teacherRepository.Update(teacher);

            teacher = await _teacherRepository.GetOneRelated(id);

            teacher.Subjects.Clear();

            await _teacherRepository.Update(teacher);

            teacher = await _teacherRepository.GetOneRelated(id);

            foreach (var t in editTeacherViewModel.SubjectIds)
                teacher.Subjects
                    .Add(await _subjectRepository
                        .GetOne(t));

            await _teacherRepository.Update(teacher);

            TempData["Message"] = $"Teacher: {_teacherRepository.GetOne(id).Result.FullName} was edited at {DateTime.Now.ToShortTimeString()}";

            return RedirectToAction("GetTeachers");
        }

        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> DeleteTeacher(int id)
        {
            await _teacherRepository.Delete(id);

            TempData["Message"] = $"Teacher with id : {id} was deleted at {DateTime.Now.ToShortTimeString()}";

            return RedirectToAction("GetTeachers");

        }

        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> DetailsTeacher(int id)
        {
            var teacher = await _teacherRepository.GetOneRelated(id);

            var @class = _classRepository
                             .GetAll()
                             .Result
                             .FirstOrDefault(c =>
                                 c.TeacherId == teacher.Id)
                             ?.Name ??
                         "no class";

            var model = new DetailsTeacherViewModel(teacher,
                @class);

            return View(model);
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> CreateStudent()
        {
            ViewData["Classes"] = new SelectList(await _classRepository.GetAll(),
                "Id",
                "Name");

            ViewData["Subjects"] = new SelectList(await _subjectRepository.GetAll(),
                "Id",
                "Name");

            return View();
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> CreateStudent(
            [Bind("FirstName, LastName, Image, ClassId, SubjectIds, Gender, Id, Age")]
            CreateStudentViewModel createStudentViewModel)
        {
            if (!ModelState.IsValid) return View(createStudentViewModel);
            
            await _studentRepository.Add(new Student
            {
                Age = createStudentViewModel.Age,
                ClassId = createStudentViewModel.ClassId,
                LastName = createStudentViewModel.LastName,
                Gender = createStudentViewModel.Gender,
                FirstName = createStudentViewModel.FirstName,
                Image = createStudentViewModel.Image,
                Subjects = _subjectRepository
                    .GetAll()
                    .Result
                    .Where(i =>
                        createStudentViewModel.SubjectIds
                            .ToList()
                            .Exists(t =>
                                t == i.Id))
                    .ToList()
            });

            TempData["Message"] =
                $"Student: {createStudentViewModel.FirstName + " " + createStudentViewModel.LastName} was created at {DateTime.Now.ToShortTimeString()}";

            return RedirectToAction("GetStudents");
        }

        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> EditStudent(int id)
        {

            var student = await _studentRepository.GetOneRelated(id);

            var classes = await _classRepository.GetAll();

            var subjects = await _subjectRepository.GetAll();

            ViewData["Classes"] = new SelectList(classes,
                "Id",
                "Name");

            ViewData["Subjects"] = new SelectList(subjects,
                "Id",
                "Name");

            var model = new EditStudentViewModel(student);

            return View(model);
        }

        [HttpPost("[action]/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditStudent(int id,
            [Bind("Id, FirstName, LastName, Image, Age, Gender, ClassId, SubjectIds")]
            EditStudentViewModel editStudentViewModel)
        {
            var student = await _studentRepository.GetOne(id);

            student.FirstName = editStudentViewModel.FirstName;
            student.LastName = editStudentViewModel.LastName;
            student.Image = editStudentViewModel.Image;
            student.Age = editStudentViewModel.Age;
            student.Gender = editStudentViewModel.Gender;

            await _studentRepository.Update(student);

            student = await _studentRepository.GetOne(id);

            student.ClassId = editStudentViewModel.ClassId;

            student.Class = await _classRepository.GetOne(editStudentViewModel.ClassId);

            await _studentRepository.Update(student);

            try
            {
                student = await _studentRepository.GetOneRelated(id);

                student.Subjects.Clear();

                await _studentRepository.Update(student);

                student = await _studentRepository.GetOneRelated(id);

                foreach (var t in editStudentViewModel.SubjectIds)
                    student.Subjects
                        .Add(await _subjectRepository
                            .GetOne(t));

                await _studentRepository.Update(student);
            }
            catch (Exception)
            {
                return RedirectToAction("GetStudents");
            }

            TempData["Message"] =
                $"Student: {student.FirstName + " " + student.LastName} was edited at {DateTime.Now.ToShortTimeString()}";

            return RedirectToAction("GetStudents");
        }

        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> DeleteStudent(int id)
        {
            await _studentRepository.Delete(id);

            TempData["Message"] = $"Student: with {id} was deleted at {DateTime.Now.ToShortTimeString()}";

            return RedirectToAction("GetStudents");
        }

        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> DetailsStudent(int id)
        {
            var student = await _studentRepository.GetOneRelated(id);

            var @class = student.ClassId != null
                ? _classRepository.GetOneRelated(student.ClassId)
                    .Result
                    .Name
                : "no class";

            var model = new DetailsStudentViewModel(student,
                @class);

            return View(model);
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> CreateClass()
        {
            var teachers = _teacherRepository
                .GetAll()
                .Result
                .Except(
                    _teacherRepository
                        .GetAll()
                        .Result
                        .Where(t =>
                            _classRepository
                                .GetRelatedData()
                                .ToList()
                                .Exists(c =>
                                    c.TeacherId == t.Id)));


            ViewData["Students"] = new SelectList(await _studentRepository.GetAll(),
                "Id",
                "FullName");

            ViewData["Teachers"] = new SelectList(
                teachers,
                "Id",
                "FullName");

            return View();
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> CreateClass(
            [Bind("Name, StudentIds, TeacherId")]
            CreateClassViewModel createClassViewModel)
        {
            if (!ModelState.IsValid) return View(createClassViewModel);

            await _classRepository.Add(new Class
            {
                Name = createClassViewModel.Name,
                TeacherId = createClassViewModel.TeacherId,
                Students = _studentRepository
                    .GetAll()
                    .Result
                    .Where(i =>
                        createClassViewModel.StudentIds
                            .ToList()
                            .Exists(t =>
                                t == i.Id))
                    .ToList()
            });

            TempData["Message"] =
                $"Class: {createClassViewModel.Name} was created at {DateTime.Now.ToShortTimeString()}";

            return RedirectToAction("GetClasses");

        }

        [HttpGet("[action]")]
        public async Task<IActionResult> CreateSubject()
        {
            ViewData["Students"] = new SelectList(await _studentRepository.GetAll(),
                "Id",
                "FullName");

            ViewData["Teachers"] = new SelectList(await _teacherRepository.GetAll(),
                "Id",
                "FullName");

            return View();
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> CreateSubject(
            [Bind("Id, Name, StudentIds, TeacherIds")]
            CreateSubjectViewModel createSubjectViewModel)
        {
            if (!ModelState.IsValid) 
            {
                ViewData["Students"] = new SelectList(await _studentRepository.GetAll(),
                    "Id",
                    "FullName");

                ViewData["Teachers"] = new SelectList(await _teacherRepository.GetAll(),
                    "Id",
                    "FullName");
                return View(createSubjectViewModel);
            }

            await _subjectRepository.Add(new Subject
            {
                Name = createSubjectViewModel.Name,
                Students = createSubjectViewModel.StudentIds != null
                    ? _studentRepository
                        .GetAll()
                        .Result
                        .Where(i =>
                            createSubjectViewModel.StudentIds
                                .ToList()
                                .Exists(t =>
                                    t == i.Id))
                        .ToList()
                    : null,
                Teachers = createSubjectViewModel.TeacherIds != null
                    ? _teacherRepository
                        .GetAll()
                        .Result
                        .Where(i =>
                            createSubjectViewModel.TeacherIds
                                .ToList()
                                .Exists(t =>
                                    t == i.Id))
                        .ToList()
                    : null
            });

            TempData["Message"] =
                $"Subject: {createSubjectViewModel.Name} was created at {DateTime.Now.ToShortTimeString()}";

            return RedirectToAction("GetSubjects");
        }

        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> EditClass(int id)
        {
            var @class = await _classRepository.GetOneRelated(id);

            var students = await _studentRepository.GetAll();

            var teachers = _teacherRepository
                .GetAll()
                .Result
                .Except(
                    _teacherRepository
                        .GetAll()
                        .Result
                        .Where(t =>
                            _classRepository
                                .GetRelatedData()
                                .ToList()
                                .Exists(c =>
                                    c.TeacherId == t.Id)));

            teachers = teachers.Append(await _teacherRepository.GetOne(@class.TeacherId));

            ViewData["Students"] = new SelectList(students,
                "Id",
                "FullName");

            ViewData["Teachers"] = new SelectList(teachers,
                "Id",
                "FullName");

            var model = new EditClassViewModel(@class);

            return View(model);
        }

        [HttpPost("[action]/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditClass(int id,
            [Bind("Id, Name, StudentIds, TeacherId")]
            EditClassViewModel editClassViewModel)
        {
            if (id != editClassViewModel.Id)
                return NotFound();

            var form = await _classRepository.GetOne(id);

            form.Name = editClassViewModel.Name;

            form.TeacherId = editClassViewModel.TeacherId;

            form.Teacher = await _teacherRepository.GetOne(editClassViewModel.TeacherId);

            await _classRepository.Update(form);

            form = await _classRepository.GetOneRelated(id);

            form.Students.Clear();

            await _classRepository.Update(form);

            foreach (var i in editClassViewModel.StudentIds)
            {
                var student = await _studentRepository.GetOne(i);

                student.ClassId = id;

                student.Class = await _classRepository.GetOne(id);

                await _studentRepository.Update(student);
            }


            TempData["Message"] = $"Class: {editClassViewModel.Name} was edited at {DateTime.Now.ToShortTimeString()}";

            return RedirectToAction("GetClasses");
        }

        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> DeleteClass(int id)
        {
            await _classRepository.Delete(id);

            TempData["Message"] = $"Class: with id: {id} was deleted at {DateTime.Now.ToShortTimeString()}";

            return RedirectToAction("GetClasses");
        }

        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> DetailsClass(int id)
        {
            var @class = await _classRepository.GetOneRelated(id);

            var teacher = @class.TeacherId != null
                ? _teacherRepository.GetOneRelated(@class.TeacherId)
                    .Result
                    ?
                    .FullName
                : "no teacher";

            var model = new DetailsClassViewModel(@class,
                teacher);

            return View(model);
        }

        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> EditSubject(int id)
        {
            var subject = await _subjectRepository.GetOneRelated(id);

            var model = new EditSubjectViewModel(subject);

            return View(model);
        }

        [HttpPost("[action]/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditSubject(int? id,
            [Bind("Id, Name")] EditSubjectViewModel editSubjectViewModel)
        {
            var subject = await _subjectRepository.GetOne(id);

            subject.Name = editSubjectViewModel.Name;

            await _subjectRepository.Update(subject);

            TempData["Message"] = $"Subject: {subject.Name} was edited at {DateTime.Now.ToShortTimeString()}";

            return RedirectToAction("GetSubjects");
        }

        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> DeleteSubject(int id)
        {
            await _subjectRepository.Delete(id);

            TempData["Message"] = $"Subject with id: {id} was deleted at {DateTime.Now.ToShortTimeString()}";

            return RedirectToAction("GetSubjects");
        }

        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> DetailsSubject(int id)
        {
            var subject = await _subjectRepository.GetOneRelated(id);

            var students = _subjectRepository.GetOneRelated(id)
                .Result
                .Students
                .Select(s => s.FullName);

            var teachers = _subjectRepository
                .GetOneRelated(id)
                .Result
                .Teachers
                .Select(s => s.FullName);

            var model = new DetailsSubjectViewModel(subject,
                students,
                teachers);

            return View(model);
        }
    }
}