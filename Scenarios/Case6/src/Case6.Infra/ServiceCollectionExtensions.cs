using Case5.Web;
using System.Data.Common;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddInfra6Dependency(this IServiceCollection services)
        {
            services.AddTnfDefaultConventionalRegistrations();

            services.AddTnfNotifications();

            services.AddTransient<DbProviderFactory, TnfDbProviderFactory>();

            return services;
        }
    }
}
