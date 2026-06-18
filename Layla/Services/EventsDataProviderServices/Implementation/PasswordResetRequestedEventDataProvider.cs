using Layla.DataRepository;
using Layla.DomainEvents.Domain.Events;
using Layla.Models.DtosModels.EventDtos;
using Layla.Models.MainModels;
using Layla.Services.EventsDataProviderServices.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Layla.Services.EventsDataProviderServices.Implementation
{
    public class PasswordResetRequestedEventDataProvider : IEventDataProvider<PasswordResetRequestedEvent, PasswordResetRequestedEventDto>
    {
        private readonly IRepository<User> _users;

        public PasswordResetRequestedEventDataProvider(IRepository<User> users)
        {
            _users = users;
        }
        public async Task<PasswordResetRequestedEventDto> GetDataAsync(PasswordResetRequestedEvent @event, CancellationToken ct)
        {
            return await _users.Query()
            .AsNoTracking()
            .Where(u => u.Guid == @event.UserGuid)
            .Select(u => new PasswordResetRequestedEventDto
            {
                UserId = u.Id,
                Email = u.Email!.Value,
                Lang = u.Lang!.Code,
                Token = @event.Token
            })
            .SingleAsync(ct);
        }
    }
}
