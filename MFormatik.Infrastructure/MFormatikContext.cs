using Microsoft.EntityFrameworkCore;

namespace MFormatik.Infrastructure
{
    public class MFormatikContext : DbContext
    {
        public MFormatikContext(DbContextOptions<MFormatikContext> options)
            : base(options) { }

        // TODO: DbSets ...
    }
}
