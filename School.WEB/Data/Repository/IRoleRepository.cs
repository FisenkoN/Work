using System.Threading.Tasks;
using School.WEB.Models;

namespace School.WEB.Data.Repository
{
    public interface IRoleRepository
    {
        Task<Role> Get(string name);
        
        Task<Role> Get(int id);
    }
}