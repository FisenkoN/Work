using System.Collections.Generic;

namespace School.BLL.Dto
{
    public class SubjectDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public IEnumerable<int> TeacherIds { get; set; }

        public IEnumerable<int> StudentIds { get; set; }
    }
}