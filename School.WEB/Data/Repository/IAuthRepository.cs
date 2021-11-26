using System.Threading.Tasks;
using School.WEB.Models;

namespace School.WEB.Data.Repository
{
    public interface IAuthRepository
    {
        Task<User> Get(string email, string password);
        
        Task<User> Get(string email);
        
        Task<User> Get(int id);

        void CleanLocal();

        void Update(User user);

        Task Add(User user);

        Task SaveChanges();
    }
}