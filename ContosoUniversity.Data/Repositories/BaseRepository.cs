﻿using System.Collections.Generic;
using System.Linq;

namespace ContosoUniversity.Data.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        public readonly SchoolContext Context;

        public BaseRepository(SchoolContext context)
        {
            Context = context;
        }

        public virtual IEnumerable<T> GetAll()
        {
            return Context.Set<T>().ToList();
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
            Context.Set<T>().Add(entity);
        }

        public void AddRange(IEnumerable<T> entities)
        {
            Context.Set<T>().AddRange(entities);
        }

        public virtual void Update(T student)
        {
            Context.Set<T>().Update(student);
        }

        public void Save()
        {
            Context.SaveChanges();
        }
    }
}
