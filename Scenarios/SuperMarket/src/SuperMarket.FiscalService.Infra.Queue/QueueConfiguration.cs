using SuperMarket.Backoffice.Sales.Infra.Queue.Messages;
using System;
using Tnf.Bus.Queue;
using Tnf.Bus.Queue.RabbitMQ;
using Tnf.Configuration;

namespace SuperMarket.FiscalService.Infra.Queue
{
    public static class QueueConfiguration
    {
        public static ITnfConfiguration ConfigureFiscalServiceQueueInfraDependency(this ITnfConfiguration configuration)
        {
            // Cria um Tópico da mensagem PurchaseOrderChangedMessage
            var purchaseOrderChangedTopic = TopicSetup.Builder
                .New(s =>
                        s.Message<PurchaseOrderChangedMessage>()
                        .AddKey("PurchaseOrder.Changed.Message"));

            var purchaseOrderQueue = QueueSetup.Builder
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
                    .AddTopics(purchaseOrderChangedTopic));

            // Cria um Tópico da mensagem TaxMovimentCalculatedMessage
            var taxMovimentCalculatedTopic = TopicSetup.Builder
                .New(s =>
                        s.Message<TaxMovimentCalculatedMessage>()
                        .AddKey("TaxMoviment.Calculated.Message"));

            var taxMovimentQueue = QueueSetup.Builder
               .New(s => s
                    .QueueName("TaxMovimentQueue")
                    .Reliability(r => r
                        .AutoAck(false)
                        .AutoDeleteQueue(true)
                        .MaxMessageSize(256)
                        .PersistMessage(false))
                    .QoS(q => q
                        .PrefetchGlobalLimit(true)
                        .PrefetchLimit(100)
                        .PrefetchSize(0))
                    .AddTopics(taxMovimentCalculatedTopic));

            // Cria um Exchange Router
            var exchangeRouter = ExchangeRouter
                .Builder
                .Factory()
                .Name("SuperMarket")
                .ServerAddress("127.0.0.1")
                .Type(ExchangeType.topic)
                .QueueChannel(QueueChannel.Amqp)
                .Reliability(isDurable: false, isAutoDelete: false, isPersistent: false)
                .AddQueue(purchaseOrderQueue)
                .AddQueue(taxMovimentQueue)
                .SetExclusive(false)
                .Build();

            // Configura para que ela publique mensagens
            configuration
                .BusClient()
                .AddPublisher(
                    exBuilder: e => exchangeRouter,
                    listener: er => new PublisherListener(
                        exchangeRouter: er,
                        serviceProvider: configuration.ServiceProvider))
                .AddSubscriber(
                        exBuilder: e => exchangeRouter,
                        listener: er => new SubscriberListener(
                            exchangeRouter: er,
                            serviceProvider: configuration.ServiceProvider),
                        poolSize: 2);

            return configuration;
        }
    }
}
