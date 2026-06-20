using Layla.Domain.Common;
using NetTopologySuite;
using NetTopologySuite.Geometries;
namespace Layla.Domain.ValueObjects.ApartmentValueObject;

public sealed class GeoLocation : ValueObject
{
    
    private GeoLocation() { }

    private GeoLocation(
        string street,
        string buildingNumber,
        string apartmentNumber,
        string city,
        string district,
        string country,
        Coordinates coordinates)
    {
        var geometryFactory = NtsGeometryServices.Instance.CreateGeometryFactory(srid: 4326);
        Street = street;
        BuildingNumber = buildingNumber;
        ApartmentNumber = apartmentNumber;
        City = city;
        District = district;
        Country = country;
        Coordinates = geometryFactory.CreatePoint( new Coordinate(coordinates.Longitude, coordinates.Latitude));
    }

    public string Street { get; private set; } = null!;
    public string BuildingNumber { get; private set; } = null!;
    public string ApartmentNumber { get; private set; } = null!;
    public string City { get; private set; } = null!;
    public string District { get; private set; } = null!;
    public string Country { get; private set; } = null!;

    public Point Coordinates { get; private set; } = null!;

    public static GeoLocation Create(string street,  string buildingNumber,  string apartmentNumber, string city, string district,  string country, Coordinates coordinates)
    {
        Validate(street, buildingNumber, apartmentNumber, city, district, country);
        return new GeoLocation(
            street.Trim(),
            buildingNumber.Trim(),
            apartmentNumber.Trim(),
            city.Trim(),
            district.Trim(),
            country.Trim(),
            coordinates);
    }

    private static void Validate(
        string street,
        string buildingNumber,
        string apartmentNumber,
        string city,
        string district,
        string country)
    {
        if (string.IsNullOrWhiteSpace(street))
            throw new ArgumentException("Street is required.");

        if (string.IsNullOrWhiteSpace(buildingNumber))
            throw new ArgumentException("Building number is required.");

        if (string.IsNullOrWhiteSpace(apartmentNumber))
            throw new ArgumentException("Apartment number is required.");

        if (string.IsNullOrWhiteSpace(city))
            throw new ArgumentException("City is required.");

        if (string.IsNullOrWhiteSpace(district))
            throw new ArgumentException("District is required.");

        if (string.IsNullOrWhiteSpace(country))
            throw new ArgumentException("Country is required.");
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Street.ToUpperInvariant();
        yield return BuildingNumber.ToUpperInvariant();
        yield return ApartmentNumber.ToUpperInvariant();
        yield return City.ToUpperInvariant();
        yield return District.ToUpperInvariant();
        yield return Country.ToUpperInvariant();
        yield return Coordinates;
    }

    public override string ToString()
    {
        return $"{ApartmentNumber}, {BuildingNumber}, {Street}, {District}, {City}, {Country}";
    }
}