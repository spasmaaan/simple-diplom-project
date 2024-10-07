using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SimpleDiplomBackend.Persistence.Configurations
{
    public class BookingConfiguration : IEntityTypeConfiguration<Domain.Entities.Booking>
    {
        public void Configure(EntityTypeBuilder<Domain.Entities.Booking> builder)
        {
            builder.HasKey(e => e.Id)
                .HasName("booking");

            builder.Property(e => e.Id)
                .HasColumnName("id")
                .ValueGeneratedOnAdd();

            builder.Property(e => e.UserId)
                .HasColumnName("userId")
                .IsRequired();

            builder.Property(e => e.CreationDate)
                .HasColumnName("creationDate")
                .IsRequired();

            builder.Property(e => e.StartDate)
                .HasColumnName("startDate")
                .IsRequired();

            builder.Property(e => e.EndDate)
                .HasColumnName("endDate")
                .IsRequired();

            builder.Property(e => e.StatusId)
                .HasColumnName("statusId")
                .IsRequired();
        }
    }
}