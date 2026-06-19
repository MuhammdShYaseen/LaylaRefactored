

namespace Layla.Domain.Events
{
    public class ReportCreatedEvent : IEvent
    {
        public Guid ReportGuid { get; }
        public ReportCreatedEvent(Guid reportGuid)
        {
            ReportGuid = reportGuid;
        }
    }
}
