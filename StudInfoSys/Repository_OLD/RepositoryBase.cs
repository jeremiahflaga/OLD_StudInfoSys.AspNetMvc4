using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using StudInfoSys.Models;

namespace StudInfoSys.Repository
{
    public class RepositoryBase<T> : IRepository<T> where T : class 
    {
        protected DbSet<T> DbSet;
        protected DbContext Context;

        public RepositoryBase(DbContext dataContext)
        {
            DbSet = dataContext.Set<T>();
            Context = dataContext;
        }
 
        #region IRepository<T> Members
 
        public virtual void Insert(T entity)
        {
            DbSet.Add(entity);
        }

        public virtual void Delete(T entity)
        {
            DbSet.Remove(entity);
        }

        public virtual void Update(T entity)
        {
            Context.Entry(entity).State = EntityState.Modified;
        }

        public virtual IQueryable<T> SearchFor(Expression<Func<T, bool>> predicate)
        {
            return DbSet.Where(predicate);
        }

        /// <summary>
        /// Gets all records. But if an entity implements IDeletableEntity, this method only those whose IsDeleted property is false.
        /// </summary>
        /// <returns></returns>
        public virtual IQueryable<T> GetAll()
        {
            return DbSet;
        }

        public virtual T GetById(int id)
        {
            return DbSet.Find(id);
        }

        public virtual void Save()
        {
            Context.SaveChanges();
        }

        

        #endregion

    }
}