using Layla.DomainEvents.Domain.Dispatcher;
using Layla.DomainEvents.Domain.Events;


namespace Layla.Test.Services.MokeDbContext
{
    public class FakeEventDispatcher : IEventDispatcher
    {
        public Task EnqueueAsync(IEvent @event, CancellationToken ct = default)
        {
            return Task.CompletedTask;
        }
    } 
}
