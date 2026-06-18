using Layla.Services.DynamicApartmentSearchService.BuilderServices;

namespace Layla.Services.DynamicApartmentSearchService
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
