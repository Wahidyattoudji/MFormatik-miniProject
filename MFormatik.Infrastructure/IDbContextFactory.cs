using System.Data.Entity;

namespace MFormatik.Infrastructure
{
    public interface IDbContextFactory<TContext> where TContext : DbContext
    {
        TContext CreateDbContext();
    }
}
