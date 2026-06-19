using Layla.Domain.Entities;
using Layla.Domain.ValueObjects.ApartmentValueObject;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NetTopologySuite;
using NetTopologySuite.Geometries;

namespace Layla.Infrastructure.DataAccess.Configurations;

public class ApartmentConfiguration : IEntityTypeConfiguration<Apartment>
{
    public void Configure(EntityTypeBuilder<Apartment> builder)
    {

        // ── Base Entity ───────────────────────────────────────────────────────
        builder.HasKey(a => a.Id);
        builder.Property(a => a.Guid).IsRequired();
        builder.Property(a => a.CreatedAt).IsRequired();
        builder.Property(a => a.UpdatedAt);
        builder.Property(a => a.IsDeleted).HasDefaultValue(false);

        // ── Scalar properties ─────────────────────────────────────────────────
        builder.Property(a => a.Title).IsRequired().HasMaxLength(200);
        builder.Property(a => a.Description).HasMaxLength(1000);
        builder.Property(a => a.Orientation).HasMaxLength(100);
        builder.Property(a => a.Area).IsRequired();
        builder.Property(a => a.FloorNumber);
        builder.Property(a => a.NumberOfBedRooms);
        builder.Property(a => a.NumberOfLivingRooms);
        builder.Property(a => a.NumberOfReceptionRooms);
        builder.Property(a => a.NumberOfBathrooms);
        builder.Property(a => a.NumberOfBalconies);
        builder.Property(a => a.HasElevator);
        builder.Property(a => a.HasParking);
        builder.Property(a => a.HasPool);
        builder.Property(a => a.IsAvailable).HasDefaultValue(true);
        builder.Property(a => a.IsChatEnabled).HasDefaultValue(true);
        builder.Property(a => a.OwnerId);
        builder.Property(a => a.CityId);

        // ── Enums as strings ──────────────────────────────────────────────────
        builder.Property(a => a.Type).HasConversion<string>().HasMaxLength(50);
        builder.Property(a => a.View).HasConversion<string>().HasMaxLength(50);
        builder.Property(a => a.Finishing).HasConversion<string>().HasMaxLength(50);

        // ── Money ─────────────────────────────────────────────────────────────
        builder.OwnsOne<Money>(
            navigationName: "PricePerHour",
            buildAction: money =>
            {
                money.WithOwner();
                money.Property<decimal>("Value")
                    .HasColumnName("PricePerHour")
                    .HasColumnType("decimal(18,4)")
                    .IsRequired();
            });

        builder.OwnsOne<Money>(
            navigationName: "PricePerDay",
            buildAction: money =>
            {
                money.WithOwner();
                money.Property<decimal>("Value")
                    .HasColumnName("PricePerDay")
                    .HasColumnType("decimal(18,4)")
                    .IsRequired();
            });

        // ── GeoLocation ───────────────────────────────────────────────────────
        builder.OwnsOne<GeoLocation>(
            navigationName: "Location",
            buildAction: geo =>
            {
                geo.WithOwner();

                geo.Property<string>("Street")
                    .HasColumnName("Street")
                    .HasMaxLength(200)
                    .IsRequired();

                geo.Property<string>("BuildingNumber")
                    .HasColumnName("BuildingNumber")
                    .HasMaxLength(50)
                    .IsRequired();

                geo.Property<string>("ApartmentNumber")
                    .HasColumnName("ApartmentNumber")
                    .HasMaxLength(50)
                    .IsRequired();

                geo.Property<string>("City")
                    .HasColumnName("LocationCity")
                    .HasMaxLength(100)
                    .IsRequired();

                geo.Property<string>("District")
                    .HasColumnName("District")
                    .HasMaxLength(100)
                    .IsRequired();

                geo.Property<string>("Country")
                    .HasColumnName("Country")
                    .HasMaxLength(100)
                    .IsRequired();

                // Convert Coordinates ↔ Point without touching domain
                var geometryFactory = NtsGeometryServices.Instance.CreateGeometryFactory(srid: 4326);

                geo.Property<Point>("CoordinatesPoint")
                    .HasColumnName("Coordinates")
                    .HasColumnType("geography(Point,4326)")
                    .IsRequired()
                    .HasConversion(
                        coords => geometryFactory.CreatePoint(
                            new Coordinate(coords.X, coords.Y)),   // Point → Point (already a Point)
                        point => point);                          // no-op on read

                geo.Ignore(g => g.Coordinates);
            });

        // ── Soft delete filter ────────────────────────────────────────────────
        builder.HasQueryFilter(a => !a.IsDeleted);

        // ── Relationships ─────────────────────────────────────────────────────
        builder.HasOne(a => a.Owner)
            .WithMany()
            .HasForeignKey(a => a.OwnerId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(a => a.City)
            .WithMany()
            .HasForeignKey(a => a.CityId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(a => a.Bookings)
            .WithOne()
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(a => a.Reviews)
            .WithOne()
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(a => a.MediaFiles)
            .WithOne()
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(a => a.Conversations)
            .WithOne()
            .OnDelete(DeleteBehavior.Cascade);
    }
}