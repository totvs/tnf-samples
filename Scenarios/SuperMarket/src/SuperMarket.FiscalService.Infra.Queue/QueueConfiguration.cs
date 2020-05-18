using Microsoft.Extensions.DependencyInjection;
using SuperMarket.FiscalService.Infra.Queue.Messages;
using System;
using System.Threading;
using Tnf.Bus.Client;
using Tnf.Bus.Queue;
using Tnf.Bus.Queue.RabbitMQ;
using Tnf.Configuration;

namespace SuperMarket.FiscalService.Infra.Queue
{
    public static class QueueConfiguration
    {
        public static void ConfigureFiscalServiceQueueInfraDependency(this ITnfBuilder builder)
        {
            // Cria um Tópico da mensagem PurchaseOrderChangedMessage
            var purchaseOrderChangedTopicToSubscribe = TopicSetup.Builder
                .New(s =>
                        s.Message<PurchaseOrderChangedMessage>()
                        .AddKey("PurchaseOrder.Changed.Message"));

            var purchaseOrderQueueToSubscribe = QueueSetup.Builder
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
                    .AddTopics(purchaseOrderChangedTopicToSubscribe));

            // Cria um Tópico da mensagem TaxMovimentCalculatedMessage
            var taxMovimentCalculatedTopicToPublish = TopicSetup.Builder
                .New(s =>
                        s.Message<TaxMovimentCalculatedMessage>()
                        .AddKey("TaxMoviment.Calculated.Message"));

            var taxMovimentQueueToPublish = QueueSetup.Builder
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
                    .AddTopics(taxMovimentCalculatedTopicToPublish));

            // Cria um Exchange Router
            var exchangeRouterToPublish = ExchangeRouter
                .Builder
                .Factory()
                .Name("SuperMarket")
                .ServerAddress("127.0.0.1")
                .Type(ExchangeType.topic)
                .QueueChannel(QueueChannel.Amqp)
                .Reliability(isDurable: false, isAutoDelete: false, isPersistent: false)
                .AddQueue(taxMovimentQueueToPublish)
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
                .AddQueue(purchaseOrderQueueToSubscribe)
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
            builder.BusClient(busClient =>
            {
                busClient.AddPublisher(
                exBuilder: e => exchangeRouterToPublish,
                listener: er => new PublisherListener(
                    exchangeRouter: er,
                    serviceProvider: busClient.ServiceProvider));

                busClient.AddSubscriber(
                    exBuilder: e => exchangeRouterToSubscribe,
                    listener: er => new SubscriberListener(
                        exchangeRouter: er,
                        serviceProvider: busClient.ServiceProvider),
                    poolSize: 4);
            });
        }
    }
}
