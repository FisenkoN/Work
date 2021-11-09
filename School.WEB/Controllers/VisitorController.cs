using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using School.WEB.Data.Repository;
using School.WEB.ViewModels.Visitor.ClassDetails;
using School.WEB.ViewModels.Visitor.GetClasses;
using School.WEB.ViewModels.Visitor.GetSubjects;
using School.WEB.ViewModels.Visitor.GetTeachers;
using School.WEB.ViewModels.Visitor.SubjectDetails;
using School.WEB.ViewModels.Visitor.TeacherDetails;

namespace School.WEB.Controllers
{
    [Route("[controller]")]
    public class VisitorController : Controller
    {
        private readonly ITeacherRepository _teacherRepository;
        private readonly ISubjectRepository _subjectRepository;
        private readonly IClassRepository _classRepository;

        public VisitorController(
            ITeacherRepository teacherRepository,
            IClassRepository classRepository,
            ISubjectRepository subjectRepository)
        {
            _teacherRepository = teacherRepository;
            _subjectRepository = subjectRepository;
            _classRepository = classRepository;
        }

        [HttpGet("[action]")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetClasses()
        {
            var classes = await _classRepository.GetAll();

            var model = new GetClassesViewModel(classes);

            return View(model);
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetTeachers()
        {
            var teachers = await _teacherRepository.GetAll();

            var model = new GetTeachersViewModel(teachers);

            return View(model);
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetSubjects()
        {
            var subjects = await _subjectRepository.GetAll();

            var model = new GetSubjectsViewModel(subjects);

            return View(model);
        }

        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> ClassDetails(int id)
        {
            var @class = await _classRepository.GetOneRelated(id);

            if (@class == null)
            {
                return NotFound();
            }

            var model = new ClassDetailsViewModel(@class);

            return View(model);
        }

        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> TeacherDetails(int id)
        {
            var teacher = await _teacherRepository.GetOneRelated(id);

            if (teacher == null)
            {
                return NotFound();
            }

            var className = _classRepository.GetRelatedData()
                                .FirstOrDefault(p => p.TeacherId == teacher.Id)?.Name
                            ?? "no class";

            var model = new TeacherDetailsViewModel(teacher,
                className);

            return View(model);
        }

        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> SubjectDetails(int id)
        {
            var subject = await _subjectRepository.GetOneRelated(id);

            if (subject == null)
            {
                return NotFound();
            }

            var model = new SubjectDetailsModelView(subject);

            return View(model);
        }
    }
}