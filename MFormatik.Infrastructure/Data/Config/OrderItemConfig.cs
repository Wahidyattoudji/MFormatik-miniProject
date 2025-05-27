using MFormatik.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MFormatik.Infrastructure.Data.Config
{
    public class OrderItemConfig : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            builder.ToTable("OrderItem", "dbo");

            // Primary key
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("OrderItemId");

            builder.Property(oi => oi.Quantity).IsRequired();
            builder.Property(oi => oi.UnitPrice)
                   .HasColumnType("decimal(18,2)")
                   .IsRequired();

            builder.Property(oi => oi.DiscountRate).HasColumnType("decimal(5,2)");

            builder.Property(oi => oi.Position).IsRequired();

            // 1 - N 
            builder.HasOne(oi => oi.Order)
                   .WithMany(o => o.OrderItems)
                   .HasForeignKey(oi => oi.OrderId)
                   .HasConstraintName("OrderItem_ClientId");
            // 1 - N 
            builder.HasOne(oi => oi.Product)
                   .WithMany(p => p.OrderItems)
                   .HasForeignKey(oi => oi.ProductId)
                   .HasConstraintName("OrderItem_ProductId");
        }
    }


}
