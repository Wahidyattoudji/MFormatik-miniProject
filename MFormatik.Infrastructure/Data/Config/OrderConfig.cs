using MFormatik.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MFormatik.Infrastructure.Data.Config
{
    public class OrderConfig : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.ToTable("Order", "dbo");

            // Primary key
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("OrdertId");

            builder.Property(o => o.OrderDate).IsRequired();
            builder.Property(o => o.Total).HasColumnType("decimal(18,2)");
            builder.Property(o => o.TotalNet).HasColumnType("decimal(18,2)");
            builder.Property(o => o.DiscountRate).HasColumnType("decimal(5,2)");

            // Foreign key relationship with Client
            builder.HasOne(o => o.Client)
                   .WithMany(c => c.Orders)
                   .HasForeignKey(o => o.ClientId)
                   .HasConstraintName("Order_ClientId");
        }
    }


}
