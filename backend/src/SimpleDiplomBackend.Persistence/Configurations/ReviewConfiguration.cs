using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SimpleDiplomBackend.Domain.Entities;

namespace SimpleDiplomBackend.Persistence.Configurations
{
    public class ReviewConfiguration : IEntityTypeConfiguration<Review>
    {
        public void Configure(EntityTypeBuilder<Review> builder)
        {
            builder.ToTable("reviews");

            builder.HasKey(e => e.Id);

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
                .HasColumnName("rating");

            builder.Property(e => e.CreationDate)
                .HasColumnName("creationDate")
                .IsRequired();
        }
    }
}