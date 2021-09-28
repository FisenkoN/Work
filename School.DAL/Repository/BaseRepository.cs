using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using School.DAL.Entities;
using School.DAL.Interfaces;

namespace School.DAL.Repository
{
    public class BaseRepository<T> : IRepository<T> where T : EntityBase, new()
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

        public virtual int Add(T entity)
        {
            entity.CreatedTime = DateTime.Now;
            
            entity.LastUpdatedTime = DateTime.Now;
            
            _table.Add(entity);

            return SaveChanges();
        }

        internal int SaveChanges()
        {
            try
            {
                return DbContext.SaveChanges();
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
        
        public virtual int Update(T entity)
        {
            entity.LastUpdatedTime = DateTime.Now;
            
            _table.Update(entity);

            return SaveChanges();
        }

        public virtual int Delete(int id)
        {
            Delete(_table.FirstOrDefault(t => t.Id == id));

            return SaveChanges();
        }

        public virtual int Delete(T entity)
        {
            DbContext.Entry(entity).State = EntityState.Deleted;

            return SaveChanges();
        }

        public virtual T GetOne(int? id) =>
            _table.Find(id);

        public virtual IQueryable<T> GetSome(Expression<Func<T, bool>> @where) =>
            _table.Where(where);

        public virtual List<T> GetAll() => _table.ToList();
    }
}