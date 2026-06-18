using Layla.Services.ChatServices.Implementations;
using Layla.Services.ChatServices.Interfaces;

namespace Layla.Services.ChatServices.ServiceCollectionExtensions
{
    public static class ChatServiceExtensions
    {
        public static IServiceCollection AddChatServiceExtensions(this IServiceCollection services) 
        {
            services.AddScoped<IConversationReadService, ConversationReadService>();
            services.AddSignalR();
            services.AddScoped<IConversationService, ConversationService>();
            services.AddScoped<IMessageService, MessageService>();
            services.AddScoped<IVoiceStorageService, VoiceStorageService>();
            return services;
        }
    }
}
