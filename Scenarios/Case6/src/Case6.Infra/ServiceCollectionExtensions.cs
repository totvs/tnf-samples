using Microsoft.Extensions.DependencyInjection;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddInfra6Dependency(this IServiceCollection services)
        {
            services.AddTnfDefaultConventionalRegistrations();

            services.AddTnfNotifications();

            return services;
        }
    }
}
