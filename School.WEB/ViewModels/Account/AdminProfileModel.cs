using School.WEB.Models;

namespace School.WEB.ViewModels.Account
{
    public class AdminProfileModel
    {
        public int Id { get; set; }
        
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Image { get; set; }

        public int Age { get; set; }

        public string Email { get; set; }
        
        public string Password { get; set; }

        public string RoleName { get; set; }

        public Gender Gender { get; set; }

        public AdminProfileModel(Admin admin, User user, Role role)
        {
            Id = admin.Id;
            FirstName = admin.FirstName;
            LastName = admin.LastName;
            Age = admin.Age;
            Image = admin.Image;
            Gender = admin.Gender;
            Email = user.Email;
            Password = user.Password;
            RoleName = role.Name;
        }
    }
}