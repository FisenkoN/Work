using System.Threading.Tasks;
using School.WEB.Models;

namespace School.WEB.Data.Repository
{
    public interface IUserRepository : IBaseRepository<User>
    {
        Task<User> GetForEmail(string email);
        
    }
}