using Layla.Services.AdminDashboardService.Interfaces;
using Layla.Services.AdminDashboardService.Implementations;

namespace Layla.Services.AdminDashboardService.ServiceCollectionExtensions
{
    public static class AdminDashBoardServiceCollectionExtensions
    {
        public static IServiceCollection AddAdminDashBoardService(this IServiceCollection services) 
        {
            services.AddScoped<IDashboardService, DashboardService>();
            return services;
        }

    }
}
