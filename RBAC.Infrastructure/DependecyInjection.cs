using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using RBAC.Core.Interfaces;
using RBAC.Core.Options;
using RBAC.Infrastructure.Data;
using RBAC.Infrastructure.Repositories;

namespace RBAC.Infrastructure
{
    public static class DependecyInjection
    {
        public static IServiceCollection AddInfrastructureDI(this IServiceCollection services)
        {
            /*services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer("Server=JE\\SQLEXPRESS;Database=rbac;User Id=root;Password=123321123;TrustServerCertificate=True;");
            });*/

            services.AddDbContext<AppDbContext>((provider, options) =>
            {
                options.UseSqlServer(provider.GetRequiredService<IOptionsSnapshot<ConnectionStringOptions>>().Value.DefaultConnection);
            });

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddScoped<IPermissionRepository, PermissionRepository>();
            // Register password hasher
            services.AddScoped<IPasswordHasher, PasswordHasher>();

            return services;
        }
    }
}
