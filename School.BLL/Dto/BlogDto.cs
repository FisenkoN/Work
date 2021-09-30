using System;

namespace School.BLL.Dto
{
    public class BlogDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Text { get; set; }

        public string Category { get; set; }

        public string Image { get; set; }   
        
        public DateTime CreatedTime { get; set; }

        public DateTime LastUpdatedTime { get; set; }
    }
}