using Layla.DataRepository;
using Layla.DomainEvents.Domain.Events;
using Layla.Models.DtosModels.EventDtos;
using Layla.Models.MainModels;
using Layla.Options;
using Layla.Services.EventsDataProviderServices.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Layla.Services.EventsDataProviderServices.Implementation
{
    public class UserRegisteredEventDataProvider : IEventDataProvider<UserRegisteredEvent, UserRegisteredEventDto>
    {
        private readonly IRepository<User> _users;
        private readonly FrontendOptions _frontendOptions;

        public UserRegisteredEventDataProvider(
            IRepository<User> users,
            IOptions<FrontendOptions> frontendOptions)
        {
            _users = users;
            _frontendOptions = frontendOptions.Value;
        }

        public async Task<UserRegisteredEventDto> GetDataAsync(
            UserRegisteredEvent @event,
            CancellationToken ct)
        {
            return await _users.Query()
                .AsNoTracking()
                .Where(u => u.Guid == @event.UserGuid)
                .Select(u => new UserRegisteredEventDto
                {
                    FullName = u.FullName,
                    Email = u.Email!.Value,
                    Lang = u.Lang!.Code,
                    VerificationUrl =
                        $"{_frontendOptions.Verify}{@event.Token}"
                })
                .SingleAsync(ct);
        }
    }
}
