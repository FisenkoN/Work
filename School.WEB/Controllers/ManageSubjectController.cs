using System;
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
        private readonly ISubjectRepository _subjectRepository;
        private readonly ITeacherRepository _teacherRepository;

        public ManageSubjectController(ISubjectRepository subjectRepository,
            ITeacherRepository teacherRepository)
        {
            _subjectRepository = subjectRepository;
            _teacherRepository = teacherRepository;
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetSubjects()
        {
            var subjects = await _subjectRepository.GetAll();

            var model = new GetSubjectsViewModel(subjects);

            if (TempData["Result"] != null)
            {
                model.OperationResult = TempData.Get<OperationResult>("Result");
            }

            return View(model);
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> CreateSubject()
        {
            var model = new CreateSubjectViewModel();

            var teachers = await _teacherRepository.GetAll();

            ViewData["Teachers"] = new SelectList(teachers,
                "Id",
                "FullName");

            return View(model);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> CreateSubject(CreateSubjectViewModel model)
        {
            if (ModelState.IsValid)
            {
                await _subjectRepository.Add(new Subject().To(model,
                    _teacherRepository));

                await _subjectRepository.SaveChanges();

                TempData.Put("Result",
                    new OperationResult(
                        true,
                        $"Subject: {model.Name} was created at {DateTime.Now.ToShortTimeString()}"));

                return RedirectToAction("GetSubjects", "ManageSubject");
            }

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

                TempData.Put("Result",
                    new OperationResult(
                        true,
                        $"Subject: {subject.Name} was edited at {DateTime.Now.ToShortTimeString()}"));
                
                return RedirectToAction("GetSubjects", "ManageSubject");
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

            subject = await _subjectRepository.GetOne(id);

            if (subject == null)
            {
                TempData.Put("Result",
                    new OperationResult(
                        true,
                        $"Subject with id: {id} was deleted at {DateTime.Now.ToShortTimeString()}"));
            }
            else
            {
                TempData.Put("Result",
                    new OperationResult(
                        false,
                        $"Subject with id: {id} wasn't deleted"));
            }

            return RedirectToAction("GetSubjects", "ManageSubject");
        }

        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> DetailsSubject(int id)
        {
            var subject = await _subjectRepository.GetOneRelated(id);

            if (subject == null)
            {
                return NotFound();
            }

            var model = new DetailsSubjectViewModel(subject);

            return View(model);
        }
    }
}