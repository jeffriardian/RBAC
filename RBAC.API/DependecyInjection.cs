using RBAC.Application;
using RBAC.Core;
using RBAC.Infrastructure;

namespace RBAC.API
{
    public static class DependecyInjection
    {
        public static IServiceCollection AddAppDI(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddApplicationDI()
                .AddInfrastructureDI()
                .AddCoreDI(configuration);

            return services;
        }
    }
}
