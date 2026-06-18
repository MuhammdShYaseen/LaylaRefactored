using Google.Protobuf.WellKnownTypes;
using Layla.DomainEvents.Domain.Dispatcher;
using Layla.DomainEvents.Domain.Events;
using Layla.Helper.Localization;
using Layla.Models.MainModels;
using Layla.Options;
using Layla.Services.AuthServices.Interfaces;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;


namespace Layla.DomainEvents.Handlers
{
    public class UserEmailChangedEventHandler : IEventHandler<UserEmailChangedEvent>
    {
        private readonly IEmailService _emailService;
        private readonly FrontendOptions _frontendOptions;
        private readonly IStringLocalizer _localizer;
        public UserEmailChangedEventHandler(IEmailService emailService, IOptions<FrontendOptions> frontendOptions, IStringLocalizer stringLocalizer)
        {
            _emailService = emailService;
            _frontendOptions = frontendOptions.Value;
            _localizer = stringLocalizer;
        }
        public async Task HandleAsync(UserEmailChangedEvent @event, CancellationToken ct = default)
        {
            var verifyUrl = $"{_frontendOptions.Verify}{@event.EmailVerificationToken}";
            using (LocalizationHelper.UseCulture(@event.Language))
            {
                var subject = _localizer["UserUpdateEmailAddress_Email_Subject"];
                var body = _localizer["UserUpdateEmailAddress_Email_Body", @event.FullName, verifyUrl, @event.EmailVerificationTokenExpires?.ToString("yyyy - MM - dd HH: mm") ?? "N/A"];
                await _emailService.SendEmailAsync(@event.NewEmail, subject, body);
            }
        }
    }
}
