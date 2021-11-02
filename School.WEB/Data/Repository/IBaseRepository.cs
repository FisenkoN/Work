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
        Task<int> Add(T entity);

        Task<int> Update(T entity);

        Task<int> Delete(int id);

        Task<int> Delete(T entity);

        Task<T> GetOne(int? id);

        IQueryable<T> GetSome(Expression<Func<T, bool>> where);

        Task<List<T>> GetAll();
    }
}