using Layla.DataRepository;
using Layla.DomainEvents.Domain.Events;
using Layla.Models.DtosModels.EventDtos;
using Layla.Models.MainModels;
using Layla.Services.EventsDataProviderServices.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Layla.Services.EventsDataProviderServices.Implementation
{
    public class BookingCreatedEventDataProvider : IEventDataProvider<BookingCreatedEvent, BookingCreatedEventDto>
    {
        private readonly IRepository<Booking> _repository;

        public BookingCreatedEventDataProvider(IRepository<Booking> repository)
        {
            _repository = repository;
        }
        public async Task<BookingCreatedEventDto> GetDataAsync(BookingCreatedEvent @event, CancellationToken ct)
        {
            return await _repository.Query()
            .AsNoTracking()
            .Where(b => b.Guid == @event.BookingGuid)
            .Select(b => new BookingCreatedEventDto
            {
                ApartmentTitle = b.Apartment!.Title,

                StartDate = b.StartDate,
                EndDate = b.EndDate,

                OwnerId = b.Apartment.OwnerId,
                OwnerEmail = b.Apartment.Owner!.Email!.Value,
                OwnerLanguage = b.Apartment.Owner.Lang!.Code,

                RenterId = b.UserId,
                RenterEmail = b.User!.Email!.Value,
                RenterName = b.User.FullName,
                RenterLanguage = b.User.Lang!.Code
            })
            .SingleAsync(ct);
        }
    }
}
