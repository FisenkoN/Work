using School.WEB.Models;

namespace School.WEB.ViewModels.ManageStudent.DetailsStudent
{
    public class DetailsStudentViewModel
    {
        public int Id { get; set; }

        public string FirstName { get; set; }
        
        public string LastName { get; set; }

        public int Age { get; set; }
        
        public bool Active { get; set; }

        public Gender Gender { get; set; }

        public string Image { get; set; }

        public ClassModel Class { get; set; }

        public DetailsStudentViewModel(Models.Student student)
        {
            Id = student.Id;
            FirstName = student.FirstName;
            LastName = student.LastName;
            Active = student.Active;
            Age = student.Age;
            Gender = student.Gender;
            Class = student.Class != null
                ? new ClassModel
                {
                    Id = student.Class.Id,
                    Name = student.Class.Name
                }
                : null;
            Image = student.Image;
        }
    }
}