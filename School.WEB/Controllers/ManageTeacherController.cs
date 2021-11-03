using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using School.WEB.Data.Repository;
using School.WEB.Models;
using School.WEB.ViewModels.ManageTeacher.CreateTeacher;
using School.WEB.ViewModels.ManageTeacher.DetailsTeacher;
using School.WEB.ViewModels.ManageTeacher.EditTeacher;
using School.WEB.ViewModels.ManageTeacher.GetTeachers;

namespace School.WEB.Controllers
{
    [Authorize]
    [Route("[controller]")]
    public class ManageTeacherController:Controller
    {
        private readonly IClassRepository _classRepository;
        private readonly ISubjectRepository _subjectRepository;
        private readonly ITeacherRepository _teacherRepository;

        public ManageTeacherController(IClassRepository classRepository, ISubjectRepository subjectRepository, ITeacherRepository teacherRepository)
        {
            _classRepository = classRepository;
            _subjectRepository = subjectRepository;
            _teacherRepository = teacherRepository;
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

            await _teacherRepository.SaveChanges();

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
            if (!ModelState.IsValid)
            {
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
                
                return View(editTeacherViewModel);
            }
            
            var teacher = await _teacherRepository.GetOne(id);

            teacher.FirstName = editTeacherViewModel.FirstName;
            teacher.LastName = editTeacherViewModel.LastName;
            teacher.Age = editTeacherViewModel.Age;
            teacher.Gender = editTeacherViewModel.Gender;
            teacher.Image = editTeacherViewModel.Image;

            _teacherRepository.Update(teacher);

            await _teacherRepository.SaveChanges();

            teacher = await _teacherRepository.GetOne(id);

            teacher.ClassId = editTeacherViewModel.ClassId;

            teacher.Class = await _classRepository.GetOne(editTeacherViewModel.ClassId);

            _teacherRepository.Update(teacher);
            
            await _teacherRepository.SaveChanges();

            teacher = await _teacherRepository.GetOneRelated(id);

            teacher.Subjects.Clear();

            _teacherRepository.Update(teacher);
            
            await _teacherRepository.SaveChanges();

            teacher = await _teacherRepository.GetOneRelated(id);

            foreach (var t in editTeacherViewModel.SubjectIds)
                teacher.Subjects
                    .Add(await _subjectRepository
                        .GetOne(t));

            _teacherRepository.Update(teacher);
            
            await _teacherRepository.SaveChanges();

            TempData["Message"] = $"Teacher: {_teacherRepository.GetOne(id).Result.FullName} was edited at {DateTime.Now.ToShortTimeString()}";

            return RedirectToAction("GetTeachers");
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
    }
}