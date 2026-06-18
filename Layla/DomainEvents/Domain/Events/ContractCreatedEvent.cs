using System.Diagnostics.Contracts;

namespace Layla.DomainEvents.Domain.Events
{
    public class ContractCreatedEvent : IEvent
    {
        public Guid ContractGuid { get; }

        public ContractCreatedEvent(Guid contractGuid)
        {
           ContractGuid = contractGuid;
        }
    }
}
