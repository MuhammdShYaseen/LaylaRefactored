using static Layla.Models.MainModels.Booking;

namespace Layla.DomainEvents.Domain.Events
{
    public class BookingStatusChangedEvent : IEvent
    {
        public Guid BookingGuid { get; }
        public BookingStatus NewStatus { get; }

        public BookingStatusChangedEvent(Guid bookingGuid, BookingStatus newStatus)
        {
            BookingGuid = bookingGuid;
            NewStatus = newStatus;
        }
    }
}
