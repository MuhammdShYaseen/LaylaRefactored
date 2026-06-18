
using Layla.Options;
using Layla.Services.ChatServices.Interfaces;
using Microsoft.Extensions.Options;
using static Layla.Models.MainModels.Message;
using Layla.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace Layla.Services.BackgroundServices
{
    public class VoiceCleanupService : BackgroundService
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly ChatOptions _options;
        public VoiceCleanupService(IServiceScopeFactory scopeFactory, IOptions<ChatOptions> options)
        {
            _scopeFactory = scopeFactory;
            _options = options.Value;
            
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            if (_options.VoiceMessageRetentionHours == null) return;

            while (!stoppingToken.IsCancellationRequested)
            {
                using var scope = _scopeFactory.CreateScope();

                var db = scope.ServiceProvider.GetRequiredService<LaylaContext>();

                var storage = scope.ServiceProvider.GetRequiredService<IVoiceStorageService>();

                var threshold = DateTime.UtcNow.AddHours(-_options.VoiceMessageRetentionHours.Value);

                var oldMessages = await db.Messages.Where(x => x.Type == MessageType.Voice && x.CreatedAt < threshold && x.VoiceFilePath != null).ToListAsync();

                foreach (var msg in oldMessages)
                {
                    await storage.DeleteAsync(msg.VoiceFilePath!);
                    msg.DeleteVoiceFilePath();
                }
                await db.SaveChangesAsync();
                await Task.Delay(TimeSpan.FromHours(1), stoppingToken);
            }
        }
    }
}
