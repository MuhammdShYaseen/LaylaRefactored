using Layla.DomainEvents.Domain.Events;

namespace Layla.Services.EventsDataProviderServices.Interfaces
{
    public interface IEventDataProvider<in TEvent, TData> where TEvent : IEvent
    {
        Task<TData> GetDataAsync(TEvent @event, CancellationToken ct);
    }
}
