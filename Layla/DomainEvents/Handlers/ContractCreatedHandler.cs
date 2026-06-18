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
    public class ContractCreatedHandler : IEventHandler<ContractCreatedEvent>
    {
        private readonly INotificationService _notificationService;
        private readonly IEmailService _emailService;
        private readonly IStringLocalizer<Notifications> _localizer;
        private readonly IEventDataProvider<ContractCreatedEvent, ContractCreatedEventDto> _dataProvider;
        public ContractCreatedHandler(INotificationService notificationService, IEmailService emailService,IStringLocalizer<Notifications> notificationLocalizer, IEventDataProvider<ContractCreatedEvent, ContractCreatedEventDto> dataProvider)
        {
            _notificationService = notificationService;
            _emailService = emailService;
            _localizer = notificationLocalizer;
            _dataProvider = dataProvider;
        }

        public async Task HandleAsync(ContractCreatedEvent @event, CancellationToken ct = default)
        {
            var data =await _dataProvider.GetDataAsync(@event, ct);

            // 🔔 إشعار + إيميل المالك
            using (LocalizationHelper.UseCulture(data.OwnerLang))
            {
                var title = _localizer["ContractCreated_Owner_Title"];
                var body = _localizer[ "ContractCreated_Owner_Body", data.BookingId, data.ApartmentTitle];

                await _notificationService.SendToUserAsync(data.OwnerId, title, body);

                var emailSubject = _localizer["ContractCreated_Owner_Title"];
                var emailBody = _localizer["ContractCreated_Owner_Body", data.BookingId, data.ApartmentTitle];

                await _emailService.SendEmailAsync(data.OwnerEmail, emailSubject, emailBody);
            }

            // 🔔 إشعار + إيميل المستأجر
            using (LocalizationHelper.UseCulture(data.RenterLang))
            {
                var title = _localizer["ContractCreated_Renter_Title"];
                var body = _localizer["ContractCreated_Renter_Body", data.ApartmentTitle];

                await _notificationService.SendToUserAsync(data.RenterId, title, body);

                var emailSubject = _localizer["ContractCreated_Renter_Title"];
                var emailBody = _localizer["ContractCreated_Renter_Body", data.ApartmentTitle];

                await _emailService.SendEmailAsync(data.RenterEmail, emailSubject, emailBody);
            }
        }
    }
}
