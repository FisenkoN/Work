using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using School.WEB.Models;

namespace School.WEB.Data.Repository
{
    public class RoleRepository : IRoleRepository
    {
        private readonly SchoolDbContext _db;
        
        public RoleRepository(SchoolDbContext db)
        {
            _db = db;
        }
        
        public async Task<Role> Get(string name)
        {
            return await _db.Roles.FirstOrDefaultAsync(r => r.Name == name);
        }

        public async Task<Role> Get(int id)
        {
            return await _db.Roles.FirstOrDefaultAsync(r => r.Id == id);
        }
    }
}