using Layla.Domain.Events;

namespace Layla.Application.Interfaces
{
    public interface IEventDispatcher
    {
        Task EnqueueAsync(IEvent @event, CancellationToken ct = default);
    }
}
