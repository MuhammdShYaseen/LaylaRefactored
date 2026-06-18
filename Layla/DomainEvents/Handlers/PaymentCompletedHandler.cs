using Layla.DomainEvents.Domain.Dispatcher;
using Layla.DomainEvents.Domain.Events;

namespace Layla.DomainEvents.Handlers
{
    public class PaymentCompletedHandler : IEventHandler<PaymentCompletedEvent>
    {
        public Task HandleAsync(PaymentCompletedEvent @event, CancellationToken ct = default)
        {
            throw new NotImplementedException();
        }
    }
}
