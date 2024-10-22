using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SimpleDiplomBackend.Domain.Entities;

namespace SimpleDiplomBackend.Persistence.Configurations
{
    public class BookingStatusConfiguration : IEntityTypeConfiguration<BookingStatus>
    {
        public void Configure(EntityTypeBuilder<BookingStatus> builder)
        {
            builder.ToTable("booking-statuses");

            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id)
                .HasColumnName("id")
                .ValueGeneratedOnAdd();

            builder.Property(e => e.Name)
                .HasColumnName("name")
                .HasMaxLength(300)
                .IsRequired();
        }
    }
}