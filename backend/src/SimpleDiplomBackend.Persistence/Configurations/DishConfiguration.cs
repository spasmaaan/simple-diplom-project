using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SimpleDiplomBackend.Domain.Entities;

namespace SimpleDiplomBackend.Persistence.Configurations
{
    public class DishConfiguration : IEntityTypeConfiguration<Dish>
    {
        public void Configure(EntityTypeBuilder<Dish> builder)
        {
            builder.ToTable("dishes");

            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id)
                .HasColumnName("id")
                .ValueGeneratedOnAdd();

            builder.Property(e => e.Name)
                .HasColumnName("name")
                .HasMaxLength(100)
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

            //builder.Property(e => e.Catergory)
            //    .HasColumnName("categoryId")
            //    .IsRequired();
        }
    }
}