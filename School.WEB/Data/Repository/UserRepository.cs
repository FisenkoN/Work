using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using School.WEB.Models;

namespace School.WEB.Data.Repository
{
    public class UserRepository : BaseRepository<User>, IUserRepository

    {
        public UserRepository(SchoolDbContext db) : base(db)
        {
        }

        public async Task<User> GetForEmail(string email)
        {
            return await DbContext.Users.FirstOrDefaultAsync(u => u.Email == email);
        }
    }
}