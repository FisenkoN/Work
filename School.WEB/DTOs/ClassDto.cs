using System.Collections.Generic;

namespace School.WEB.DTOs
{
    public class ClassDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int? TeacherId { get; set; }

        public IEnumerable<int> StudentIds { get; set; }
    }
}