using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using QNomy.Api.Data;

namespace QNomy.Persistence
{
    [ExcludeFromCodeCoverage]
    public static class ServicesConfiguration
    {
        public static IServiceCollection AddPersistenceServices(this IServiceCollection services,
            string connectionStr, string migrationsAssemblyName)
        {
            //configuration.GetConnectionString("DefaultConnection")
            services.AddDbContext<DataContext>(options =>
                options.UseSqlServer(connectionStr,
                    sqlOptions => sqlOptions.MigrationsAssembly(migrationsAssemblyName)));
            return services;
        }
    }
}
