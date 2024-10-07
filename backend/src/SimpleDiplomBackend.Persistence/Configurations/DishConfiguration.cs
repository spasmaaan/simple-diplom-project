using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SimpleDiplomBackend.Domain.Entities;

namespace SimpleDiplomBackend.Persistence.Configurations
{
    public class DishConfiguration : IEntityTypeConfiguration<Dish>
    {
        public void Configure(EntityTypeBuilder<Dish> builder)
        {
            builder.HasKey(e => e.Id)
                .HasName("dishes");

            builder.Property(e => e.Id)
                .HasColumnName("id")
                .ValueGeneratedOnAdd();

            builder.Property(e => e.Name)
                .HasColumnType("name")
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(e => e.Description)
                .HasColumnType("description")
                .HasMaxLength(500)
                .IsRequired();

            builder.Property(e => e.PreviewImage)
                .HasColumnType("previewImage")
                .IsRequired();

            builder.Property(e => e.Price)
                .HasColumnType("price")
                .IsRequired();

            //builder.Property(e => e.Catergory)
            //    .HasColumnType("categoryId")
            //    .IsRequired();
        }
    }
}