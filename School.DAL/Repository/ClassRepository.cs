using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using School.DAL.Entities;
using School.DAL.Interfaces;

namespace School.DAL.Repository
{
    public class ClassRepository : BaseRepository<Class>, IClassRepository
    {
        public ClassRepository()
        {
        }

        public ClassRepository(SchoolDbContext dbContext) : base(dbContext)
        {
        }

        public virtual IIncludableQueryable<Class, Teacher> GetRelatedData()
        {
            return DbContext.Classes
                .Include(c => c.Students)
                .Include(c => c.Teacher);
        }

        public virtual Class GetOneRelated(int? id)
        {
            return GetRelatedData()
                .First(c => c.Id == id);
        }
    }
}