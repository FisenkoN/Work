using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using School.WEB.Models;

namespace School.WEB.Data.Repository
{
    public class ClassRepository : BaseRepository<Class>, IClassRepository
    {
        public ClassRepository(SchoolDbContext dbContext) : base(dbContext)
        {
        }

        public virtual IIncludableQueryable<Class, Teacher> GetRelatedData()
        {
            return DbContext.Classes
                .Include(c => c.Students)
                .Include(c => c.Teacher);
        }

        public virtual async Task<Class> GetOneRelated(int? id)
        {
            return await GetRelatedData()
                .FirstOrDefaultAsync(c => c.Id == id);
        }
    }
}