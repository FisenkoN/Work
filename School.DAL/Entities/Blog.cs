namespace School.DAL.Entities
{
    public class Blog : EntityBase
    {
        public string Name { get; set; }

        public string Category { get; set; }

        public string Text { get; set; }

        public string Image { get; set; }
    }
}