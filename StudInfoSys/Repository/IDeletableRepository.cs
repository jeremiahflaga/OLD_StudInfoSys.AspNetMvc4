using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace StudInfoSys.Repository
{
    public interface IDeletableRepository<T> : IRepository<T>
    {
        IQueryable<T> SearchFor(Expression<Func<T, bool>> predicate, bool includeDeleted);
        new IQueryable<T> GetAll();
        new void Delete(T entity);
        void Restore(T entity);
    }
}