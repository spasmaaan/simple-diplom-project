﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SimpleDiplomBackend.Domain.Entities;

namespace SimpleDiplomBackend.Persistence.Configurations
{
    public class BookingEventConfiguration : IEntityTypeConfiguration<BookingEvent>
    {
        public void Configure(EntityTypeBuilder<BookingEvent> builder)
        {
            builder.HasKey(e => e.Id)
                .HasName("booking-events");

            builder.Property(e => e.Id)
                .HasColumnName("id")
                .ValueGeneratedOnAdd();

            builder.Property(e => e.UserId)
                .HasColumnName("userId")
                .IsRequired();

            builder.Property(e => e.BookingId)
                .HasColumnName("bookingId")
                .IsRequired();

            builder.Property(e => e.Message)
                .HasColumnName("message")
                .IsRequired();

            builder.Property(e => e.Image)
                .HasColumnName("image")
                .IsRequired();

            builder.Property(e => e.CreationDate)
                .HasColumnName("creationDate")
                .IsRequired();
        }
    }
}