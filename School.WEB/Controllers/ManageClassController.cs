using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using School.WEB.Data.Repository;
using School.WEB.Extensions;
using School.WEB.Models;
using School.WEB.ViewModels.ManageClass.DetailsClass;
using School.WEB.ViewModels.ManageClass.EditCreateClass;
using School.WEB.ViewModels.ManageClass.GetClasses;

namespace School.WEB.Controllers
{
    [Authorize]
    [Route("[controller]")]
    public class ManageClassController : Controller
    {
        private readonly IStudentRepository _studentRepository;
        private readonly IClassRepository _classRepository;
        private readonly ITeacherRepository _teacherRepository;

        public ManageClassController(
            IStudentRepository studentRepository,
            IClassRepository classRepository,
            ITeacherRepository teacherRepository)
        {
            _studentRepository = studentRepository;
            _classRepository = classRepository;
            _teacherRepository = teacherRepository;
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetClasses()
        {
            var classes = await _classRepository.GetAll();

            var model = new GetClassesViewModel(classes);

            if (TempData["Result"] != null)
            {
                model.OperationResult = TempData.Get<OperationResult>("Result");
            }

            return View(model);
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> CreateClass()
        {
            var model = new EditCreateClassViewModel();

            var classTeachers = _teacherRepository
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

            var students = await _studentRepository.GetAll();

            var teachers = await _teacherRepository.GetAll();

            ViewData["Students"] = new SelectList(
                students,
                "Id",
                "FullName");

            ViewData["ClassTeachers"] = new SelectList(
                classTeachers,
                "Id",
                "FullName");

            ViewData["Teachers"] = new SelectList(
                teachers,
                "Id",
                "FullName");

            return View(model);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> CreateClass(EditCreateClassViewModel model)
        {
            ModelState["TeacherId"].ValidationState = ModelValidationState.Valid;

            if (ModelState.IsValid)
            {
                await _classRepository.Add(
                    new Class()
                        .To(model,
                            _studentRepository,
                            _teacherRepository));

                await _classRepository.SaveChanges();

                TempData.Put("Result",
                    new OperationResult(
                        true,
                        $"Class: {model.Name} was created at {DateTime.Now.ToShortTimeString()}"));

                return RedirectToAction("GetClasses", "ManageClass");
            }

            return View(model);
        }

        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> EditClass(int id)
        {
            var @class = await _classRepository.GetOneRelated(id);

            if (@class == null)
            {
                return NotFound();
            }

            var students = await _studentRepository.GetAll();

            var teachers = await _teacherRepository.GetAll();

            var classTeachers = _teacherRepository
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

            classTeachers = classTeachers.Append(await _teacherRepository.GetOne(@class.TeacherId));

            ViewData["Students"] = new SelectList(
                students,
                "Id",
                "FullName");

            ViewData["ClassTeachers"] = new SelectList(
                classTeachers,
                "Id",
                "FullName");

            ViewData["Teachers"] = new SelectList(
                teachers,
                "Id",
                "FullName");

            var model = new EditCreateClassViewModel(@class);

            return View(model);
        }

        [HttpPost("[action]/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditClass(EditCreateClassViewModel model)
        {
            if (ModelState.IsValid)
            {
                var form = await _classRepository.GetOne(model.Id);

                form.Name = model.Name;

                form.TeacherId = model.TeacherId;

                form.Teacher = await _teacherRepository.GetOne(model.TeacherId);

                _classRepository.Update(form);

                await _classRepository.SaveChanges();

                form = await _classRepository.GetOneRelated(form.Id);

                form.Students.Clear();

                _classRepository.Update(form);

                await _classRepository.SaveChanges();

                foreach (var i in model.StudentIds)
                {
                    var student = await _studentRepository.GetOne(i);

                    student.ClassId = form.Id;

                    student.Class = await _classRepository.GetOne(form.Id);

                    _studentRepository.Update(student);

                    await _studentRepository.SaveChanges();
                }

                form = await _classRepository.GetOneRelated(form.Id);

                form.Teachers.Clear();

                _classRepository.Update(form);

                await _classRepository.SaveChanges();

                form = await _classRepository.GetOneRelated(form.Id);

                foreach (var id in model.TeacherIds)
                {
                    form.Teachers.Add(await _teacherRepository.GetOne(id));
                }

                _classRepository.Update(form);

                await _classRepository.SaveChanges();

                TempData.Put("Result",
                    new OperationResult(
                        true,
                        $"Class: {model.Name} was edited at {DateTime.Now.ToShortTimeString()}"));

                return RedirectToAction("GetClasses", "ManageClass");
            }

            return View(model);
        }

        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> DeleteClass(int id)
        {
            var @class = await _classRepository.GetOne(id);

            if (@class == null)
            {
                return NotFound();
            }

            _classRepository.Delete(@class);

            await _classRepository.SaveChanges();

            @class = await _classRepository.GetOne(id);

            if (@class == null)
            {
                TempData.Put("Result",
                    new OperationResult(
                        true,
                        $"Class: with id: {id} was deleted at {DateTime.Now.ToShortTimeString()}"));
            }
            else
            {
                TempData.Put("Result",
                    new OperationResult(
                        false,
                        $"Class: with id: {id} wasn't deleted"));
            }

            return RedirectToAction("GetClasses", "ManageClass");
        }

        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> DetailsClass(int id)
        {
            var @class = await _classRepository.GetOneRelated(id);

            if (@class == null)
            {
                return NotFound();
            }

            var model = new DetailsClassViewModel(@class);

            return View(model);
        }
    }
}