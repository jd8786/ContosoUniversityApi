using System.Collections.Generic;

namespace ContosoUniversity.Data.Repositories
{
    public interface IBaseRepository<T> where T: class 
    {
        IEnumerable<T> GetAll();

        void Remove(T entity);

        void RemoveRange(IEnumerable<T> entities);

        void Add(T entity);

        void AddRange(IEnumerable<T> entities);

        void Update(T student);

        void Save();
    }
}
