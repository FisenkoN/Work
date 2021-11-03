using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using School.WEB.Models;

namespace School.WEB.Data.Repository
{
    public class SubjectRepository : BaseRepository<Subject>, ISubjectRepository
    {
        public SubjectRepository(SchoolDbContext db) : base(db)
        {
        }

        public virtual IIncludableQueryable<Subject, ICollection<Teacher>> GetRelatedData()
        {
            return DbContext.Subjects
                .Include(s => s.Students)
                .Include(s => s.Teachers);
        }

        public virtual async Task<Subject> GetOneRelated(int? id)
        {
            return await GetRelatedData()
                .FirstOrDefaultAsync(s => s.Id == id);
        }
    }
}