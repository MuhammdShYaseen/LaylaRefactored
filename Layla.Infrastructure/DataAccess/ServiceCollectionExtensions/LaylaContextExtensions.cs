using Layla.Infrastructure.DataAccess.DbContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Layla.Infrastructure.DataAccess.ServiceCollectionExtensions
{
    public static class LaylaContextExtensions
    {
        public static IServiceCollection AddLaylaContextExtension(this IServiceCollection services, IConfiguration configuration) 
        {
            services.AddDbContext<LaylaContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
             sqlOptions => sqlOptions.UseNetTopologySuite()));
            return services;
        }
    }
}
