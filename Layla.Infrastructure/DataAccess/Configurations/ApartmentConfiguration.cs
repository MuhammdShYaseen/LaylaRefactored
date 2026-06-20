using Layla.Domain.Entities;
using Layla.Domain.ValueObjects.ApartmentValueObject;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NetTopologySuite;

namespace Layla.Infrastructure.DataAccess.Configurations;

public sealed class ApartmentConfiguration : IEntityTypeConfiguration<Apartment>
{
    public void Configure(EntityTypeBuilder<Apartment> builder)
    {
        builder.ToTable("Apartments");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Guid)
            .IsRequired();

        builder.Property(x => x.CreatedAt)
            .IsRequired();

        builder.Property(x => x.IsDeleted)
            .HasDefaultValue(false);

        builder.HasQueryFilter(x => !x.IsDeleted);

        // Basic fields

        builder.Property(x => x.Title)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(x => x.Description)
            .HasMaxLength(1000);

        builder.Property(x => x.Area)
            .IsRequired();

        builder.Property(x => x.Orientation)
            .HasMaxLength(100);

        builder.Property(x => x.IsAvailable)
            .HasDefaultValue(true);

        builder.Property(x => x.IsChatEnabled)
            .HasDefaultValue(true);

        // Enums

        builder.Property(x => x.Type)
            .HasConversion<string>()
            .HasMaxLength(50);

        builder.Property(x => x.View)
            .HasConversion<string>()
            .HasMaxLength(50);

        builder.Property(x => x.Finishing)
            .HasConversion<string>()
            .HasMaxLength(50);

        // Money

        builder.OwnsOne(x => x.PricePerHour, money =>
        {
            money.Property(x => x.Value)
                .HasColumnName("PricePerHour")
                .HasColumnType("decimal(18,4)")
                .IsRequired();
        });

        builder.OwnsOne(x => x.PricePerDay, money =>
        {
            money.Property(x => x.Value)
                .HasColumnName("PricePerDay")
                .HasColumnType("decimal(18,4)")
                .IsRequired();
        });

        // Location

        builder.OwnsOne(x => x.Location, geo =>
        {
            geo.Property(x => x.Street)
                .HasColumnName("Street")
                .HasMaxLength(200)
                .IsRequired();

            geo.Property(x => x.BuildingNumber)
                .HasColumnName("BuildingNumber")
                .HasMaxLength(50)
                .IsRequired();

            geo.Property(x => x.ApartmentNumber)
                .HasColumnName("ApartmentNumber")
                .HasMaxLength(50)
                .IsRequired();

            geo.Property(x => x.City)
                .HasColumnName("LocationCity")
                .HasMaxLength(100)
                .IsRequired();

            geo.Property(x => x.District)
                .HasColumnName("District")
                .HasMaxLength(100)
                .IsRequired();

            geo.Property(x => x.Country)
                .HasColumnName("Country")
                .HasMaxLength(100)
                .IsRequired();

            geo.Property(x => x.Coordinates)
                .HasColumnName("Coordinates")
                .HasColumnType("geography")
                .IsRequired();

            
        });

        builder.Navigation(x => x.Location)
            .IsRequired();

        // Relationships

        builder.HasOne(x => x.Owner)
            .WithMany()
            .HasForeignKey(x => x.OwnerId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.City)
            .WithMany(x => x.Apartments)
            .HasForeignKey(x => x.CityId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(x => x.Bookings)
            .WithOne()
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(x => x.Reviews)
            .WithOne()
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(x => x.MediaFiles)
            .WithOne()
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(x => x.Conversations)
            .WithOne()
            .OnDelete(DeleteBehavior.Cascade);
    }
}