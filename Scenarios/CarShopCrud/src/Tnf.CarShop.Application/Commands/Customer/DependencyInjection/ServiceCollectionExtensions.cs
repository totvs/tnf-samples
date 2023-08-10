using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Tnf.CarShop.Application.Commands.Car.Create;
using Tnf.CarShop.Application.Commands.Customer.Create;
using Tnf.CarShop.Application.Commands.Customer.Delete;
using Tnf.CarShop.Application.Commands.Customer.Get;
using Tnf.CarShop.Application.Commands.Customer.Update;

namespace Tnf.CarShop.Application.Commands.Customer.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection CreateCustomerCommandHandler(this IServiceCollection services) =>
       services.AddTransient<ICreateCustomerCommandHandler, CreateCustomerCommandHandler>();
    public static IServiceCollection DeleteCustomerCommandHandler(this IServiceCollection services) =>
       services.AddTransient<IDeleteCustomerCommandHandler, DeleteCustomerCommandHandler>();
    public static IServiceCollection GetCustomerCommandHandler(this IServiceCollection services) =>
       services.AddTransient<IGetCustomerCommandHandler, GetCustomerCommandHandler>();
    public static IServiceCollection UpdateCustomerCommandHandler(this IServiceCollection services) =>
       services.AddTransient<IUpdateCustomerCommandHandler, UpdateCustomerCommandHandler>();
}
