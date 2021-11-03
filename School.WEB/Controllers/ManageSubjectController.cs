using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using School.WEB.Data.Repository;
using School.WEB.Models;
using School.WEB.ViewModels.ManageSubject.CreateSubject;
using School.WEB.ViewModels.ManageSubject.DetailsSubject;
using School.WEB.ViewModels.ManageSubject.EditSubject;
using School.WEB.ViewModels.ManageSubject.GetSubjects;

namespace School.WEB.Controllers
{
    [Authorize]
    [Route("[controller]")]
    public class ManageSubjectController:Controller

    {
    private readonly IStudentRepository _studentRepository;
    private readonly ISubjectRepository _subjectRepository;
    private readonly ITeacherRepository _teacherRepository;

    public ManageSubjectController(IStudentRepository studentRepository,
        ISubjectRepository subjectRepository, ITeacherRepository teacherRepository)
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

        await _subjectRepository.SaveChanges();

        TempData["Message"] =
            $"Subject: {createSubjectViewModel.Name} was created at {DateTime.Now.ToShortTimeString()}";

        return RedirectToAction("GetSubjects");
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

        _subjectRepository.Update(subject);

        await _subjectRepository.SaveChanges();

        TempData["Message"] = $"Subject: {subject.Name} was edited at {DateTime.Now.ToShortTimeString()}";

        return RedirectToAction("GetSubjects");
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