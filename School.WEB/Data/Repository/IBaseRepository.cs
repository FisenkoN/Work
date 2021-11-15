using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using School.WEB.Models;

namespace School.WEB.Data.Repository
{
    public interface IBaseRepository<T> where T : EntityBase, new()
    {
        Task Add(T entity);

        void Update(T entity);

        void Delete(T entity);

        Task<T> GetOne(int? id);

        Task SaveChanges();

        IQueryable<T> GetSome(Expression<Func<T, bool>> predicate);

        Task<List<T>> GetAll();
    }
}