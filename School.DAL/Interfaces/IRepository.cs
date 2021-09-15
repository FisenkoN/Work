using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using School.DAL.Entities;

namespace School.DAL.Interfaces
{
    public interface IRepository<T> where T : EntityBase, new()
    {
        int Add(T entity);

        int Update(T entity);

        int Delete(int id);

        int Delete(T entity);

        T GetOne(int? id);

        IQueryable<T> GetSome(Expression<Func<T, bool>> where);

        List<T> GetAll();
    }
}