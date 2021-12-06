using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using School.WEB.Models;

namespace School.WEB.Data.Repository
{
    public sealed class StudentRepository : BaseRepository<Student>, IStudentRepository
    {
        public StudentRepository(SchoolDbContext db) : base(db)
        {
        }

        public IIncludableQueryable<Student, Class> GetRelatedData()
        {
            return DbContext.Students
                .Include(s => s.Class);
        }

        public async Task<Student> GetOneRelated(int? id)
        {
            return await GetRelatedData()
                .FirstOrDefaultAsync(s => s.Id == id);
        }


        public override IQueryable<Student> GetSome(Expression<Func<Student, bool>> predicate)
        {
            return GetRelatedData()
                .Where(predicate);
        }
    }
}