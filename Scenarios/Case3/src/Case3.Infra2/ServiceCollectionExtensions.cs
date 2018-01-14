namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static void AddInfra2Dependency(this IServiceCollection services)
        {
            // Configura o uso do cache em memoria
            services.AddTnfMemoryCache();
        }
    }
}
