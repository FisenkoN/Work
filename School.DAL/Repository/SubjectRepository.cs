using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using School.DAL.Entities;
using School.DAL.Interfaces;

namespace School.DAL.Repository
{
    public class SubjectRepository : BaseRepository<Subject>,
        ISubjectRepository
    {
        public SubjectRepository(SchoolDbContext db) : base(db)
        {
        }

        public SubjectRepository()
        {
        }

        public virtual IIncludableQueryable<Subject, ICollection<Teacher>> GetRelatedData()
        {
            return DbContext.Subjects
                .Include(s => s.Students)
                .Include(s => s.Teachers);
        }

        public virtual Subject GetOneRelated(int? id)
        {
            return GetRelatedData()
                .ToList()
                .Find(s => s.Id == id);
        }
    }
}