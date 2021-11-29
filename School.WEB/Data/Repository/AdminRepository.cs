using School.WEB.Models;

namespace School.WEB.Data.Repository
{
    public class AdminRepository : BaseRepository<Admin>, IAdminRepository
    {
        public AdminRepository(SchoolDbContext db) : base(db)
        {
            
        }
    }
}