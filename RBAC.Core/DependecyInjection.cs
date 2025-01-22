using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RBAC.Core.Options;

namespace RBAC.Core
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddCoreDI(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<ConnectionStringOptions>(configuration.GetSection(ConnectionStringOptions.SectionName));

            return services;
        }
    }
}
