using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SimpleDiplomBackend.Domain.Entities;

namespace SimpleDiplomBackend.Persistence.Configurations
{
    public class DishCategoryConfiguration : IEntityTypeConfiguration<DishCategory>
    {
        public void Configure(EntityTypeBuilder<DishCategory> builder)
        {
            builder.ToTable("dish-categories");

            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id)
                .HasColumnName("id")
                .ValueGeneratedOnAdd();

            builder.Property(e => e.Name)
                .HasColumnName("name")
                .HasMaxLength(150)
                .IsRequired();

            builder.Property(e => e.Description)
                .HasColumnName("description")
                .HasMaxLength(300)
                .IsRequired();

            builder.Property(e => e.PreviewImage)
                .HasColumnName("previewImage");
        }
    }
}