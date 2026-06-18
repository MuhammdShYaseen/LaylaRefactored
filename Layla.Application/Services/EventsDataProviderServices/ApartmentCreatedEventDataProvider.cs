using Layla.DataRepository;
using Layla.DomainEvents.Domain.Events;
using Layla.Models.DtosModels.EventDtos;
using Layla.Models.MainModels;
using Layla.Services.EventsDataProviderServices.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Layla.Application.Services.EventsDataProviderServices
{
    public class ApartmentCreatedEventDataProvider : IEventDataProvider<ApartmentCreatedEvent, ApartmentCreatedEventDto>
    {
        private readonly IRepository<Apartment> _repository;
        public ApartmentCreatedEventDataProvider(IRepository<Apartment> repository)
        {
            _repository = repository;
        }
        public async Task<ApartmentCreatedEventDto> GetDataAsync(ApartmentCreatedEvent @event, CancellationToken ct)
        {
            return await _repository.Query()
                .AsNoTracking()
                .Where(a => a.Guid == @event.ApartmentGuid)
                .Select(a => new ApartmentCreatedEventDto
                {
                    ApartmentId = a.Guid,
                    ApartmentTitle = a.Title,
                    OwnerId = a.OwnerId,
                    OwnerEmail = a.Owner!.Email!.Value,
                    OwnerLang = a.Owner.Lang!.Code
                })
                .SingleAsync(ct);
        }
    }
}
