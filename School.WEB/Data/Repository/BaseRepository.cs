using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
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

        protected BaseRepository()
        {
            
        }

        public virtual async Task<int> Add(T entity)
        {
            await _table.AddAsync(entity);

            return await SaveChanges();
        }

        private async Task<int> SaveChanges()
        {
            try
            {
                return await DbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
            catch (RetryLimitExceededException e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
            catch (DbUpdateException e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
        }
        
        public virtual Task<int> Update(T entity)
        {
            _table.Update(entity);

            return SaveChanges();
        }

        public virtual async Task<int> Delete(int id)
        {
            await Delete(_table.FirstOrDefault(t => t.Id == id));

            return await SaveChanges();
        }

        public virtual async Task<int> Delete(T entity)
        {
            DbContext.Entry(entity).State = EntityState.Deleted;

            return await SaveChanges();
        }

        public virtual async Task<T> GetOne(int? id)
        {
            return await _table.FindAsync(id);
        }

        public virtual IQueryable<T> GetSome(Expression<Func<T, bool>> @where)
        {
            return _table.Where(where);
        }

        public virtual async  Task<List<T>> GetAll()
        {
            return await _table.ToListAsync();
        }
    }
}