using Layla.DataAccess;
using Layla.DomainEvents.Domain.Dispatcher;
using Layla.DomainEvents.Domain.Events;
using Layla.Services.FirebaseServices.Interfaces;

namespace Layla.DomainEvents.Handlers
{
    public class MediaUploadedHandler : IEventHandler<MediaUploadedEvent>
    {
        private readonly LaylaContext _context;
        private readonly INotificationService _notificationService;
        private readonly ILogger<MediaUploadedHandler> _logger;

        public MediaUploadedHandler(
            LaylaContext context,
            INotificationService notificationService,
            ILogger<MediaUploadedHandler> logger)
        {
            _context = context;
            _notificationService = notificationService;
            _logger = logger;
        }

        public async Task HandleAsync(MediaUploadedEvent @event, CancellationToken ct = default)
        {
            try
            {
                var apartment = await _context.Apartments.FindAsync(@event.ApartmentId);
                await _notificationService.SendToUserAsync(apartment!.OwnerId, "تم رفع ملف جديد", $"تم رفع ملف جديد لشقتك." );
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to notify for media upload {Id}", @event.MediaFileId);
            }
        }
    }
}
