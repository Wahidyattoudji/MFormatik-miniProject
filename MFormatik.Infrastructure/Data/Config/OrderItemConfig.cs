using MFormatik.Core.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace MFormatik.Infrastructure.Data.Config
{
    public class OrderItemConfig : EntityTypeConfiguration<OrderItem>
    {
        public OrderItemConfig()
        {
            ToTable("OrderItem", "dbo");

            // Primary key
            HasKey(p => p.Id);
            Property(p => p.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity)
                .HasColumnName("OrderItemId");

            Property(oi => oi.Quantity)
                .IsRequired();

            Property(oi => oi.UnitPrice)
                .HasPrecision(18, 2)
                .IsRequired();

            Property(oi => oi.DiscountRate)
                .HasPrecision(5, 2);

            Property(oi => oi.Position)
                .IsRequired();

            // Relationships (1 - N)
            HasRequired(oi => oi.Order)
                .WithMany(o => o.OrderItems)
                .HasForeignKey(oi => oi.OrderId)
                .WillCascadeOnDelete(true);

            HasRequired(oi => oi.Product)
                .WithMany(p => p.OrderItems)
                .HasForeignKey(oi => oi.ProductId)
                .WillCascadeOnDelete(true);
        }
    }
}
