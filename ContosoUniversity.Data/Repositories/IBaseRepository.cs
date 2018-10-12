using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ContosoUniversity.Data.Repositories
{
    public interface IBaseRepository<T> where T: class 
    {
        Task<T> Get(int id);

        Task<IEnumerable<T>> GetAll();

        Task<IEnumerable<T>> Find(Expression<Func<T, bool>> predicate);

        Task Remove(T entity);

        Task RemoveRange(IEnumerable<T> entities);

        Task Add(T entity);

        Task AddRange(IEnumerable<T> entities);

        Task Update(T entity);
    }
}
