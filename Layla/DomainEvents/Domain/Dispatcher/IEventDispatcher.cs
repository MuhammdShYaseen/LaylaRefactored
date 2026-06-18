using Layla.DomainEvents.Domain.Events;

namespace Layla.DomainEvents.Domain.Dispatcher
{
    public interface IEventDispatcher
    {
        Task EnqueueAsync(IEvent @event, CancellationToken ct = default);
    }
}
