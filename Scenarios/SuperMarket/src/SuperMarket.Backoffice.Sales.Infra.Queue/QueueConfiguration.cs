using SuperMarket.Backoffice.Sales.Infra.Queue.Messages;
using System;
using System.Threading;
using Tnf.Bus.Queue;
using Tnf.Bus.Queue.RabbitMQ;
using Tnf.Configuration;

namespace SuperMarket.Backoffice.Sales.Infra.Queue
{
    public static class QueueConfiguration
    {
        public static ITnfConfiguration ConfigureSalesQueueInfraDependency(this ITnfConfiguration configuration)
        {
            // Cria um Tópico da mensagem PurchaseOrderChangedMessage
            var purchaseOrderChangedTopicToPublish = TopicSetup.Builder
                .New(s =>
                        s.Message<PurchaseOrderChangedMessage>()
                        .AddKey("PurchaseOrder.Changed.Message"));

            var purchaseOrderQueueToPublish = QueueSetup.Builder
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
                    .AddTopics(purchaseOrderChangedTopicToPublish));

            // Cria um Tópico da mensagem TaxMovimentCalculatedMessage
            var taxMovimentCalculatedTopicToSubscribe = TopicSetup.Builder
                .New(s =>
                        s.Message<TaxMovimentCalculatedMessage>()
                        .AddKey("TaxMoviment.Calculated.Message"));

            var taxMovimentQueueToSubscribe = QueueSetup.Builder
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
                    .AddTopics(taxMovimentCalculatedTopicToSubscribe));

            // Cria um Exchange Router
            var exchangeRouterToPublish = ExchangeRouter
                .Builder
                .Factory()
                .Name("SuperMarket")
                .ServerAddress("127.0.0.1")
                .Type(ExchangeType.topic)
                .QueueChannel(QueueChannel.Amqp)
                .Reliability(isDurable: false, isAutoDelete: false, isPersistent: false)
                .AddQueue(purchaseOrderQueueToPublish)
                .SetExclusive(false)
                .AutomaticRecovery(
                    isEnable: true,
                    connectionTimeout: 15000,
                    networkRecoveryInterval: TimeSpan.FromSeconds(10))
                .MessageCollector(
                    refreshInterval: TimeSpan.FromMilliseconds(value: 2000),
                    timeout: TimeSpan.FromSeconds(60))
                .ShutdownBehavior(
                    graceful: new CancellationTokenSource(),
                    forced: new CancellationTokenSource())
                .Build();

            var exchangeRouterToSubscribe = ExchangeRouter
                .Builder
                .Factory()
                .Name("SuperMarket")
                .ServerAddress("127.0.0.1")
                .Type(ExchangeType.topic)
                .QueueChannel(QueueChannel.Amqp)
                .Reliability(isDurable: false, isAutoDelete: false, isPersistent: false)
                .AddQueue(taxMovimentQueueToSubscribe)
                .SetExclusive(false)
                .AutomaticRecovery(
                    isEnable: true,
                    connectionTimeout: 15000,
                    networkRecoveryInterval: TimeSpan.FromSeconds(10))
                .MessageCollector(
                    refreshInterval: TimeSpan.FromMilliseconds(value: 2000),
                    timeout: TimeSpan.FromSeconds(60))
                .ShutdownBehavior(
                    graceful: new CancellationTokenSource(),
                    forced: new CancellationTokenSource())
                .Build();

            // Configura para que ela publique mensagens
            configuration
                .BusClient()
                .AddPublisher(
                    exBuilder: e => exchangeRouterToPublish,
                    listener: er => new PublisherListener(
                        exchangeRouter: er,
                        serviceProvider: configuration.ServiceProvider))
                .AddSubscriber(
                        exBuilder: e => exchangeRouterToSubscribe,
                        listener: er => new SubscriberListener(
                            exchangeRouter: er,
                            serviceProvider: configuration.ServiceProvider),
                        poolSize: 1);

            return configuration;
        }
    }
}
