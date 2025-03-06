using Microsoft.EntityFrameworkCore;
using Stocker.Infrastructer.Data;
using Stocker.Infrastructer.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Stocker.Infrastructer.Repository
{
    class Repository<T> : IRepository<T> where T : class
    {
        private readonly ApplicationDbContext _context;

        public Repository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<T> GetAll()
        {
            return _context.Set<T>().ToList();
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }
        public async Task<IEnumerable<T>> GetAllAsyncWithNoTracking(string[] includes = null)
        {
            var query =  _context.Set<T>().AsNoTracking();
            foreach (var inlcude in includes)
            {
                query.Include(inlcude);
            }
            return await query.ToListAsync();
        }

        public async Task<T> Find(Expression<Func<T, bool>> filter, string[] includes = null)
        {
            IQueryable<T> query = _context.Set<T>();
            foreach (var inlcude in includes ?? [])
            {
                query = query.Include(inlcude);
            }
            return await query.Where(filter).FirstOrDefaultAsync();
        }
        public async Task<T> GetByIdAsync(int id, string[] includes = null)
        {
            var query = _context.Set<T>();
            foreach (var inlcude in includes ?? [])
            {
                query.Include(inlcude);
            }
            return await query.FindAsync(id);
        }

        public async Task<IEnumerable<T>> GetWhereAsync(Expression<Func<T, bool>> filter)
        {
            return await _context.Set<T>().Where(filter).ToListAsync();
        }

        public async Task<IEnumerable<T>> GetWhereWithIncludeAsync(Expression<Func<T, bool>> filter, string[] inlcudes = null)
        {
            IQueryable<T> query = _context.Set<T>();
            foreach (var inlcude in inlcudes ?? [])
            {
                query = query.Include(inlcude);
            }
            return await query.Where(filter).ToListAsync();
        }

        public void Add(T entity)
        {
            _context.Set<T>().Add(entity);
        }

        public async Task AddAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
        }

        public async Task AddRangeAsync(IEnumerable<T> entities)
        {
            await _context.Set<T>().AddRangeAsync(entities);
        }

        public void Update(int id, T newEntity)
        {
            _context.Set<T>().Update(newEntity);
        }

        public void Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
        }

        public void DeleteRange(IEnumerable<T> entities)
        {
            _context.Set<T>().RemoveRange(entities);
        }

        public async Task<bool> IsExist(Expression<Func<T, bool>> filter)
        {
            return await _context.Set<T>().AnyAsync(filter);
        }
    }
}
