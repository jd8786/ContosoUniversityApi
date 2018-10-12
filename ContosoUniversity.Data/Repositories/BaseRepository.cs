using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ContosoUniversity.Data.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T: class
    {
        private readonly SchoolContext _context;

        public BaseRepository(SchoolContext context)
        {
            _context = context;
        }

        public async Task<T> Get(int id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public async Task<IEnumerable<T>> Find(Expression<Func<T, bool>> predicate)
        {
            return await _context.Set<T>().Where(predicate).ToListAsync();
        }

        public async Task Remove(T entity)
        {
            _context.Set<T>().Remove(entity);

            await _context.SaveChangesAsync();
        }

        public async Task RemoveRange(IEnumerable<T> entities)
        {
            _context.Set<T>().RemoveRange(entities);

            await _context.SaveChangesAsync();
        }

        public async Task Add(T entity)
        {
            await _context.AddAsync(entity);

            await _context.SaveChangesAsync();
        }

        public async Task AddRange(IEnumerable<T> entities)
        {
            await _context.AddRangeAsync(entities);

            await _context.SaveChangesAsync();
        }

        public async Task Update(T entity)
        {
            _context.Update(entity);

            await _context.SaveChangesAsync();
        }
    }
}
