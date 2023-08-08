using Microsoft.Extensions.Options;
//using Microsoft.Extensions.Options;
//using RabbitMQ.Client;
//using Tnf.Messaging.RabbitMq;


namespace Tnf.CarShop.Host.DependencyInjection;

internal static class ServiceCollectionExtensions
{
    //public static IServiceCollection AddMessaging(this IServiceCollection services, IConfiguration configuration)
    //{
    //    services.Configure<RabbitMqOptions>(configuration.GetSection("RabbitMq"));
    //    services.AddSingleton<IValidateOptions<RabbitMqOptions>, RabbitMqOptionsValidator>();

    //    services.AddScoped<IEventPublisher, EventPublisher>();
    //    services.AddScoped<IEventEnricher, CurrentUserEventEnricher>();
    //    services.AddScoped<IEventOutput, EventStorageOutput>();

    //    services.AddTnfMessaging(messaging =>
    //    {
    //        messaging.UseCloudEventsStructuredMode();
    //        messaging.AddRabbitMqTransport(rabbit =>
    //        {
    //            rabbit.ConfigureOptions(configuration.GetSection("RabbitMq"));
    //            rabbit.ConfigureRacOutgoingMessages();
    //        });
    //    });

    //    return services;
    //}

    //public static IServiceCollection AddDapServices(this IServiceCollection services)
    //{
    //    services.AddTransient<IProductFeatureSeeder, ProductFeatureSeeder>();

    //    return services;
    //}

    //public static IServiceCollection AddRac(this IServiceCollection services)
    //{
    //    services.AddScoped<IRacService, RacService>();
    //    services.AddScoped<IRacClient, RacClient>();

    //    return services;
    //}

    //private static void ConfigureRacOutgoingMessages(this IRabbitMqTransportBuilder rabbit)
    //{
    //    rabbit.AddExchange(Exchanges.RacOutgoing, ExchangeType.Headers);
    //    rabbit.AddExchange(Exchanges.RacProducts);

    //    rabbit.AddExchangeBind(new ExchangeBind()
    //    {
    //        Source = Exchanges.RacOutgoing,
    //        Destination = Exchanges.RacProducts,
    //        Arguments = new Dictionary<string, object>()
    //        {
    //            ["type"] = nameof(ProductCreatedEvent)
    //        }
    //    });

    //    rabbit.AddQueue(Queues.RacProducts);
    //    rabbit.AddQueueBind(Queues.RacProducts, Exchanges.RacProducts);
    //}
}
