using Microsoft.Extensions.DependencyInjection;

namespace RBAC.Application
{
    public static class DependecyInjection
    {
        public static IServiceCollection AddApplicationDI (this IServiceCollection services)
        {
            return services;
        }
    }
}
