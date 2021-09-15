using System;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using School.DAL.Entities;
using School.DAL.Interfaces;

namespace School.DAL.Repository
{
    public class StudentRepository : BaseRepository<Student>, IStudentRepository
    {
        public StudentRepository()
        {
            
        }
        
        public StudentRepository(SchoolDbContext db) : base(db)
        {
            
        }

        public virtual IIncludableQueryable<Student, Class> GetRelatedData() =>
            DbContext.Students
                .Include(s => s.Subjects)
                .Include(s => s.Class);

        public virtual Student GetOneRelated(int? id) =>
            GetAll()
                .ToList()
                .Find(s => s.Id == id);


        public override IQueryable<Student> GetSome(Expression<Func<Student, bool>> @where) =>
            GetRelatedData()
                .Where(where);
    }
}