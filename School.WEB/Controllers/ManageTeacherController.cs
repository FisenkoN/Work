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
    [Authorize]
    [Route("[controller]")]
    public class ManageTeacherController : Controller
    {
        private readonly IClassRepository _classRepository;
        private readonly ISubjectRepository _subjectRepository;
        private readonly ITeacherRepository _teacherRepository;
        private readonly IUserRepository _userRepository;
        private readonly IRoleRepository _roleRepository;

        public ManageTeacherController(
            IClassRepository classRepository,
            ISubjectRepository subjectRepository,
            ITeacherRepository teacherRepository,
            IUserRepository userRepository,
            IRoleRepository roleRepository)
        {
            _classRepository = classRepository;
            _subjectRepository = subjectRepository;
            _teacherRepository = teacherRepository;
            _userRepository = userRepository;
            _roleRepository = roleRepository;
        }

        [Authorize(Roles = "admin")]
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

        [Authorize(Roles = "admin, teacher")]
        [HttpGet("[action]")]
        public async Task<IActionResult> CreateTeacher(string email = "no")
        {
            var model = new EditCreateTeacherViewModel();

            var subjects = await _subjectRepository.GetAll();

            var classes = await _classRepository.GetAll();

            var classesWithoutClassTeacher = _classRepository.GetRelatedData()
                .Where(c =>
                    c.TeacherId == null && c.Teacher == null);
            
            if (email != "no")
            {
                var user = await _userRepository.GetForEmail(email);

                model.User = user;

                model.UserId = user.Id;
            }

            ViewData["ClassesWithoutClassTeacher"] = new SelectList(
                classesWithoutClassTeacher,
                "Id",
                "Name");

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

        [Authorize(Roles = "admin, teacher")]
        [HttpPost("[action]")]
        public async Task<IActionResult> CreateTeacher(EditCreateTeacherViewModel model)
        {
            if (ModelState.IsValid)
            {
                await _teacherRepository
                    .Add(new Teacher()
                        .To(model,
                            _classRepository));

                await _teacherRepository.SaveChanges();
                
                var u = await _userRepository.GetOne(model.UserId);

                var t = await _teacherRepository.GetOneRelated(
                    _teacherRepository.GetAll()
                        .Result
                        .Count);

                u.TeacherId = t.Id;

                u.Teacher = t;

                await _userRepository.SaveChanges();

                TempData.Put("Result",
                    new OperationResult(
                        true,
                        $"Teacher: {model.FirstName + " " + model.LastName} was created at {DateTime.Now.ToShortTimeString()}"));

                return _roleRepository.GetForEmail(User.Identity.Name)
                    .Result.Name == "admin"
                    ? RedirectToAction("GetTeachers", 
                        "ManageTeacher")
                    : RedirectToAction("Index",
                        "Home");
            }

            return View(model);
        }

        [Authorize(Roles = "admin, teacher")]
        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> EditTeacher(int id)
        {
            var teacher = await _teacherRepository.GetOneRelated(id);

            if (teacher == null)
            {
                return NotFound();
            }

            var classesWithoutClassTeacher = await _classRepository.GetRelatedData()
                .Where(c =>
                    c.TeacherId == null && c.Teacher == null)
                .ToListAsync();

            try
            {
                var c = _classRepository.GetRelatedData()
                    .FirstOrDefault(c =>
                        c.TeacherId == id);

                if (c != null)
                    classesWithoutClassTeacher.Add(c);
            }
            catch (Exception)
            {
                Console.WriteLine();
            }

            var subjects = await _subjectRepository.GetAll();

            var classes = await _classRepository.GetAll();

            try
            {
                var selectedClass = _classRepository.GetRelatedData()
                    .FirstOrDefault(c =>
                        c.TeacherId == id);

                ViewData["ClassesWithoutClassTeacher"] = new SelectList(
                    classesWithoutClassTeacher,
                    "Id",
                    "Name",
                    selectedClass);
            }
            catch (Exception)
            {
                ViewData["ClassesWithoutClassTeacher"] = new SelectList(
                    classesWithoutClassTeacher,
                    "Id",
                    "Name");
            }

            ViewData["Classes"] = new SelectList(
                classes,
                "Id",
                "Name");

            ViewData["Subjects"] = new SelectList(
                subjects,
                "Id",
                "Name");

            var model = new EditCreateTeacherViewModel(teacher);

            return View(model);
        }

        [Authorize(Roles = "admin, teacher")]
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

                teacher = await _teacherRepository.GetOne(model.Id);

                teacher.SubjectId = model.SubjectId;

                teacher.Subject = await _subjectRepository.GetOne(model.SubjectId);

                _teacherRepository.Update(teacher);

                await _teacherRepository.SaveChanges();

                teacher = await _teacherRepository.GetOneRelated(model.Id);

                teacher.Classes.Clear();

                _teacherRepository.Update(teacher);

                await _teacherRepository.SaveChanges();

                teacher = await _teacherRepository.GetOneRelated(model.Id);

                foreach (var t in model.ClassIds)
                    teacher.Classes
                        .Add(await _classRepository
                            .GetOne(t));

                _teacherRepository.Update(teacher);

                await _teacherRepository.SaveChanges();

                TempData.Put("Result",
                    new OperationResult(
                        true,
                        $"Teacher: {_teacherRepository.GetOne(model.Id).Result.FullName} was edited at {DateTime.Now.ToShortTimeString()}"));

                return _roleRepository.GetForEmail(User.Identity.Name)
                    .Result.Name == "admin"
                    ? RedirectToAction("GetTeachers", 
                        "ManageTeacher")
                    : RedirectToAction("Index",
                        "Home");
            }

            return View(model);
        }

        [Authorize(Roles = "admin")]
        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> DeleteTeacher(int id)
        {
            var teacher = await _teacherRepository.GetOne(id);

            if (teacher == null)
            {
                return NotFound();
            }

            _teacherRepository.Delete(teacher);

            _userRepository.Delete(
                _userRepository.GetOne(
                    _teacherRepository.GetOneRelated(id)
                        .Result
                        .UserId)
                    .Result);

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

        [Authorize(Roles = "admin")]
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
                    c.TeacherId == teacher.Id);

            ClassModel classModel = null;

            if (@class != null)
            {
                classModel = new ClassModel
                {
                    Id = @class.Id,
                    Name = @class.Name
                };
            }

            SubjectModel subjectModel = null;

            var subject = await _subjectRepository.GetOne(teacher.SubjectId);

            if (subject != null)
            {
                subjectModel = new SubjectModel()
                {
                    Id = subject.Id,
                    Name = subject.Name
                };
            }

            var model = new DetailsTeacherViewModel(teacher,
                classModel,
                subjectModel);

            return View(model);
        }
    }
}