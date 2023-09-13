using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using Tnf.CarShop.Application.Messages;
using Tnf.CarShop.Application.Messages.Events;
using Tnf.Messaging;

namespace Tnf.CarShop.Application.DependencyInjection;
public static class ServiceCollectionExtensions
{    
    public static IServiceCollection AddMessaging(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddTransient<ICarEventPublisher, CarEventPublisher>();       

        services.AddTnfMessaging(messaging =>
        {
            messaging.UseCloudEventsStructuredMode();
            messaging.AddRabbitMqTransport(rabbit =>
            {
                rabbit.ConfigureOptions(configuration.GetSection("RabbitMq"));

                rabbit.AddExchange(Exchanges.CarShopOutput, ExchangeType.Headers);

                rabbit.ConfigureSending<ICloudEvent<IOutputMessage>>(context =>
                {
                    context.Exchange = Exchanges.CarShopOutput;
                });

                rabbit.AddExchange(Exchanges.CarShopInput, ExchangeType.Fanout);
                rabbit.AddQueue(Queues.CarShopInput);
                rabbit.AddQueueBind(Queues.CarShopInput, Exchanges.CarShopInput);
            });
        });

        return services;
    }

    public static IServiceCollection AddCommands(this IServiceCollection services)
    {
        services.AddTnfCommands(commands =>
        {
            commands.AddCommandHandlersFromAssembly(typeof(ServiceCollectionExtensions).Assembly);
        });
        return services;
    }
}
