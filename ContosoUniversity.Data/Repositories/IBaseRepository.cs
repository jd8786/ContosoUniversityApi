using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace ContosoUniversity.Data.Repositories
{
    public interface IBaseRepository<T> where T: class 
    {
        T Get(int id);

        IEnumerable<T> GetAll();

        IEnumerable<T> Find(Expression<Func<T, bool>> predicate);

        void Remove(T entity);

        void RemoveRange(IEnumerable<T> entities);

        void Add(T entity);

        void AddRange(IEnumerable<T> entities);

        void Update(T entity);

        void Save(string entityName);
    }
}
