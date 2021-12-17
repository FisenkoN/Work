using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using School.WEB.Data.Repository;

namespace School.WEB.Controllers
{
    public class ActiveController : Controller
    {
        private readonly IStudentRepository _studentRepository;
        private readonly ITeacherRepository _teacherRepository;
        
        public ActiveController(
            IStudentRepository studentRepository,
            ITeacherRepository teacherRepository)
        {
            _studentRepository = studentRepository;
            _teacherRepository = teacherRepository;
        }
        
        [HttpPost]
        public async Task<IActionResult> Active(string controllerName, int id, bool state)
        {
            switch (controllerName)
            {
                case nameof(ManageStudentController):
                {
                    var student = await _studentRepository.GetOne(id);

                    student.Active = state;
                
                    _studentRepository.Update(student);

                    await _studentRepository.SaveChanges();

                    RedirectToAction("GetStudents", "ManageStudent");
                    break;
                }
                case nameof(ManageTeacherController):
                {
                    var teacher = await _teacherRepository.GetOne(id);

                    teacher.Active = state;
                
                    _teacherRepository.Update(teacher);

                    await _teacherRepository.SaveChanges();
                
                    RedirectToAction("GetTeachers", "ManageTeacher");
                    break;
                }
            }

            return RedirectToAction("Index", "Home");
        }
    }
}