using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using School.WEB.Models;

namespace School.WEB.Data.Repository
{
    public class StudentRepository : BaseRepository<Student>,
        IStudentRepository
    {
        public StudentRepository(SchoolDbContext db) : base(db)
        {
        }

        public virtual IIncludableQueryable<Student, Class> GetRelatedData()
        {
            return DbContext.Students
                .Include(s => s.Subjects)
                .Include(s => s.Class);
        }

        public virtual async Task<Student> GetOneRelated(int? id)
        {
            var students = GetRelatedData();

            return await students.FirstOrDefaultAsync(s => s.Id == id);
        }


        public override IQueryable<Student> GetSome(Expression<Func<Student, bool>> where)
        {
            return GetRelatedData()
                .Where(where);
        }
    }
}