using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SimpleDiplomBackend.Domain.Entities;

namespace SimpleDiplomBackend.Persistence.Configurations
{
    public class BookingServiceConfiguration : IEntityTypeConfiguration<BookingService>
    {
        public void Configure(EntityTypeBuilder<BookingService> builder)
        {
            builder.ToTable("booking-services");

            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id)
                .HasColumnName("id")
                .ValueGeneratedOnAdd();

            builder.Property(e => e.BookingId)
                .HasColumnName("bookingId")
                .IsRequired();

            builder.Property(e => e.ServiceId)
                .HasColumnName("serviceId")
                .IsRequired();

            builder.Property(e => e.Count)
                .HasColumnName("count")
                .IsRequired();
        }
    }
}