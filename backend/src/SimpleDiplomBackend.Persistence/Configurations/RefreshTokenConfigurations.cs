
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SimpleDiplomBackend.Domain.Entities;

namespace SimpleDiplomBackend.Persistence.Configurations
{
    public class RefreshTokenConfigurations : IEntityTypeConfiguration<RefreshToken>
    {
        public void Configure(EntityTypeBuilder<RefreshToken> builder)
        {
            builder.ToTable("refresh-tokens");

            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id)
                .HasColumnName("id")
                .ValueGeneratedOnAdd();

            builder.Property(e => e.UserId)
                .HasColumnName("userId")
                .HasMaxLength(128)
                .IsRequired();

            builder.HasIndex(e => e.JwtId);
            builder.Property(e => e.JwtId)
                .HasColumnName("jwtId")
                .HasMaxLength(128)
                .IsRequired();

            builder.Property(e => e.Token)
                .HasColumnName("token")
                .HasMaxLength(128)
                .IsRequired();

            builder.Property(e => e.CreationDate)
                .HasColumnName("creationDate")
                .IsRequired();

            builder.Property(e => e.ExpirationDate)
                .HasColumnName("expirationDate")
                .IsRequired();

            builder.Property(e => e.Revoked)
                .HasColumnName("revoked");
        }
    }
}