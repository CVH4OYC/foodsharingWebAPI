using FoodsharingWebAPI.Data;
using FoodsharingWebAPI.Interfaces;
using Microsoft.EntityFrameworkCore;

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
