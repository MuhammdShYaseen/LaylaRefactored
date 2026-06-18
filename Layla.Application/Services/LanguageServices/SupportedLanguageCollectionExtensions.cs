using Layla.Application.Services.LanguageServices;
using Layla.Options;

namespace Layla.Services.LanguageServices
{
    public static class SupportedLanguageCollectionExtensions
    {
        public static IServiceCollection AddSupportedLanguageService(this IServiceCollection services) 
        {
            services.AddSingleton<ISupportedLanguagePolicy, SupportedLanguagePolicy>();
           
            return services;
        }
    }
}
