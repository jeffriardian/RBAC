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
                options.UseSqlServer("Server=JE\\SQLEXPRESS;Database=rbac;User Id=root;Password=123321123;TrustServerCertificate=True;");
            });

            return services;
        }
    }
}
