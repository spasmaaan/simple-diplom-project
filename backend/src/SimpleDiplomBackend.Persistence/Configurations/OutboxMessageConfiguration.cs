using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SimpleDiplomBackend.Domain.Entities;

namespace SimpleDiplomBackend.Persistence.Configurations
{
    //public class OutboxMessageConfiguration : IEntityTypeConfiguration<OutboxMessage>
    //{
    //    public void Configure(EntityTypeBuilder<OutboxMessage> builder)
    //    {
    //        builder.ToTable("outbox-message");

    //        builder.HasKey(e => e.Id)
    //            .IsClustered(false);

    //        builder.Property(e => e.Id)
    //            .HasColumnName("id")
    //            // .ValueGeneratedOnAdd()
    //            .ValueGeneratedNever();

    //        builder.Property(e => e.Type)
    //            .HasColumnName("type")
    //            .HasMaxLength(255)
    //            .IsRequired();

    //        builder.Property(e => e.Payload)
    //            .HasColumnName("payload")
    //            .HasMaxLength(1000)
    //            .IsRequired();

    //        builder.Property(e => e.CreatedAt)
    //            .HasColumnName("createdAt")
    //            .IsRequired();

    //        builder.Property(e => e.ProcessedAt)
    //            .HasColumnName("processedAt");

    //        builder.Property(e => e.Error)
    //            .HasMaxLength(500)
    //            .HasColumnName("error");
    //    }
    //}
}