using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Stocker.Infrastructer.Repository.Interfaces
{
    public interface IRepository<T>
    {
        // Fetch
        Task<T> Find(Expression<Func<T, bool>> filter, string[] includes = null);
        IEnumerable<T> GetAll();
        Task<IEnumerable<T>> GetAllAsync();
        Task<IEnumerable<T>> GetAllAsyncWithNoTracking(string[] includes);
        Task<T> GetByIdAsync(int id, string[] includes = null);
        Task<IEnumerable<T>> GetWhereAsync(Expression<Func<T, bool>> filter);
        Task<IEnumerable<T>> GetWhereWithIncludeAsync(Expression<Func<T, bool>> filter, string[] inlcudes);

        // Add
        void Add(T entity);
        Task AddAsync(T entity);
        Task AddRangeAsync(IEnumerable<T> entities);

        // Update
        void Update(int id, T newEntity);

        // Remove
        void Delete(T entity);
        void DeleteRange(IEnumerable<T> entities);


        Task<bool> IsExist(Expression<Func<T, bool>> filter);
    }
}
