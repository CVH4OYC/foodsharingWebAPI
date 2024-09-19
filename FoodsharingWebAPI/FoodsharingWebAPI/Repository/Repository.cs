using FoodsharingWebAPI.Data;
using FoodsharingWebAPI.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace FoodsharingWebAPI.Repository
{
    public class Repository <T> : IRepository<T> where T : class
    {
        DbContext context;
        public Repository(AppDbContext context)
        {
            this.context = context;
        }
        public async Task<IEnumerable<T>> GetAll()
        {
            return await context.Set<T>().ToListAsync(); ;
        }

        public async Task<T> GetById(int id)
        {
            return await context.FindAsync<T>(id);
        }

        public async Task<bool> Add(T entity)
        {
            await context.AddAsync(entity);
            return await Save();
        }

        public async Task<bool> Update(T entity)
        {
            context.Update(entity);
            return await Save();
        }

        public async Task<bool> Delete(T entity)
        {
            context.Remove(entity);
            return await Save();
        }
        public async Task<bool> Save()
        {
            return await context.SaveChangesAsync() > 0;
        }
        public async Task<IEnumerable<T>> GetWithInclude(params Expression<Func<T, object>>[] includeProperties)
        {
            return await Include(includeProperties).ToListAsync();
        }

        public async Task<IEnumerable<T>> GetWithInclude(Func<T, bool> predicate,
            params Expression<Func<T, object>>[] includeProperties)
        {
            var query = Include(includeProperties);
            return await Task.FromResult(query.Where(predicate).ToList());
        }

        private IQueryable<T> Include(params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = context.Set<T>().AsNoTracking();
            return includeProperties
                .Aggregate(query, (current, includeProperty) => current.Include(includeProperty));
        }

        private bool disposed = false;
        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
