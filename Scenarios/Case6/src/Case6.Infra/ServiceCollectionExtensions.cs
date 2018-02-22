using Case6.Web;
using Case6.Infra.Mappers;
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

            services.AddTnfAutoMapper(options =>
            {
                options.AddProfile<CustomerProfile>();
            });

            return services;
        }
    }
}
