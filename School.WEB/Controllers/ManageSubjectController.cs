using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using School.WEB.Data.Repository;
using School.WEB.Extensions;
using School.WEB.Models;
using School.WEB.ViewModels.ManageSubject.DetailsSubject;
using School.WEB.ViewModels.ManageSubject.EditCreateSubject;
using School.WEB.ViewModels.ManageSubject.GetSubjects;

namespace School.WEB.Controllers
{
    [Authorize]
    [Route("[controller]")]
    public class ManageSubjectController : Controller

    {
        private readonly IStudentRepository _studentRepository;
        private readonly ISubjectRepository _subjectRepository;
        private readonly ITeacherRepository _teacherRepository;

        public ManageSubjectController(IStudentRepository studentRepository,
            ISubjectRepository subjectRepository,
            ITeacherRepository teacherRepository)
        {
            _studentRepository = studentRepository;
            _subjectRepository = subjectRepository;
            _teacherRepository = teacherRepository;
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
        public async Task<IActionResult> CreateSubject()
        {
            var model = new CreateSubjectViewModel();

            ViewData["Students"] = new SelectList(await _studentRepository.GetAll(),
                "Id",
                "FullName");

            ViewData["Teachers"] = new SelectList(await _teacherRepository.GetAll(),
                "Id",
                "FullName");

            return View(model);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> CreateSubject(
            CreateSubjectViewModel model)
        {
            if (ModelState.IsValid)
            {
                await _subjectRepository.Add(new Subject().To(model,
                    _studentRepository,
                    _teacherRepository));

                await _subjectRepository.SaveChanges();

                TempData["Message"] =
                    $"Subject: {model.Name} was created at {DateTime.Now.ToShortTimeString()}";

                return RedirectToAction("GetSubjects");
            }

            ViewData["Students"] = new SelectList(await _studentRepository.GetAll(),
                "Id",
                "FullName");

            ViewData["Teachers"] = new SelectList(await _teacherRepository.GetAll(),
                "Id",
                "FullName");
            return View(model);
        }

        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> EditSubject(int id)
        {
            var subject = await _subjectRepository.GetOneRelated(id);

            if (subject == null)
            {
                return NotFound();
            }

            var model = new EditSubjectViewModel(subject);

            return View(model);
        }

        [HttpPost("[action]/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditSubject(EditSubjectViewModel model)
        {
            if (ModelState.IsValid)
            {
                var subject = await _subjectRepository.GetOne(model.Id);

                subject.Name = model.Name;

                _subjectRepository.Update(subject);

                await _subjectRepository.SaveChanges();

                TempData["Message"] = $"Subject: {subject.Name} was edited at {DateTime.Now.ToShortTimeString()}";

                return RedirectToAction("GetSubjects");
            }

            return View(model);
        }

        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> DeleteSubject(int id)
        {
            var subject = await _subjectRepository.GetOne(id);

            if (subject == null)
            {
                return NotFound();
            }

            _subjectRepository.Delete(subject);

            await _subjectRepository.SaveChanges();

            TempData["Message"] = $"Subject with id: {id} was deleted at {DateTime.Now.ToShortTimeString()}";

            return RedirectToAction("GetSubjects");
        }

        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> DetailsSubject(int id)
        {
            var subject = await _subjectRepository.GetOneRelated(id);

            if (subject == null)
            {
                return NotFound();
            }

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