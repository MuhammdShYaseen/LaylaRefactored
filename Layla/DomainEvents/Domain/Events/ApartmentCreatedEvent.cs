
namespace Layla.DomainEvents.Domain.Events
{
    public class ApartmentCreatedEvent : IEvent
    {
        public Guid ApartmentGuid { get; }

        public ApartmentCreatedEvent(Guid apartmentGuid)
        {
            ApartmentGuid = apartmentGuid;
        }
    }
}
