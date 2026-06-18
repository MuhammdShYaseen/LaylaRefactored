using Layla.DataRepository;
using Layla.DomainEvents.Domain.Events;
using Layla.Models.DtosModels.EventDtos;
using Layla.Models.MainModels;
using Layla.Services.EventsDataProviderServices.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Layla.Services.EventsDataProviderServices.Implementation
{
    public class ReviewCreatedEventDataProvider : IEventDataProvider<ReviewCreatedEvent, ReviewCreatedEventDto>
    {
        private readonly IRepository<Review> _reviews;

        public ReviewCreatedEventDataProvider(IRepository<Review> reviews)
        {
            _reviews = reviews;
        }

        public async Task<ReviewCreatedEventDto> GetDataAsync(ReviewCreatedEvent @event, CancellationToken ct)
        {
            return await _reviews.Query()
                .AsNoTracking()
                .Where(r => r.Guid == @event.ReviewGuid)
                .Select(r => new ReviewCreatedEventDto
                {
                    Rating = r.Rating,
                    OwnerId = r.Apartment!.OwnerId,
                    OwnerLang = r.Apartment!.Owner!.Lang!.Code
                })
                .SingleAsync(ct);
        }
    }
}
