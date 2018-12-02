using ContosoUniversity.Data.Exceptions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ContosoUniversity.Data.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        protected readonly SchoolContext Context;

        public BaseRepository(SchoolContext context)
        {
            Context = context;
        }

        public virtual IEnumerable<T> GetAll()
        {
            return Context.Set<T>().AsNoTracking().ToList();
        }

        public void Remove(T entity)
        {
            Context.Set<T>().Remove(entity);
        }

        public void RemoveRange(IEnumerable<T> entities)
        {
            Context.Set<T>().RemoveRange(entities);
        }

        public void Add(T entity)
        {
            Context.Add(entity);
        }

        public void AddRange(IEnumerable<T> entities)
        {
            Context.AddRange(entities);
        }

        public void Update(T entity)
        {
            Context.Update(entity);
        }

        public void Save(string entityName)
        {
            try
            {
                Context.SaveChanges();
            }
            catch(Exception ex)
            {
                throw new FailedToSaveDatabaseException($"{entityName} failed to save to the database", ex);
            }
        }
    }
}
