using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using NewMessaging.ApplicationA.Models;
using RabbitMQ.Client;
using Tnf.Messaging;
using Tnf.Messaging.RabbitMq;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        const string ExchangeApplicationBIn = "ApplicationB.Input";
        const string ExchangeApplicationBOut = "ApplicationB.Output";

        const string ExchangeApplicationAQueue = "ApplicationA.Queue.From.ApplicationB.Output";

        public static IServiceCollection AddMessaging(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTnfMessaging(messaging =>
            {
                messaging.AddRabbitMqTransport(rabbit =>
                {
                    rabbit.ConfigureOptions(configuration.GetSection("RabbitMq"));

                    // ConfigureSending for ApplicationB Input

                    rabbit.AddExchange(ExchangeApplicationBIn, ExchangeType.Headers);

                    rabbit.ConfigureSending<ICloudEvent<NewCustomerModel>>(context =>
                    {
                        context.Exchange = ExchangeApplicationBIn;
                        context.Properties.Headers.Add("element", "NewMessaging.ApplicationB");
                    });

                    // ConfigureReceive for ApplicationB Output

                    rabbit.AddExchange(ExchangeApplicationBOut, ExchangeType.Headers);

                    rabbit.AddQueue(ExchangeApplicationAQueue);
                    rabbit.AddQueueBind(new QueueBind(ExchangeApplicationAQueue, ExchangeApplicationBOut, string.Empty, new Dictionary<string, object>()
                    {
                        ["x-match"] = "any",
                        ["element"] = "NewMessaging"
                    }));
                });
            });

            return services;
        }
    }
}
