using System;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using School.DAL.Entities;
using School.DAL.Interfaces;

namespace School.DAL.Repository
{
    public class TeacherRepository : BaseRepository<Teacher>,
        ITeacherRepository
    {
        public TeacherRepository()
        {
        }

        public TeacherRepository(SchoolDbContext db) : base(db)
        {
        }

        public virtual IIncludableQueryable<Teacher, Class> GetRelatedData()
        {
            return DbContext.Teachers
                .Include(t => t.Subjects)
                .Include(t => t.Class);
        }

        public virtual Teacher GetOneRelated(int? id)
        {
            return GetRelatedData()
                .ToList()
                .Find(t => t.Id == id);
        }

        public override IQueryable<Teacher> GetSome(Expression<Func<Teacher, bool>> where)
        {
            return GetRelatedData()
                .Where(where)
                .Select(t => t);
        }
    }
}