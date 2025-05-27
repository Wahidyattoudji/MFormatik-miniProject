using MFormatik.Core.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace MFormatik.Infrastructure.Data.Config
{
    public class ProductConfig : EntityTypeConfiguration<Product>
    {
        public ProductConfig()
        {
            ToTable("Product", "dbo");

            // Primary key
            HasKey(p => p.Id);
            Property(p => p.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity)
                .HasColumnName("ProductId");

            Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(100);

            Property(p => p.UnitPrice)
                .HasPrecision(18, 2)
                .IsRequired();
        }
    }
}
