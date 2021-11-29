using System.Collections.Generic;
using School.WEB.Models;

namespace School.WEB.ViewModels.Account
{
    public class TeacherProfileModel
    {
        public int Id { get; set; }
        
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Image { get; set; }

        public int Age { get; set; }

        public string Email { get; set; }
        
        public string Password { get; set; }

        public string RoleName { get; set; }

        public Gender Gender { get; set; }
        
        public string ClassName { get; set; }
        
        public IEnumerable<string> SubjectNames { get; set; }
        
        public TeacherProfileModel(Teacher teacher, string className, IEnumerable<string> subjectNames, User user, Role role)
        {
            Id = teacher.Id;
            ClassName = className;
            FirstName = teacher.FirstName;
            LastName = teacher.LastName;
            Age = teacher.Age;
            SubjectNames = subjectNames;
            Gender = teacher.Gender;
            Image = teacher.Image;
            Email = user.Email;
            Password = user.Password;
            RoleName = role.Name;
        }
    }
}