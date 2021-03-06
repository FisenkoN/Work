using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using School.WEB.Data.Repository;
using School.WEB.Extensions;
using School.WEB.ViewModels.Student.Edit;
using School.WEB.ViewModels.Student.Index;
using School.WEB.ViewModels.Student.ShowClassmates;
using School.WEB.ViewModels.Student.ShowClassTeacher;
using School.WEB.ViewModels.Student.StudentDetails;

namespace School.WEB.Controllers
{
    [Authorize]
    [Route("[controller]")]
    public class StudentController : Controller
    {
        private readonly IStudentRepository _studentRepository;
        private readonly IClassRepository _classRepository;
        private readonly ISubjectRepository _subjectRepository;
        private readonly ITeacherRepository _teacherRepository;


        public StudentController(
            IStudentRepository studentRepository,
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
        public async Task<IActionResult> Index()
        {
            var students = await _studentRepository.GetAll();

            var model = new IndexViewModel(students);
            
            if (TempData["Result"] != null)
            {
                model.OperationResult = TempData.Get<OperationResult>("Result");
            }

            return View(model);
        }

        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> StudentDetails(int id)
        {
            var student = await _studentRepository.GetOneRelated(id);

            if (student == null)
            {
                return NotFound();
            }

            var @class = await _classRepository.GetOne(student.ClassId);

            var model = new StudentDetailsViewModel(student, @class);

            return View(model);
        }

        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> Edit(int id)
        {

            var student = await _studentRepository.GetOneRelated(id);

            if (student == null)
            {
                return NotFound();
            }

            var classes = await _classRepository.GetAll();

            var subjects = await _subjectRepository.GetAll();

            ViewData["Classes"] = new SelectList(
                classes,
                "Id",
                "Name");

            ViewData["Subjects"] = new SelectList(
                subjects,
                "Id",
                "Name");

            var model = new EditViewModel(student);

            return View(model);
        }

        [HttpPost("[action]/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditViewModel model)
        {
            ModelState["ClassId"].ValidationState = ModelValidationState.Valid;
            
            if (ModelState.IsValid)
            {
                var student = await _studentRepository.GetOne(model.Id);

                student.FirstName = model.FirstName;

                student.LastName = model.LastName;

                student.Age = model.Age;

                student.Active = model.Active;

                student.Image = model.Image;

                student.Gender = model.Gender;

                _studentRepository.Update(student);

                await _studentRepository.SaveChanges();

                student = await _studentRepository.GetOne(model.Id);

                student.ClassId = model.ClassId;

                student.Class = await _classRepository.GetOne(model.ClassId);

                _studentRepository.Update(student);

                await _studentRepository.SaveChanges();

                TempData.Put("Result", 
                    new OperationResult(
                    true,
                    $"Student: {model.FirstName + " " + model.LastName} was edit at {DateTime.Now.ToShortTimeString()}"));

                return RedirectToAction(nameof(Index));
            }

            return View(model);
        }

        [HttpGet("[action]/{id}")]
        public IActionResult ShowClassmates(int id)
        {
            var classId = _studentRepository
                .GetOneRelated(id)
                .Result
                .ClassId;

            if (classId != null)
            {
                var students = _studentRepository
                    .GetSome(s =>
                        s.ClassId == classId)
                    .ToList();

                var model = new ShowClassmatesViewModel(students);

                return View(model);
            }

            TempData.Put("Result", 
                new OperationResult( 
                    false,
                    "This student doesn't have classmates"));

            return RedirectToAction("Index");
        }

        [HttpGet("[action]/{id}")]
        public IActionResult ShowClassTeacher(int id)
        {
            var classId = _studentRepository
                .GetOneRelated(id)
                .Result
                .ClassId;

            if (classId != null)
            {
                var teacher = _teacherRepository
                    .GetSome(t => t.Id ==
                                  _classRepository
                                      .GetOne(classId)
                                      .Result
                                      .TeacherId)
                    ?
                    .FirstOrDefault();

                if (teacher == null)
                    return NotFound();

                var model = new ShowClassTeacherViewModel(teacher);

                return View(model);
            }

            TempData.Put("Result", 
                new OperationResult(
                    false, 
                    "This student doesn't have a class"));

            return RedirectToAction("Index");
        }
    }
}