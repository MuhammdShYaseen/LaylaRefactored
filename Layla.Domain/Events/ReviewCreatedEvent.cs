

using Layla.Domain.Events;

namespace Layla.Domain.Events
{
    public class ReviewCreatedEvent : IEvent
    { 
        public Guid ReviewGuid { get; }
        public ReviewCreatedEvent(Guid reviewGuid)
        {
            ReviewGuid = reviewGuid;
        }

       
    }
}
