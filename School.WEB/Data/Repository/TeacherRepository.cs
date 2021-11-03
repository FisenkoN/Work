using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using School.WEB.Models;

namespace School.WEB.Data.Repository
{
    public class TeacherRepository : BaseRepository<Teacher>, ITeacherRepository
    {
        public TeacherRepository(SchoolDbContext db) : base(db)
        {
        }

        public virtual IIncludableQueryable<Teacher, Class> GetRelatedData()
        {
            return DbContext.Teachers
                .Include(t => t.Subjects)
                .Include(t => t.Class);
        }

        public virtual async Task<Teacher> GetOneRelated(int? id)
        {
            return await GetRelatedData()
                .FirstOrDefaultAsync(s => s.Id == id);
        }

        public override IQueryable<Teacher> GetSome(Expression<Func<Teacher, bool>> where)
        {
            return GetRelatedData()
                .Where(where)
                .Select(t => t);
        }
    }
}