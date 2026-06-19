using Layla.Domain.Common;

namespace Layla.Domain.Entities
{
    public class City : Entity
    {
        public string Name { get; private set; } = null!;

        public int CountryId { get; private set; }
        public Country Country { get; private set; } = null!;

        public ICollection<Apartment> Apartments { get; private set; } = new List<Apartment>();
    }
}
