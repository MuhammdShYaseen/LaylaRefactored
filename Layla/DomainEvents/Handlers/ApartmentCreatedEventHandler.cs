using Layla.DomainEvents.Domain.Dispatcher;
using Layla.DomainEvents.Domain.Events;
using Layla.Helper.Localization;
using Layla.Models.DtosModels.EventDtos;
using Layla.Resources.Localization;
using Layla.Services.AuthServices.Interfaces;
using Layla.Services.EventsDataProviderServices.Interfaces;
using Layla.Services.FirebaseServices.Interfaces;
using Microsoft.Extensions.Localization;

namespace Layla.DomainEvents.Handlers
{
    public class ApartmentCreatedEventHandler : IEventHandler<ApartmentCreatedEvent>
    {

        private readonly INotificationService _notificationService;
        private readonly IStringLocalizer<Notifications> _localizer;
        private readonly IEventDataProvider<ApartmentCreatedEvent, ApartmentCreatedEventDto> _dataProvider;
        private readonly IEmailService _emailService;
        public ApartmentCreatedEventHandler(INotificationService notificationService, IStringLocalizer<Notifications> localizer,IEmailService emailService, IEventDataProvider<ApartmentCreatedEvent, ApartmentCreatedEventDto> eventDataProvider)
        {
            _notificationService = notificationService;
             _emailService = emailService;
            _localizer = localizer;
            _dataProvider = eventDataProvider;
            
        }

        public async Task HandleAsync(ApartmentCreatedEvent @event, CancellationToken ct = default)
        {
            var data = await _dataProvider.GetDataAsync(@event, ct);
            using (LocalizationHelper.UseCulture(data.OwnerLang! ?? "en"))
            {
                var title = _localizer["ApartmentCreated_Title"];
                var body = _localizer["ApartmentCreated_Body"] + " " + data.ApartmentTitle;

                await _notificationService.SendToUserAsync(data.OwnerId, title, body);
                await _emailService.SendEmailAsync(data.OwnerEmail!, title, body);
            }
        }
    }
}
