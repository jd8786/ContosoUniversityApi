using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using ContosoUniversity.Data.Exceptions;

namespace ContosoUniversity.Data.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        protected readonly SchoolContext _context;

        public BaseRepository(SchoolContext context)
        {
            _context = context;
        }

        public T Get(int id)
        {
            return _context.Set<T>().Find(id);
        }

        public IEnumerable<T> GetAll()
        {
            return _context.Set<T>().ToList();
        }

        public IEnumerable<T> Find(Expression<Func<T, bool>> predicate)
        {
            return _context.Set<T>().Where(predicate).ToList();
        }

        public void Remove(T entity)
        {
            _context.Set<T>().Remove(entity);
        }

        public void RemoveRange(IEnumerable<T> entities)
        {
            _context.Set<T>().RemoveRange(entities);
        }

        public void Add(T entity)
        {
            _context.Add(entity);
        }

        public void AddRange(IEnumerable<T> entities)
        {
            _context.AddRange(entities);
        }

        public void Update(T entity)
        {
            _context.Update(entity);
        }

        public void Save(string entityName)
        {
            try
            {
                _context.SaveChanges();
            }
            catch(Exception ex)
            {
                throw new FailedToSaveDatabaseException($"{entityName} failed to save to the database", ex);
            }
        }
    }
}
