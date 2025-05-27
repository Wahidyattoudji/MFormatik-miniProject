using MFormatik.Core.Models;
using MFormatik.Infrastructure.Data.Config;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace MFormatik.Infrastructure
{
    public class MFormatikContext : DbContext
    {
        public MFormatikContext(string connectionString)
            : base(connectionString)
        {
        }

        public virtual DbSet<Client> Clients { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<OrderItem> OrderItems { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            // The Best Practise is to apply configurations from assembly name but its not supported in EF6
            modelBuilder.Configurations.Add(new ClientConfig());
            modelBuilder.Configurations.Add(new ProductConfig());
            modelBuilder.Configurations.Add(new OrderConfig());
            modelBuilder.Configurations.Add(new OrderItemConfig());

            base.OnModelCreating(modelBuilder);
        }
    }
}
