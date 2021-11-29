using System.Collections.Generic;
using System.Threading.Tasks;
using School.WEB.Models;

namespace School.WEB.Data.Repository
{
    public interface IRoleRepository
    {
        Task<Role> Get(string name);
        
        Task<Role> GetForEmail(string email);

        Task<IEnumerable<Role>> GetAll();

        Task<Role> Get(int id);
    }
}