using HelloWorld.Application;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ApplicationServiceCollectionExtensions
    {
        public static IServiceCollection AddApplicationServiceDependency(this IServiceCollection services)
        {
            // Dependencia do projeto Tnf.HelloWord.SharedKernel
            services.AddSharedKernelDependency();

            // Registro do serviço de search
            services.AddTransient<ISearchAppService, SearchAppService>();

            return services;
        }
    }
}
