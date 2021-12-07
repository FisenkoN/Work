namespace School.WEB.Models
{
    public class Admin : EntityBase
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }
        
        public string Image { get; set; }

        public int Age { get; set; }

        public Gender Gender { get; set; }

        public int? UserId { get; set; }

        public User User { get; set; }
    }
}