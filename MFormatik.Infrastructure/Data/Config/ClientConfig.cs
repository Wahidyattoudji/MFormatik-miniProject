using MFormatik.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MFormatik.Infrastructure.Data.Config
{
    public class ClientConfig : IEntityTypeConfiguration<Client>
    {
        public void Configure(EntityTypeBuilder<Client> builder)
        {
            builder.ToTable("Client", "dbo");

            // Primary key
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("ClientId");

            // Required properties
            builder.Property(p => p.FirstName).HasMaxLength(80).HasColumnType("VARCHAR").IsRequired();
            builder.Property(p => p.LastName).HasMaxLength(60).HasColumnType("VARCHAR").IsRequired();

            builder.Property(c => c.Email).HasMaxLength(100);


            // Additional configurations
            builder.Property(p => p.Email)
                .HasConversion(
                    v => v.ToLowerInvariant(), // Normalize emails to lowercase
                    v => v);
        }
    }
}
