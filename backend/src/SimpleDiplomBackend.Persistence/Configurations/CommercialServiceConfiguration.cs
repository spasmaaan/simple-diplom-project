using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SimpleDiplomBackend.Domain.Entities;

namespace SimpleDiplomBackend.Persistence.Configurations
{
    public class CommercialServiceConfiguration : IEntityTypeConfiguration<CommercialService>
    {
        public void Configure(EntityTypeBuilder<CommercialService> builder)
        {
            builder.ToTable("commercial-services");

            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id)
                .HasColumnName("id")
                .ValueGeneratedOnAdd();

            builder.Property(e => e.Name)
                .HasColumnName("name")
                .HasMaxLength(300)
                .IsRequired();

            builder.Property(e => e.Description)
                .HasColumnName("description")
                .HasMaxLength(500)
                .IsRequired();

            builder.Property(e => e.PreviewImage)
                .HasColumnName("previewImage");

            builder.Property(e => e.Price)
                .HasColumnName("price")
                .IsRequired();
        }
    }
}