using System.Collections.Generic;

namespace School.BLL.Dto
{
    public class StudentDto
    {
        public int Id { get; set; }

        public string FullName => FirstName + " " + LastName;

        public string FirstName { get; set; }

        public GenderDto Gender { get; set; }

        public string LastName { get; set; }

        public int Age { get; set; }

        public int? ClassId { get; set; }

        public IEnumerable<int> SubjectIds { get; set; }
    }
}