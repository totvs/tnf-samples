using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using Tnf.CarShop.Application.Messages.Events;
using Tnf.Messaging;

namespace Tnf.CarShop.Application.Messages;
public static class MessagingServiceCollectionExtensions
{
    public static IServiceCollection AddMessaging(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddTransient<ICarEventPublisher, CarEventPublisher>();

        services.Configure<RabbitMqOptions>(configuration.GetSection("RabbitMq"));
        services.AddSingleton<IValidateOptions<RabbitMqOptions>, RabbitMqOptionsValidator>();

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
            });
        });

        return services;
    }
}
