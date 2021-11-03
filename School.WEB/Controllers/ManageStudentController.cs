using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using School.WEB.Data.Repository;
using School.WEB.Models;
using School.WEB.ViewModels.ManageStudent.CreateStudent;
using School.WEB.ViewModels.ManageStudent.DetailsStudent;
using School.WEB.ViewModels.ManageStudent.EditStudent;
using School.WEB.ViewModels.ManageStudent.GetStudents;

namespace School.WEB.Controllers
{
    [Authorize]
    [Route("[controller]")]
    public class ManageStudentController:Controller
    {
        private readonly IStudentRepository _studentRepository;
        private readonly IClassRepository _classRepository;
        private readonly ISubjectRepository _subjectRepository;
        
        public ManageStudentController(IStudentRepository studentRepository, IClassRepository classRepository, ISubjectRepository subjectRepository)
        {
            _studentRepository = studentRepository;
            _classRepository = classRepository;
            _subjectRepository = subjectRepository;
        }
        
        [HttpGet("[action]")]
        public async Task<IActionResult> GetStudents()
        {
            if (TempData["Message"] != null)
                ViewBag.Message = TempData["Message"]
                    .ToString();

            var students = await _studentRepository.GetAll();

            var model = new GetStudentsViewModel(students);

            return View(model);
        }
        
        [HttpGet("[action]")]
        public async Task<IActionResult> CreateStudent()
        {
            ViewData["Classes"] = new SelectList(await _classRepository.GetAll(),
                "Id",
                "Name");

            ViewData["Subjects"] = new SelectList(await _subjectRepository.GetAll(),
                "Id",
                "Name");

            return View();
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> CreateStudent(
            [Bind("FirstName, LastName, Image, ClassId, SubjectIds, Gender, Id, Age")]
            CreateStudentViewModel createStudentViewModel)
        {
            if (!ModelState.IsValid)
            {
                ViewData["Classes"] = new SelectList(await _classRepository.GetAll(),
                    "Id",
                    "Name");

                ViewData["Subjects"] = new SelectList(await _subjectRepository.GetAll(),
                    "Id",
                    "Name");
                
                return View(createStudentViewModel);
            }
            
            await _studentRepository.Add(new Student
            {
                Age = createStudentViewModel.Age,
                ClassId = createStudentViewModel.ClassId,
                LastName = createStudentViewModel.LastName,
                Gender = createStudentViewModel.Gender,
                FirstName = createStudentViewModel.FirstName,
                Image = createStudentViewModel.Image,
                Subjects = _subjectRepository
                    .GetAll()
                    .Result
                    .Where(i =>
                        createStudentViewModel.SubjectIds
                            .ToList()
                            .Exists(t =>
                                t == i.Id))
                    .ToList()
            });

            await _studentRepository.SaveChanges();

            TempData["Message"] =
                $"Student: {createStudentViewModel.FirstName + " " + createStudentViewModel.LastName} was created at {DateTime.Now.ToShortTimeString()}";

            return RedirectToAction("GetStudents");
        }

        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> EditStudent(int id)
        {

            var student = await _studentRepository.GetOneRelated(id);

            var classes = await _classRepository.GetAll();

            var subjects = await _subjectRepository.GetAll();

            ViewData["Classes"] = new SelectList(classes,
                "Id",
                "Name");

            ViewData["Subjects"] = new SelectList(subjects,
                "Id",
                "Name");

            var model = new EditStudentViewModel(student);

            return View(model);
        }

        [HttpPost("[action]/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditStudent(int id,
            [Bind("Id, FirstName, LastName, Image, Age, Gender, ClassId, SubjectIds")]
            EditStudentViewModel editStudentViewModel)
        {
            if (!ModelState.IsValid)
            {
                var classes = await _classRepository.GetAll();

                var subjects = await _subjectRepository.GetAll();

                ViewData["Classes"] = new SelectList(classes,
                    "Id",
                    "Name");

                ViewData["Subjects"] = new SelectList(subjects,
                    "Id",
                    "Name");
                
                return View(editStudentViewModel);
            }
            
            var student = await _studentRepository.GetOne(id);

            student.FirstName = editStudentViewModel.FirstName;
            student.LastName = editStudentViewModel.LastName;
            student.Image = editStudentViewModel.Image;
            student.Age = editStudentViewModel.Age;
            student.Gender = editStudentViewModel.Gender;

            _studentRepository.Update(student);
            
            await _studentRepository.SaveChanges();

            student = await _studentRepository.GetOne(id);

            student.ClassId = editStudentViewModel.ClassId;

            student.Class = await _classRepository.GetOne(editStudentViewModel.ClassId);
            
            _studentRepository.Update(student);
            
            await _studentRepository.SaveChanges();

            try
            {
                student = await _studentRepository.GetOneRelated(id);

                student.Subjects.Clear();

                _studentRepository.Update(student);
                
                await _studentRepository.SaveChanges();

                student = await _studentRepository.GetOneRelated(id);

                foreach (var t in editStudentViewModel.SubjectIds)
                    student.Subjects
                        .Add(await _subjectRepository
                            .GetOne(t));

                _studentRepository.Update(student);
                
                await _studentRepository.SaveChanges();
            }
            catch (Exception)
            {
                return RedirectToAction("GetStudents");
            }

            TempData["Message"] =
                $"Student: {student.FirstName + " " + student.LastName} was edited at {DateTime.Now.ToShortTimeString()}";

            return RedirectToAction("GetStudents");
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

            TempData["Message"] = $"Student: with {id} was deleted at {DateTime.Now.ToShortTimeString()}";

            return RedirectToAction("GetStudents");
        }

        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> DetailsStudent(int id)
        {
            var student = await _studentRepository.GetOneRelated(id);

            var @class = student.ClassId != null
                ? _classRepository.GetOneRelated(student.ClassId)
                    .Result
                    .Name
                : "no class";

            var model = new DetailsStudentViewModel(student,
                @class);

            return View(model);
        }
    }
}