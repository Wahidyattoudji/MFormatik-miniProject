using MFormatik.Core.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace MFormatik.Infrastructure.Data.Config
{
    public class OrderConfig : EntityTypeConfiguration<Order>
    {
        public OrderConfig()
        {
            ToTable("Order", "dbo");

            // Primary key
            HasKey(p => p.Id);
            Property(p => p.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity)
                .HasColumnName("OrdertId");

            // Properties
            Property(o => o.OrderDate)
                .IsRequired();

            Property(o => o.Total)
                .HasPrecision(18, 2);

            Property(o => o.TotalNet)
                .HasPrecision(18, 2);

            Property(o => o.DiscountRate)
                .HasPrecision(5, 2);

            // Foreign key relationship with Client
            HasRequired(o => o.Client)
                .WithMany(c => c.Orders)
                .HasForeignKey(o => o.ClientId)
                .WillCascadeOnDelete(false);
        }
    }
}
