using School.WEB.Models;

namespace School.WEB.ViewModels.ManageStudent.GetStudents
{
    public class StudentModel
    {
        public int Id { get; set; }

        public string FirstName { get; set; }
        
        public string LastName { get; set; }
        
        public bool Active { get; set; }
        
        public int Age { get; set; }
        
        public Gender Gender { get; set; }
        
        public string Image { get; set; }
    }
}