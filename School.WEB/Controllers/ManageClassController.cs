using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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

        public ManageClassController(IStudentRepository studentRepository,
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
            if (TempData["Message"] != null)
                ViewBag.Message = TempData["Message"]
                    .ToString();

            var classes = await _classRepository.GetAll();

            var model = new GetClassesViewModel(classes);

            return View(model);
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> CreateClass()
        {
            var model = new EditCreateClassViewModel();

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

            return View(model);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> CreateClass(EditCreateClassViewModel model)
        {
            if (ModelState.IsValid)
            {
                await _classRepository.Add(
                    new Class()
                        .To(model, _studentRepository));

                await _classRepository.SaveChanges();

                TempData["Message"] =
                    $"Class: {model.Name} was created at {DateTime.Now.ToShortTimeString()}";

                return RedirectToAction("GetClasses");
            }

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

                TempData["Message"] = $"Class: {model.Name} was edited at {DateTime.Now.ToShortTimeString()}";

                return RedirectToAction("GetClasses");
            }

            var @class = await _classRepository.GetOne(model.Id);

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

            TempData["Message"] = $"Class: with id: {id} was deleted at {DateTime.Now.ToShortTimeString()}";

            return RedirectToAction("GetClasses");
        }

        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> DetailsClass(int id)
        {
            var @class = await _classRepository.GetOneRelated(id);

            if (@class == null)
            {
                return NotFound();
            }

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
    }
}