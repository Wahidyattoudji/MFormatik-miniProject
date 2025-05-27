using MFormatik.Core.Contracts;
using MFormatik.Infrastructure;
using System.Data.Entity;
using VisaBOT.Core.Extentions;

namespace HolcimTC.Data.Repositories
{
    public abstract class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        private readonly IDbContextFactory<MFormatikContext> _dbContextFactory;

        protected BaseRepository(IDbContextFactory<MFormatikContext> dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }
        public async Task<string> AddAsync(T entity)
        {
            try
            {
                using (var context = _dbContextFactory.CreateDbContext())
                {
                    context.Set<T>().Add(entity);
                    await context.SaveChangesAsync();
                }
                return "1";
            }
            catch (Exception ex)
            {
                ex.LogError();
                return ex.InnerException?.Message ?? ex.Message;
            }
        }

        public async Task<string> EditAsync(T entity)
        {
            try
            {
                using (var context = _dbContextFactory.CreateDbContext())
                {
                    context.Set<T>().Attach(entity);
                    context.Entry(entity).State = EntityState.Modified;
                    await context.SaveChangesAsync();
                }
                return "1";
            }
            catch (Exception ex)
            {
                ex.LogError();
                return ex.InnerException?.Message ?? ex.Message;
            }
        }

        public async Task<string> DeleteAsync(int id)
        {
            try
            {
                using (var context = _dbContextFactory.CreateDbContext())
                {
                    var entity = await context.Set<T>().FindAsync(id);
                    if (entity == null)
                        return "Entity not found";

                    context.Set<T>().Remove(entity);
                    await context.SaveChangesAsync();
                }
                return "1";
            }
            catch (Exception ex)
            {
                ex.LogError();
                return ex.InnerException?.Message ?? ex.Message;
            }
        }
    }
}
