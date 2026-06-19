using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Layla.Application
{
    public static class ApplicationExtansions
    {
        public static IServiceCollection AddApplication (this IServiceCollection services)
        {
            return services;
        }
    }
}
