using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SimpleDiplomBackend.Domain.Entities;

namespace SimpleDiplomBackend.Persistence.Configurations
{
    public class BookingDishConfiguration : IEntityTypeConfiguration<BookingDish>
    {
        public void Configure(EntityTypeBuilder<BookingDish> builder)
        {
            builder.ToTable("booking-dishes");

            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id)
                .HasColumnName("id")
                .ValueGeneratedOnAdd();

            builder.Property(e => e.BookingId)
                .HasColumnName("bookingId")
                .IsRequired();

            builder.Property(e => e.DishId)
                .HasColumnName("dishId")
                .IsRequired();

            builder.Property(e => e.Count)
                .HasColumnName("count")
                .IsRequired();
        }
    }
}