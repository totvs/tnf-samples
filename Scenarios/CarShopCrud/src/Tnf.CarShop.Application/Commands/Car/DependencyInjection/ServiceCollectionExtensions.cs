using Microsoft.Extensions.DependencyInjection;
using Tnf.CarShop.Application.Commands.Car.Create;
using Tnf.CarShop.Application.Commands.Car.Delete;
using Tnf.CarShop.Application.Commands.Car.Get;
using Tnf.CarShop.Application.Commands.Car.Update;

namespace Tnf.CarShop.Application.Commands.Car.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection CreateCarCommandHandler(this IServiceCollection services) =>
       services.AddTransient<ICreateCarCommandHandler, CreateCarCommandHandler>();

    public static IServiceCollection DeleteCarCommandHandler(this IServiceCollection services) =>
     services.AddTransient<IDeleteCarCommandHandler, DeleteCarCommandHandler>();

    public static IServiceCollection GetCarCommandHandler(this IServiceCollection services) =>
     services.AddTransient<IGetCarCommandHandler, GetCarCommandHandler>();

    public static IServiceCollection UpdateCarCommandHandler(this IServiceCollection services) =>
     services.AddTransient<IUpdateCarCommandHandler, UpdateCarCommandHandler>();
}
