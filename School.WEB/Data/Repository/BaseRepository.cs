using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using School.WEB.Models;

namespace School.WEB.Data.Repository
{
    public class BaseRepository<T> : IBaseRepository<T> where T : EntityBase, new()
    {
        private readonly DbSet<T> _table;
        protected SchoolDbContext DbContext { get; }

        protected BaseRepository(SchoolDbContext db)
        {
            DbContext = db;

            _table = db.Set<T>();
        }

        public virtual async Task Add(T entity)
        {
            await _table.AddAsync(entity);
        }

        public async Task SaveChanges()
        {
            await DbContext.SaveChangesAsync();
        }

        public virtual void Update(T entity)
        {
            _table.Update(entity);
        }

        public virtual void Delete(T entity)
        {
            DbContext.Entry(entity)
                .State = EntityState.Deleted;
        }

        public virtual async Task<T> GetOne(int? id)
        {
            return await _table.FindAsync(id);
        }

        public virtual IQueryable<T> GetSome(Expression<Func<T, bool>> predicate)
        {
            return _table.Where(predicate);
        }

        public virtual async Task<List<T>> GetAll()
        {
            return await _table.ToListAsync();
        }
    }
}