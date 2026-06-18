using Layla.DataAccess;
using Layla.DomainEvents.Domain.Dispatcher;
using Layla.DomainEvents.Domain.Events;
using Layla.Helper.Localization;
using Layla.Models.DtosModels.EventDtos;
using Layla.Resources.Localization;
using Layla.Services.EventsDataProviderServices.Interfaces;
using Layla.Services.FirebaseServices.Interfaces;
using Microsoft.Extensions.Localization;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Layla.DomainEvents.Handlers
{
    public class ReviewCreatedHandler : IEventHandler<ReviewCreatedEvent>
    {

        private readonly INotificationService _notificationService;
        private readonly IStringLocalizer<Notifications> _localizer;
        private readonly IEventDataProvider<ReviewCreatedEvent, ReviewCreatedEventDto> _dataProvider;
        public ReviewCreatedHandler(INotificationService notificationService, IStringLocalizer<Notifications> localizer, IEventDataProvider<ReviewCreatedEvent, ReviewCreatedEventDto> dataProvider)
        {

            _notificationService = notificationService;
            _localizer = localizer;
            _dataProvider = dataProvider;

        }

        public async Task HandleAsync(ReviewCreatedEvent @event, CancellationToken ct = default)
        {
            var data = await _dataProvider.GetDataAsync(@event, ct);
            using (LocalizationHelper.UseCulture(data.OwnerLang))
            {
                var title = _localizer["New_Review"];
                var body = _localizer["Review_Body", data.Rating];

                await _notificationService.SendToUserAsync(data.OwnerId, title, body);
            }

        }
    }
}
