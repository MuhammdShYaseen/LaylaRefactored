using Layla.DomainEvents.Domain.Events;

namespace Layla.DomainEvents.Domain.Dispatcher
{
    public interface IEventHandler<in TEvent> where TEvent : IEvent
    {
        Task HandleAsync(TEvent @event, CancellationToken ct = default);
    }
}
