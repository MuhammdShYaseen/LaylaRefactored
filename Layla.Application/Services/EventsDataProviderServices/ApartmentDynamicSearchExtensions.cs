using Layla.Application.Services.DynamicApartmentSearchService;
using Layla.Services.DynamicApartmentSearchService;

namespace Layla.Application.Services.EventsDataProviderServices
{
    public static class ApartmentDynamicSearchExtensions
    {
        public static IServiceCollection AddApartmentDynamicSearch(this IServiceCollection services)
        {
            services.AddScoped<IApartmentFilterBuilder, ApartmentFilterBuilder>();
            services.AddScoped<IApartmentSearchService, ApartmentSearchService>();
            return services;
        }
    }
}
