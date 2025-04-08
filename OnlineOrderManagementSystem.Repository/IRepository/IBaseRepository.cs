using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace OnlineOrderManagementSystem.Repository.IRepository
{
    public interface IBaseRepository<T> where T : class
    {
        Task<T> AddAsync(T entity);
        void Remove(T entity);
        void Update(T entity);
        IQueryable<T> GetAll();
        Task<T> GetByIdAsync(object id);
        Task<T> GetByIdAsync(object id, Func<IQueryable<T>, IQueryable<T>> queryModifier = null);
    }
}
