using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using StudInfoSys.Models;

namespace StudInfoSys.Repository
{
    public class DeletableRepositoryBase<T> : RepositoryBase<T>, IDeletableRepository<T> where T : class, IDeletableEntity
    {
        public DeletableRepositoryBase(DbContext dataContext)
            :base(dataContext)
        {
            DbSet = dataContext.Set<T>();
            Context = dataContext;
        }


        public new virtual void Delete(T entity)
        {
            //TODO: test passing a null value as entity and see if it enters this "if"
            //var e = (entity as IDeletableEntity);
            if (entity != null)
            {
                entity.IsDeleted = true;
            }
        }

        public virtual IQueryable<T> SearchFor(Expression<Func<T, bool>> predicate, bool includeDeleted)
        {
            return DbSet.Where(predicate).Where(e => e.IsDeleted == false);
        }

        /// <summary>
        /// Gets all records. But if an entity implements IDeletableEntity, this method only those whose IsDeleted property is false.
        /// </summary>
        /// <returns></returns>
        public new virtual IQueryable<T> GetAll()
        {
            return DbSet.Where(e => e.IsDeleted == false);
        }


        public virtual void Restore(T entity)
        {
            //var e = (entity as IDeletableEntity);
            if (entity != null)
            {
                entity.IsDeleted = false;
            }
        }
    }
}