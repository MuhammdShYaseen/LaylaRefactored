using Layla.Application.Interfaces;
using Layla.Domain.DomainServices;
using Layla.Infrastructure.BackgroundServices;
using Layla.Infrastructure.DataAccess.DbContext;
using Layla.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Layla.Infrastructure
{
    public static class InfrastructureExtensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<LaylaContext>(options =>       // ← اسم الـ Context الفعلي
               options.UseSqlServer(
                   configuration.GetConnectionString("DefaultConnection"),
                         sqlOptions => sqlOptions.UseNetTopologySuite()
                   .MigrationsAssembly("Layla.Infrastructure")  // ← اسم المشروع الفعلي
                   .EnableRetryOnFailure(3, TimeSpan.FromSeconds(5), null)));

            services.AddSingleton<InMemoryEventDispatcher>();

            services.AddSingleton<IEventDispatcher>(sp => sp.GetRequiredService<InMemoryEventDispatcher>());
            services.AddSingleton<ISupportedLanguagePolicy, SupportedLanguagePolicy>();
            return services;
        }
    }
}
