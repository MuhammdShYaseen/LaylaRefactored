using Layla.DomainEvents.Domain.Dispatcher;
using Layla.DomainEvents.Domain.Events;
using Layla.Helper.Localization;
using Layla.Models.DtosModels.EventDtos;
using Layla.Services.AuthServices.Interfaces;
using Layla.Services.EventsDataProviderServices.Interfaces;
using Microsoft.Extensions.Localization;



namespace Layla.DomainEvents.Handlers
{
    public class UserRegisteredHandler : IEventHandler<UserRegisteredEvent>
    {
        private readonly IEmailService _emailService;
        private readonly IStringLocalizer _localizer;
        private readonly IEventDataProvider<UserRegisteredEvent, UserRegisteredEventDto> _dataProvider;

        public UserRegisteredHandler(IEmailService emailService, IStringLocalizer stringLocalizer, IEventDataProvider<UserRegisteredEvent, UserRegisteredEventDto> dataProvider)
        {
            _emailService = emailService;
            _localizer = stringLocalizer;
            _dataProvider = dataProvider;
        }

        public async Task HandleAsync(UserRegisteredEvent @event, CancellationToken ct = default)
        {
            var data = await _dataProvider.GetDataAsync(@event, ct);
            using (LocalizationHelper.UseCulture(data.Lang))
            {
                var subject = _localizer["UserRegistered_Email_Subject"];

                var body = _localizer["UserRegistered_Email_Body", data.FullName, data.VerificationUrl];

                await _emailService.SendEmailAsync(data.Email, subject, body);
            }
        }
    }
}
