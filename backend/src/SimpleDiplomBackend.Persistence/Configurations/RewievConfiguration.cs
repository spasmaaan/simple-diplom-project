using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SimpleDiplomBackend.Domain.Entities;

namespace SimpleDiplomBackend.Persistence.Configurations
{
    public class RewievConfiguration : IEntityTypeConfiguration<Review>
    {
        public void Configure(EntityTypeBuilder<Review> builder)
        {
            builder.HasKey(e => e.Id)
                .HasName("reviews");

            builder.Property(e => e.Id)
                .HasColumnName("id")
                .ValueGeneratedOnAdd();

            builder.Property(e => e.UserId)
                .HasColumnName("userId")
                .IsRequired();

            builder.Property(e => e.Message)
                .HasColumnName("message")
                .HasMaxLength(500)
                .IsRequired();

            builder.Property(e => e.Rating)
                .HasColumnName("rating")
                .IsRequired();

            builder.Property(e => e.CreationDate)
                .HasColumnName("creationDate")
                .IsRequired();
        }
    }
}