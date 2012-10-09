using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace StudInfoSys.Repository
{
    public interface IRepository<T>
    {
        void Insert(T entity);
        void Delete(T entity);
        void Update(T entity);
        IQueryable<T> SearchFor(Expression<Func<T, bool>> predicate);
        IQueryable<T> GetAll();
        T GetById(int id);
        void Save();
        //void Restore(T entity);

    }
}