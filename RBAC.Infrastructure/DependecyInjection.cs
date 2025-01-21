using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using RBAC.Infrastructure.Data;

namespace RBAC.Infrastructure
{
    public static class DependecyInjection
    {
        public static IServiceCollection AddInfrastructureDI(this IServiceCollection services)
        {
            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer("connection string");
            });

            return services;
        }
    }
}
