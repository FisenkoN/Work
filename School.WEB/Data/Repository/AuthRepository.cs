using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using School.WEB.Models;

namespace School.WEB.Data.Repository
{
    public class AuthRepository:IAuthRepository
    {
        private readonly SchoolDbContext _db;

        public AuthRepository(SchoolDbContext db)
        {
            _db = db;
        }

        public async Task<User> Get(string email, string password)
        {
            return await _db.Users.FirstOrDefaultAsync(u => u.Email == email && u.Password == password);
        }

        public async Task<User> GetWhenForgotPassword(string email)
        {
            return await _db.Users.FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task Add(User user)
        {
            await _db.Users.AddAsync(user);
        }

        public async Task SaveChanges()
        {
            await _db.SaveChangesAsync();
        }
    }
}