using RBAC.Application;
using RBAC.Infrastructure;

namespace RBAC.API
{
    public static class DependecyInjection
    {
        public static IServiceCollection AddAppDI(this IServiceCollection services)
        {
            services.AddApplicationDI()
                .AddInfrastructureDI();

            return services;
        }
    }
}
