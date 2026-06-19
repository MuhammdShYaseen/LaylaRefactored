using Layla.Domain.Common;

namespace Layla.Domain.ValueObjects.ApartmentValueObject;

public sealed class Coordinates : ValueObject
{
    private Coordinates() { }

    private Coordinates(double latitude, double longitude)
    {
        Latitude = latitude;
        Longitude = longitude;
    }

    public double Latitude { get; private set; }
    public double Longitude { get; private set; }

    public static Coordinates Create(double latitude, double longitude)
    {
        if (latitude < -90 || latitude > 90)
            throw new ArgumentException("Invalid latitude.");

        if (longitude < -180 || longitude > 180)
            throw new ArgumentException("Invalid longitude.");

        return new Coordinates(latitude, longitude);
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Latitude;
        yield return Longitude;
    }

    public override string ToString()
        => $"{Latitude}, {Longitude}";
}