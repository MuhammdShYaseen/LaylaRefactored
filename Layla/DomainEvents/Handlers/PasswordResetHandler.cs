using Layla.DomainEvents.Domain.Dispatcher;
using Layla.DomainEvents.Domain.Events;
using Layla.Helper.Localization;
using Layla.Models.DtosModels.EventDtos;
using Layla.Options;
using Layla.Resources.Localization;
using Layla.Services.AuthServices.Interfaces;
using Layla.Services.EventsDataProviderServices.Interfaces;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;

namespace Layla.DomainEvents.Handlers
{
    public class PasswordResetHandler : IEventHandler<PasswordResetRequestedEvent>
    {
        private readonly IEmailService _emailService;
        private readonly IStringLocalizer<Notifications> _localizer;
        private readonly FrontendOptions _frontendOptions;
        private readonly IEventDataProvider<PasswordResetRequestedEvent, PasswordResetRequestedEventDto> _dataProvider;
        public PasswordResetHandler(IEmailService emailService, IStringLocalizer<Notifications> stringLocalizer, IOptions<FrontendOptions> options, IEventDataProvider<PasswordResetRequestedEvent, PasswordResetRequestedEventDto> dataProvider)
        {
            _emailService = emailService;
            _localizer = stringLocalizer;
            _frontendOptions = options.Value;
            _dataProvider = dataProvider;
        }

        public async Task HandleAsync(PasswordResetRequestedEvent @event, CancellationToken ct = default)
        {
            var data = await _dataProvider.GetDataAsync(@event, ct);
            
            using (LocalizationHelper.UseCulture(data.Lang))
            {
                var resetUrl = $"{_frontendOptions.RestPasswordURL}{data.Token}";

                var subject = _localizer["PasswordReset_Email_Subject"];
                var body = _localizer["PasswordReset_Email_Body", "\n" + resetUrl];

                await _emailService.SendEmailAsync(data.Email, subject, body);
            }

        }
    }
}
