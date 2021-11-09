using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using School.WEB.Data.Repository;
using School.WEB.Extensions;
using School.WEB.Models;
using School.WEB.ViewModels.ManageStudent.DetailsStudent;
using School.WEB.ViewModels.ManageStudent.EditCreateStudent;
using School.WEB.ViewModels.ManageStudent.GetStudents;

namespace School.WEB.Controllers
{
    [Authorize]
    [Route("[controller]")]
    public class ManageStudentController : Controller
    {
        private readonly IStudentRepository _studentRepository;
        private readonly IClassRepository _classRepository;
        private readonly ISubjectRepository _subjectRepository;

        public ManageStudentController(
            IStudentRepository studentRepository,
            IClassRepository classRepository,
            ISubjectRepository subjectRepository)
        {
            _studentRepository = studentRepository;
            _classRepository = classRepository;
            _subjectRepository = subjectRepository;
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetStudents()
        {
            if (TempData["Result"] != null)
            {
                ViewBag.Result = TempData.Get<OperationResult<string>>("Result");
            }

            var students = await _studentRepository.GetAll();

            var model = new GetStudentsViewModel(students);

            return View(model);
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> CreateStudent()
        {
            var model = new EditCreateStudentViewModel();

            ViewData["Classes"] = new SelectList(await _classRepository.GetAll(),
                "Id",
                "Name");

            ViewData["Subjects"] = new SelectList(await _subjectRepository.GetAll(),
                "Id",
                "Name");

            return View(model);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> CreateStudent(EditCreateStudentViewModel model)
        {
            if (TempData["Result"] != null)
            {
                ViewBag.Result = TempData.Get<OperationResult<string>>("Result");
            }
            
            if (ModelState.IsValid)
            {
                await _studentRepository.Add(new Student().To(model,
                    _subjectRepository));

                await _studentRepository.SaveChanges();
                
                TempData.Put("Result", OperationResult<string>.CreateSuccessResult(
                    $"Student: {model.FirstName + " " + model.LastName} was created at {DateTime.Now.ToShortTimeString()}"));

                return RedirectToAction("GetStudents");
            }

            ViewData["Classes"] = new SelectList(await _classRepository.GetAll(),
                "Id",
                "Name");

            ViewData["Subjects"] = new SelectList(await _subjectRepository.GetAll(),
                "Id",
                "Name");

            ViewBag.Result =
                OperationResult<string>.CreateFailure("The student was not created because the model is not valid");

            return View(model);
        }

        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> EditStudent(int id)
        {
            var student = await _studentRepository.GetOneRelated(id);

            if (student == null)
            {
                return NotFound();
            }

            var classes = await _classRepository.GetAll();

            var subjects = await _subjectRepository.GetAll();

            ViewData["Classes"] = new SelectList(classes,
                "Id",
                "Name");

            ViewData["Subjects"] = new SelectList(subjects,
                "Id",
                "Name");

            var model = new EditCreateStudentViewModel(student);

            return View(model);
        }

        [HttpPost("[action]/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditStudent(EditCreateStudentViewModel model)
        {
            if (TempData["Result"] != null)
            {
                ViewBag.Result = TempData.Get<OperationResult<string>>("Result");
            }
            
            if (ModelState.IsValid)
            {
                var student = await _studentRepository.GetOne(model.Id);

                student.FirstName = model.FirstName;
                student.LastName = model.LastName;
                student.Image = model.Image;
                student.Age = model.Age;
                student.Gender = model.Gender;

                _studentRepository.Update(student);

                await _studentRepository.SaveChanges();

                student = await _studentRepository.GetOne(model.Id);

                student.ClassId = model.ClassId;

                student.Class = await _classRepository.GetOne(model.ClassId);

                _studentRepository.Update(student);

                await _studentRepository.SaveChanges();

                try
                {
                    student = await _studentRepository.GetOneRelated(model.Id);

                    student.Subjects.Clear();

                    _studentRepository.Update(student);

                    await _studentRepository.SaveChanges();

                    student = await _studentRepository.GetOneRelated(model.Id);

                    foreach (var t in model.SubjectIds)
                        student.Subjects
                            .Add(await _subjectRepository
                                .GetOne(t));

                    _studentRepository.Update(student);

                    await _studentRepository.SaveChanges();
                }
                catch (Exception)
                {
                    return RedirectToAction("GetStudents");
                }

                TempData.Put("Result", OperationResult<string>.CreateSuccessResult(
                    $"Student: {student.FullName} was edited at {DateTime.Now.ToShortTimeString()}"));

                return RedirectToAction("GetStudents");
            }

            var classes = await _classRepository.GetAll();

            var subjects = await _subjectRepository.GetAll();

            ViewData["Classes"] = new SelectList(classes,
                "Id",
                "Name");

            ViewData["Subjects"] = new SelectList(subjects,
                "Id",
                "Name");

            ViewBag.Result = OperationResult<string>.CreateFailure(
                $"Student: {model.FirstName + " " + model.LastName} wasn't edited, because model is not valid");

            return View(model);
        }

        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> DeleteStudent(int id)
        {
            var student = await _studentRepository.GetOne(id);

            if (student == null)
            {
                return NotFound();
            }

            _studentRepository.Delete(student);

            await _studentRepository.SaveChanges();
            
            student = await _studentRepository.GetOne(id);

            if (@student == null)
            {
                TempData.Put("Result", OperationResult<string>.CreateSuccessResult(
                    $"Student: with id: {id} was deleted at {DateTime.Now.ToShortTimeString()}"));
            }
            else
            {
                TempData.Put("Result", OperationResult<string>.CreateFailure(
                    $"Student: with id: {id} wasn't deleted"));
            }
            
            return RedirectToAction("GetStudents");
        }

        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> DetailsStudent(int id)
        {
            var student = await _studentRepository.GetOneRelated(id);

            if (student == null)
            {
                return NotFound();
            }

            var @class = student.ClassId != null
                ? _classRepository.GetOneRelated(student.ClassId)
                    .Result
                    .Name
                : "no class";

            var model = new DetailsStudentViewModel(student,
                @class);

            return View(model);
        }
    }
}