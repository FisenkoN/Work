using System.Threading.Tasks;
using School.WEB.Models;

namespace School.WEB.Data.Repository
{
    public interface IAuthRepository
    {
        Task<User> Get(string email, string password);
        
        Task<User> GetWhenForgotPassword(string email);

        void CleanLocal();

        void Update(User user);

        Task Add(User user);

        Task SaveChanges();
    }
}