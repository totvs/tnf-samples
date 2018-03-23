using SuperMarket.Backoffice.Sales.Infra.Queue.Messages;
using System;
using Tnf.Bus.Queue;
using Tnf.Bus.Queue.RabbitMQ;
using Tnf.Configuration;

namespace SuperMarket.Backoffice.Sales.Infra
{
    public static class QueueConfiguration
    {
        public static ITnfConfiguration ConfigureSalesQueueInfraDependency(this ITnfConfiguration configuration)
        {
            // Cria um Tópico da mensagem PurchaseOrderChangedMessage
            var customerCreatedEventTopic = TopicSetup.Builder
                .New(s =>
                        s.Message<PurchaseOrderChangedMessage>()
                        .AddKey("PurchaseOrder.Changed.Message"));

            // Cria uma Fila
            var queue = QueueSetup.Builder
               .New(s => s
                    .QueueName("PurchaseOrderQueue")
                    .Reliability(r => r
                        .AutoAck(false)
                        .AutoDeleteQueue(true)
                        .MaxMessageSize(256)
                        .PersistMessage(false))
                    .QoS(q => q
                        .PrefetchGlobalLimit(true)
                        .PrefetchLimit(100)
                        .PrefetchSize(0))
                    .AddTopics(customerCreatedEventTopic));

            // Cria um Exchange Router
            var exchangeRouter = ExchangeRouter
                .Builder
                .Factory()
                .Name("SuperMarket")
                .ServerAddress("127.0.0.1")
                .Type(ExchangeType.topic)
                .QueueChannel(QueueChannel.Amqp)
                .Reliability(isDurable: false, isAutoDelete: false, isPersistent: false)
                .AddQueue(queue)
                .SetExclusive(false)
                .Build();

            // Configura para que ela publique mensagens
            configuration
                .BusClient()
                .AddPublisher(
                    exBuilder: e => exchangeRouter,
                    listener: er => new PublisherListener(
                        exchangeRouter: er,
                        serviceProvider: configuration.ServiceProvider));

            return configuration;
        }
    }
}
