using Microsoft.Extensions.DependencyInjection;

namespace RBAC.Core
{
    public static class DependecyInjection
    {
        public static IServiceCollection AddCoreDI(this IServiceCollection services)
        {
            return services;
        }
    }
}
