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
        private readonly ISubjectRepository _subjectRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly IUserRepository _userRepository;

        public ManageStudentController(
            IStudentRepository studentRepository,
            IClassRepository classRepository,
            ISubjectRepository subjectRepository,
            IRoleRepository roleRepository,
            IUserRepository userRepository)
        {
            _studentRepository = studentRepository;
            _classRepository = classRepository;
            _subjectRepository = subjectRepository;
            _roleRepository = roleRepository;
            _userRepository = userRepository;
        }

        [Authorize(Roles = "admin")]
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

        [Authorize(Roles = "admin, student")]
        [HttpGet("[action]")]
        public async Task<IActionResult> CreateStudent(string email = "no")
        {
            var model = new EditCreateStudentViewModel();

            var classes = await _classRepository.GetAll();

            var subjects = await _subjectRepository.GetAll();

            if (email != "no")
            {
                var user = await _userRepository.GetForEmail(email);

                model.User = user;

                model.UserId = user.Id;
            }

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

        [Authorize(Roles = "admin, student")]
        [HttpPost("[action]")]
        public async Task<IActionResult> CreateStudent(EditCreateStudentViewModel model)
        {
            ModelState["ClassId"]
                .ValidationState = ModelValidationState.Valid;

            if (ModelState.IsValid)
            {
                await _studentRepository
                    .Add(new Student()
                        .To(model, _subjectRepository));

                await _studentRepository.SaveChanges();

                var u = await _userRepository.GetOne(model.UserId);

                var s = await _studentRepository.GetOneRelated(
                    _studentRepository.GetAll()
                    .Result
                    .Count);

                u.StudentId = s.Id;

                u.Student = s;

                await _userRepository.SaveChanges();

                TempData
                    .Put("Result",
                        new OperationResult(
                            true,
                            $"Student: {model.FirstName + " " + model.LastName} was created at {DateTime.Now.ToShortTimeString()}"));

                return _roleRepository.GetForEmail(User.Identity.Name)
                    .Result.Name == "admin"
                    ? RedirectToAction("GetStudents",
                        "ManageStudent")
                    : RedirectToAction("Index",
                        "Home");
            }

            return View(model);
        }

        [Authorize(Roles = "admin, student")]
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

            ViewData["Classes"] = new SelectList(
                classes,
                "Id",
                "Name");

            ViewData["Subjects"] = new SelectList(
                subjects,
                "Id",
                "Name");

            var model = new EditCreateStudentViewModel(student);

            return View(model);
        }

        [Authorize(Roles = "admin, student")]
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
                student.Gender = model.Gender;

                _studentRepository.Update(student);

                await _studentRepository.SaveChanges();
                
                student = await _studentRepository.GetOne(model.Id);

                student.UserId = model.UserId;

                student.User = await _userRepository.GetOne(model.UserId);

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
                    return RedirectToAction("GetStudents", "ManageStudent");
                }

                TempData.Put("Result",
                    new OperationResult(
                        true,
                        $"Student: {student.FullName} was edited at {DateTime.Now.ToShortTimeString()}"));

                return _roleRepository.GetForEmail(User.Identity.Name)
                           .Result.Name == "admin"
                    ? RedirectToAction("GetStudents", 
                        "ManageStudent")
                    : RedirectToAction("Index",
                        "Home");
            }

            return View(model);
        }

        [Authorize(Roles = "admin")]
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

        [Authorize(Roles = "admin")]
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

            var model = new DetailsStudentViewModel(student, @class);

            return View(model);
        }
    }
}