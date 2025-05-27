using MFormatik.Core.Models;
using System.Data.Entity.ModelConfiguration;

namespace MFormatik.Infrastructure.Data.Config
{
    public class ClientConfig : EntityTypeConfiguration<Client>
    {
        public ClientConfig()
        {
            ToTable("Client", "dbo");

            // Primary key
            HasKey(p => p.Id);
            Property(p => p.Id)
                .HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity)
                .HasColumnName("ClientId");

            // Required properties
            Property(p => p.FirstName)
                .HasMaxLength(80)
                .IsRequired()
                .HasColumnType("VARCHAR");

            Property(p => p.LastName)
                .HasMaxLength(60)
                .IsRequired()
                .HasColumnType("VARCHAR");

            Property(c => c.Email)
                .HasMaxLength(100);
        }
    }
}
