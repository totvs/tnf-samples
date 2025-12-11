using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using NewMessaging.ApplicationB.Models;
using RabbitMQ.Client;
using Tnf.Messaging;
using Tnf.Messaging.RabbitMq;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        const string ExchangeApplicationBIn = "ApplicationB.Input";
        const string ExchangeApplicationBOut = "ApplicationB.Output";

        const string ExchangeApplicationBInQueue = "ApplicationB.Input.Queue";

        public static IServiceCollection AddMessaging(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTnfMessaging(messaging =>
            {
                messaging.AddRabbitMqTransport(rabbit =>
                {
                    rabbit.ConfigureOptions(configuration.GetSection("RabbitMq"));

                    // ConfigureSending for ApplicationB Input

                    rabbit.AddExchange(ExchangeApplicationBOut, ExchangeType.Headers);

                    rabbit.ConfigureSending<ICloudEvent<NewCustomerModel>>(context =>
                    {
                        context.Exchange = ExchangeApplicationBOut;
                        context.Properties.Headers.Add("element", "NewMessaging");
                    });

                    // ConfigureReceive for ApplicationB Output

                    rabbit.AddExchange(ExchangeApplicationBIn, ExchangeType.Headers);

                    rabbit.AddQueue(ExchangeApplicationBInQueue);
                    rabbit.AddQueueBind(new QueueBind(ExchangeApplicationBInQueue, ExchangeApplicationBIn, string.Empty, new Dictionary<string, object>()
                    {
                        ["x-match"] = "any",
                        ["element"] = "NewMessaging.ApplicationB"
                    }));
                });
            });

            return services;
        }
    }
}
