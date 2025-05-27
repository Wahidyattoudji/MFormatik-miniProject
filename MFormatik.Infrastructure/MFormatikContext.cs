using MFormatik.Core.Models;
using MFormatik.Infrastructure.Data.Config;
using Microsoft.EntityFrameworkCore;

namespace MFormatik.Infrastructure
{
    public class MFormatikContext : DbContext
    {
        public MFormatikContext(DbContextOptions<MFormatikContext> options)
            : base(options) { }

        public virtual DbSet<Client> Clients { get; set; }
        public virtual DbSet<Product> Products { get; set; }

        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<OrderItem> OrderItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ClientConfig).Assembly);
        }
    }
}
