using Microsoft.EntityFrameworkCore;
using OnlineOrderManagementSystem.Inferastructure.Data;
using OnlineOrderManagementSystem.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace OnlineOrderManagementSystem.Inferastructure.Repositories
{
    
    internal class BaseRepository<T> : IBaseRepository<T> 
        where T : class
    {
        protected readonly AppDbContext context;

        public BaseRepository(AppDbContext context)
        {
            this.context = context;
        }

        public virtual async Task<T> AddAsync(T entity)
        {
            var set = context.Set<T>(); 
            var entry = await set.AddAsync(entity);

            return entry.Entity;
        }

        public virtual void Remove(T entity)
        {
            context.Set<T>().Remove(entity);
        }

        public virtual void Update(T entity)
        {
            context.Set<T>().Update(entity);
        }

        public virtual IQueryable<T> GetAll()
        {
            return context.Set<T>();
        }

        public virtual async Task<T> GetByIdAsync(object id)
        {
            return await context.Set<T>().FindAsync(id);
        }

        public virtual async Task<T> GetByIdAsync(object id,
                                                  Func<IQueryable<T>, 
                                                  IQueryable<T>> queryModifier = null)
        {
            IQueryable<T> query = context.Set<T>();

            // Apply the query modifier if provided
            if (queryModifier != null)
            {
                query = queryModifier(query);
            }

            // Find the entity by its primary key
            return await query.FirstOrDefaultAsync(e => EF.Property<object>(e, "Id").Equals(id));
        }
    }
}
