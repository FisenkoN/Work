using System.Collections.Generic;
using System.Linq;

namespace School.WEB.ViewModels.Student.ShowClassmates
{
    public class ShowClassmatesViewModel
    {
        public IEnumerable<Classmate> Students { get; set; }

        public ShowClassmatesViewModel(IEnumerable<Models.Student> students)
        {
            Students = from student in students
                select new Classmate
                {
                    Id = student.Id,
                    Gender = student.Gender,
                    Age = student.Age,
                    Active = student.Active,
                    FirstName = student.FirstName,
                    Image = student.Image,
                    LastName = student.LastName
                };
        }
    }
}