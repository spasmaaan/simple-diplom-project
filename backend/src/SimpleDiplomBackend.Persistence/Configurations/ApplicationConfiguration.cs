using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.Identity.Client;
using SimpleDiplomBackend.Domain.Entities;

namespace SimpleDiplomBackend.Persistence.Configurations
{
    public class ApplicationConfiguration : IEntityTypeConfiguration<ApplicationOption>
    {
        public void Configure(EntityTypeBuilder<ApplicationOption> builder)
        {
            builder.ToTable("application-options");

            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id)
                .HasColumnName("id")
                .ValueGeneratedOnAdd();

            builder.Property(e => e.Value)
                .HasColumnName("value")
                .IsRequired();
        }
    }
}