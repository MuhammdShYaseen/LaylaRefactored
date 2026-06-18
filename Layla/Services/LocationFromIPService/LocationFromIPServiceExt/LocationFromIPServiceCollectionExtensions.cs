using Layla.Services.LocationFromIPService.Implementations;
using Layla.Services.LocationFromIPService.Interfaces;

namespace Layla.Services.LocationFromIPService.LocationFromIPServiceExt
{
    public static class LocationFromIPServiceCollectionExtensions
    {
        public static IServiceCollection AddLocationFromApi(this IServiceCollection services) 
        {
            
            services.AddHttpClient();
            services.AddScoped<ILocationFromIPExternalService, LocationFromIPExternalService>();
            return services;
        }
    }
}
