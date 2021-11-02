using System.Collections.Generic;

namespace School.WEB.DTOs
{
    public class SubjectDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public IEnumerable<int> TeacherIds { get; set; }

        public IEnumerable<int> StudentIds { get; set; }
    }
}