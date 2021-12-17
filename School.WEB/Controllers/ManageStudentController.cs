using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
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

        public ManageStudentController(
            IStudentRepository studentRepository,
            IClassRepository classRepository)
        {
            _studentRepository = studentRepository;
            _classRepository = classRepository;
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetStudents()
        {
            var students = await _studentRepository.GetAll();

            var model = new GetStudentsViewModel(students);

            if (TempData["Result"] != null)
            {
                model.OperationResult = TempData.Get<OperationResult>("Result");
            }

            return View(model);
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> CreateStudent()
        {
            var model = new EditCreateStudentViewModel();

            var classes = await _classRepository.GetAll();

            ViewData["Classes"] = new SelectList(
                classes,
                "Id",
                "Name");

            return View(model);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> CreateStudent(EditCreateStudentViewModel model)
        {
            ModelState["ClassId"].ValidationState = ModelValidationState.Valid;

            if (ModelState.IsValid)
            {
                await _studentRepository
                    .Add(new Student()
                        .To(model));

                await _studentRepository.SaveChanges();

                TempData
                    .Put("Result",
                        new OperationResult(
                            true,
                            $"Student: {model.FirstName + " " + model.LastName} was created at {DateTime.Now.ToShortTimeString()}"));

                return RedirectToAction("GetStudents", "ManageStudent");
            }

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

            ViewData["Classes"] = new SelectList(
                classes,
                "Id",
                "Name");

            var model = new EditCreateStudentViewModel(student);

            return View(model);
        }

        [HttpPost("[action]/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditStudent(EditCreateStudentViewModel model)
        {
            ModelState["ClassId"].ValidationState = ModelValidationState.Valid;

            if (ModelState.IsValid)
            {
                var student = await _studentRepository.GetOne(model.Id);

                student.FirstName = model.FirstName;
                student.LastName = model.LastName;
                student.Image = model.Image;
                student.Age = model.Age;
                student.Active = model.Active;
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
                        $"Student: {student.FullName} was edited at {DateTime.Now.ToShortTimeString()}"));

                return RedirectToAction("GetStudents", "ManageStudent");
            }

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
                TempData.Put("Result",
                    new OperationResult(
                        true,
                        $"Student: with id: {id} was deleted at {DateTime.Now.ToShortTimeString()}"));
            }
            else
            {
                TempData.Put("Result",
                    new OperationResult(
                        false,
                        $"Student: with id: {id} wasn't deleted"));
            }

            return RedirectToAction("GetStudents", "ManageStudent");
        }

        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> DetailsStudent(int id)
        {
            var student = await _studentRepository.GetOneRelated(id);

            if (student == null)
            {
                return NotFound();
            }

            var model = new DetailsStudentViewModel(student);

            return View(model);
        }
    }
}