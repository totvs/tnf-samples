using Microsoft.Extensions.DependencyInjection;
using Tnf.CarShop.Domain.Repositories;
using Tnf.CarShop.EntityFrameworkCore.Repositories;

namespace Tnf.CarShop.EntityFrameworkCore;

public static class EFCoreServiceCollectionExtensions
{
    public static IServiceCollection AddEFCore(this IServiceCollection services)
    {
        services.AddTransient<ICarRepository, CarRepository>();
        services.AddTransient<ICustomerRepository, CustomerRepository>();
        services.AddTransient<IStoreRepository, StoreRepository>();
        services.AddTransient<IPurchaseRepository, PurchaseRepository>();

        return services;
    }
}