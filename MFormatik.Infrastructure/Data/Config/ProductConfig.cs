using MFormatik.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MFormatik.Infrastructure.Data.Config
{
    public class ProductConfig : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Product", "dbo");

            // Primary key
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("ProductId");

            builder.Property(p => p.Name).IsRequired().HasMaxLength(100);

            builder.Property(p => p.UnitPrice)
                   .HasColumnType("decimal(18,2)")
                   .IsRequired();
        }
    }


}
