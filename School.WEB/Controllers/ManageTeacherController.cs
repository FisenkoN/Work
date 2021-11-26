using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using School.WEB.Data.Repository;
using School.WEB.Extensions;
using School.WEB.Models;
using School.WEB.ViewModels.ManageTeacher.DetailsTeacher;
using School.WEB.ViewModels.ManageTeacher.EditCreateTeacher;
using School.WEB.ViewModels.ManageTeacher.GetTeachers;

namespace School.WEB.Controllers
{
    [Authorize(Roles = "admin")]
    [Route("[controller]")]
    public class ManageTeacherController : Controller
    {
        private readonly IClassRepository _classRepository;
        private readonly ISubjectRepository _subjectRepository;
        private readonly ITeacherRepository _teacherRepository;

        public ManageTeacherController(
            IClassRepository classRepository,
            ISubjectRepository subjectRepository,
            ITeacherRepository teacherRepository)
        {
            _classRepository = classRepository;
            _subjectRepository = subjectRepository;
            _teacherRepository = teacherRepository;
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetTeachers()
        {
            var teachers = await _teacherRepository.GetAll();

            var model = new GetTeachersViewModel(teachers);
            
            if (TempData["Result"] != null)
            {
                model.OperationResult = TempData.Get<OperationResult>("Result");
            }

            return View(model);
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> CreateTeacher()
        {
            var model = new EditCreateTeacherViewModel();

            var subjects = await _subjectRepository.GetAll();

            var classes = _classRepository.GetRelatedData()
                .Where(c =>
                    c.TeacherId == null && c.Teacher == null);

            ViewData["Classes"] = new SelectList(
                classes,
                "Id",
                "Name");

            ViewData["Subjects"] = new SelectList(
                subjects,
                "Id",
                "Name");

            return View(model);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> CreateTeacher(EditCreateTeacherViewModel model)
        {
            if (ModelState.IsValid)
            {
                await _teacherRepository
                    .Add(new Teacher()
                        .To(model, _subjectRepository));

                await _teacherRepository.SaveChanges();

                TempData.Put("Result", 
                    new OperationResult(
                        true, 
                        $"Teacher: {model.FirstName + " " + model.LastName} was created at {DateTime.Now.ToShortTimeString()}"));

                return RedirectToAction("GetTeachers", "ManageTeacher");
            }

            return View(model);
        }

        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> EditTeacher(int id)
        {
            var teacher = await _teacherRepository.GetOneRelated(id);

            if (teacher == null)
            {
                return NotFound();
            }

            var classes = await _classRepository.GetRelatedData()
                .Where(c =>
                    c.TeacherId == null && c.Teacher == null)
                .ToListAsync();

            try
            {
                var c = _classRepository.GetRelatedData()
                    .FirstOrDefault(c =>
                        c.TeacherId == id);

                if (c != null)
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

                ViewData["Classes"] = new SelectList(
                    classes,
                    "Id",
                    "Name",
                    selectedClass);
            }
            catch (Exception)
            {
                ViewData["Classes"] = new SelectList(
                    classes,
                    "Id",
                    "Name");
            }

            ViewData["Subjects"] = new SelectList(
                subjects,
                "Id",
                "Name");

            var model = new EditCreateTeacherViewModel(teacher);

            return View(model);
        }

        [HttpPost("[action]/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditTeacher(EditCreateTeacherViewModel model)
        {
            if (ModelState.IsValid)
            {
                var teacher = await _teacherRepository.GetOne(model.Id);

                teacher.FirstName = model.FirstName;
                teacher.LastName = model.LastName;
                teacher.Age = model.Age;
                teacher.Gender = model.Gender;
                teacher.Image = model.Image;

                _teacherRepository.Update(teacher);

                await _teacherRepository.SaveChanges();

                teacher = await _teacherRepository.GetOne(model.Id);

                teacher.ClassId = model.ClassId;

                teacher.Class = await _classRepository.GetOne(model.ClassId);

                _teacherRepository.Update(teacher);

                await _teacherRepository.SaveChanges();

                teacher = await _teacherRepository.GetOneRelated(model.Id);

                teacher.Subjects.Clear();

                _teacherRepository.Update(teacher);

                await _teacherRepository.SaveChanges();

                teacher = await _teacherRepository.GetOneRelated(model.Id);

                foreach (var t in model.SubjectIds)
                    teacher.Subjects
                        .Add(await _subjectRepository
                            .GetOne(t));

                _teacherRepository.Update(teacher);

                await _teacherRepository.SaveChanges();

                TempData.Put("Result", 
                    new OperationResult(
                        true,
                        $"Teacher: {_teacherRepository.GetOne(model.Id).Result.FullName} was edited at {DateTime.Now.ToShortTimeString()}"));

                return RedirectToAction("GetTeachers", "ManageTeacher");
            }

            return View(model);
        }

        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> DeleteTeacher(int id)
        {
            var teacher = await _teacherRepository.GetOne(id);

            if (teacher == null)
            {
                return NotFound();
            }

            _teacherRepository.Delete(teacher);

            await _teacherRepository.SaveChanges();
            
            teacher = await _teacherRepository.GetOne(id);

            if (teacher == null)
            {
                TempData.Put("Result", 
                    new OperationResult(
                        true,
                        $"Teacher: with id: {id} was deleted at {DateTime.Now.ToShortTimeString()}"));
            }
            else
            {
                TempData.Put("Result", 
                    new OperationResult(
                        false,
                        $"Teacher: with id: {id} wasn't deleted"));
            }

            return RedirectToAction("GetTeachers", "ManageTeacher");

        }

        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> DetailsTeacher(int id)
        {
            var teacher = await _teacherRepository.GetOneRelated(id);

            if (teacher == null)
            {
                return NotFound();
            }

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
    }
}