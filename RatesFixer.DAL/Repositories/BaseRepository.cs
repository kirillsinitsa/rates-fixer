using System.Linq;
using System.Data.Entity;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace RatesFixer.DAL.Repositories
{
    public abstract class BaseRepository<T, TContext> 
        where T : class
        where TContext : DbContext
    {
        protected TContext context;
        protected DbSet<T> dbset;

        public BaseRepository(TContext context)
        {
            this.context = context;
        }

        public virtual void Create(T item)
        {
            dbset.Add(item);
        }

        public virtual void Delete(int id)
        {
            var entity = dbset.Find(id);
            if (entity != null)
            {
                dbset.Remove(entity);
            }
        }

        public virtual void DeleteRange(IEnumerable<int> idRange)
        { }

        public virtual IEnumerable<T> Find(Func<T, bool> predicate)
        {
            return dbset.Where(predicate);
        }

        public virtual T Get(int id)
        {
            return dbset.Find(id);
        }

        public virtual IEnumerable<T> GetAll()
        {
            return dbset;
        }

        public virtual void Update(T item)
        {
            context.Entry(item).State = EntityState.Modified;
        }
    }
}
