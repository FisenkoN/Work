using School.WEB.Models;

namespace School.WEB.ViewModels.Student.ShowClassTeacher
{
    public class ShowClassTeacherViewModel
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }
        
        public bool Active { get; set; }

        public string Image { get; set; }

        public int Age { get; set; }

        public Gender Gender { get; set; }

        public ShowClassTeacherViewModel(Teacher teacher)
        {
            Id = teacher.Id;
            Age = teacher.Age;
            Active = teacher.Active;
            FirstName = teacher.FirstName;
            LastName = teacher.LastName;
            Gender = teacher.Gender;
            Image = teacher.Image;
        }
    }
}