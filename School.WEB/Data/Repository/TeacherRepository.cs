using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using School.WEB.Models;

namespace School.WEB.Data.Repository
{
    public sealed class TeacherRepository : BaseRepository<Teacher>, ITeacherRepository
    {
        public TeacherRepository(SchoolDbContext db) : base(db)
        {
        }

        public IIncludableQueryable<Teacher, Class> GetRelatedData()
        {
            return DbContext.Teachers
                .Include(t => t.Subjects)
                .Include(t => t.Class);
        }

        public async Task<Teacher> GetOneRelated(int? id)
        {
            return await GetRelatedData()
                .FirstOrDefaultAsync(s => s.Id == id);
        }

        public override IQueryable<Teacher> GetSome(Expression<Func<Teacher, bool>> predicate)
        {
            return GetRelatedData()
                .Where(predicate)
                .Select(t => t);
        }
    }
}