using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.DependencyInjection;
using QNomy.Infrastructure.Contracts;
using QNomy.Infrastructure.Services;

namespace QNomy.Infrastructure
{
    [ExcludeFromCodeCoverage]
    public static class ServicesConfiguration
    {
        public static IServiceCollection AddInfraServices(this IServiceCollection services)
        {
            services.AddScoped<IClientProcessingService, ClientProcessingService>();
            return services;
        }
    }
}
