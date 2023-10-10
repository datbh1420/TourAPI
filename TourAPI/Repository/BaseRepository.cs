using Microsoft.EntityFrameworkCore;
using TourAPI.Data;

namespace TourAPI.Repository
{
    public class BaseRepository<T> where T : class
    {
        private readonly TourDBContext context;

        public BaseRepository(TourDBContext context)
        {
            this.context = context;
        }

        public async Task<List<T>> GetAllAsync()
        {
            try
            {
                var list = await context.Set<T>().ToListAsync();
                return list;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<T?> GetByIdAsync(string Id)
        {
            try
            {
                var entity = await context.Set<T>().FindAsync(Id);
                return entity;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> CreateAsync(T entity)
        {
            try
            {
                await context.Set<T>().AddAsync(entity);
                await context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> UpdateAsync(T entity)
        {
            try
            {
                context.Set<T>().Update(entity);
                await context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> DeleteAsync(string Id)
        {
            try
            {
                var entity = await context.Set<T>().FindAsync(Id);
                if (entity == null)
                {
                    return false;
                }
                context.Set<T>().Remove(entity);
                await context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
