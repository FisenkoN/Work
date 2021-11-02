using System.Collections.Generic;
using School.WEB.Models;

namespace School.WEB.ViewModels.Student.StudentDetails
{
    public class StudentModel
    {
        public int Id { get; set; }

        public string Image { get; set; }

        public string FirstName { get; set; }
        
        public string LastName { get; set; }
        
        public int Age { get; set; }
        
        public Gender Gender { get; set; }

        public IEnumerable<Subject> Subjects { get; set; }
    }
}