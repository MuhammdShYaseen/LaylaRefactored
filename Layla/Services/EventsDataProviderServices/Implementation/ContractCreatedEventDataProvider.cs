using Layla.DataRepository;
using Layla.DomainEvents.Domain.Events;
using Layla.Models.DtosModels.EventDtos;
using Layla.Models.MainModels;
using Layla.Services.EventsDataProviderServices.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Layla.Services.EventsDataProviderServices.Implementation
{
    public class ContractCreatedEventDataProvider : IEventDataProvider<ContractCreatedEvent, ContractCreatedEventDto>
    {
        private readonly IRepository<Contract> _contractRepository;

        public ContractCreatedEventDataProvider(IRepository<Contract> contractRepository)
        {
            _contractRepository = contractRepository;
        }
        public async Task<ContractCreatedEventDto> GetDataAsync(ContractCreatedEvent @event, CancellationToken ct)
        {
            return await _contractRepository
            .Query()
            .AsNoTracking()
            .Where(c => c.Guid == @event.ContractGuid)
            .Select(c => new ContractCreatedEventDto
            {
                ContractId = c.Guid,
                BookingId = c.Booking!.Id,

                ApartmentTitle = c.Booking.Apartment!.Title,

                OwnerId = c.Booking.Apartment.OwnerId,
                OwnerEmail = c.Booking.Apartment.Owner!.Email!.Value,
                OwnerLang = c.Booking.Apartment.Owner.Lang!.Code,

                RenterId = c.Booking.UserId,
                RenterEmail = c.Booking.User!.Email!.Value,
                RenterLang = c.Booking.User.Lang!.Code
            })
            .SingleAsync(ct);
        }
    }
    
}
