using FoodsharingWebAPI.Data;
using FoodsharingWebAPI.Interfaces;
using FoodsharingWebAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace FoodsharingWebAPI.Repository
{
    // универсальный репозиторий
    public class Repository <T> : IRepository<T> where T : class
    {
        DbContext context;
        public Repository(AppDbContext context)
        {
            this.context = context;
        }
        public async Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await context.Set<T>().ToListAsync(); ;
        }

        public async Task<T> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            return await context.FindAsync<T>(id);
        }

        public async Task AddAsync(T entity, CancellationToken cancellationToken = default)
        {
            await context.AddAsync(entity);
            await context.SaveChangesAsync();
        }

        public async Task UpdateAsync(T entity, CancellationToken cancellationToken = default)
        {
            // прикрепляем к контексту без отслеживания изменений
            context.Set<T>().Attach(entity);
            // говорим, что сущность изменена
            context.Entry(entity).State = EntityState.Modified;
            await context.SaveChangesAsync();
        }

        public async Task DeleteByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            var entity = new Address { Id = id };
            context.Entry(entity).State = EntityState.Deleted;
            await context.SaveChangesAsync();
        }
        public async Task<IEnumerable<T>> GetWithIncludeAsync(CancellationToken cancellationToken = default, params Expression<Func<T, object>>[] includeProperties)
        {
            return await Include(includeProperties).ToListAsync();
        }

        public async Task<IEnumerable<T>> GetWithIncludeAsync(Func<T, bool> predicate, CancellationToken cancellationToken = default,
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
