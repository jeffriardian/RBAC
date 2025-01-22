using MediatR.NotificationPublishers;
using Microsoft.Extensions.DependencyInjection;

namespace RBAC.Application
{
    public static class DependecyInjection
    {
        public static IServiceCollection AddApplicationDI(this IServiceCollection services)
        {
            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssembly(typeof(DependecyInjection).Assembly);
                cfg.NotificationPublisher = new TaskWhenAllPublisher();
            });

            return services;
        }
    }
}
