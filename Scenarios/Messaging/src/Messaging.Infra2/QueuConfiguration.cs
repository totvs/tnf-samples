using Messaging.Infra2.Messages;
using System;
using System.Threading;
using Tnf.Bus.Queue;
using Tnf.Bus.Queue.Interfaces;

namespace Messaging.Infra2
{
    public static class QueuConfiguration
    {
        /// <summary>
        /// Setup Mensageria - Veja documentação TDN
        /// </summary>
        public static IExchangeRouter GetExchangeRouterConfiguration()
        {
            // Cria um Tópico da mensagem CustomerCreatedEvent
            var customerCreatedEventTopicToSubscribe = TopicSetup.Builder
                .New(s =>
                        s.Message<NotificationMessage>()
                        .AddKey("Notification.Message"));

            // Cria uma Fila
            var queue = QueueSetup.Builder
               .New(s => s
                    .QueueName("MessagingQueue")
                    .Reliability(r => r
                        .AutoAck(false)
                        .AutoDeleteQueue(true)
                        .MaxMessageSize(256)
                        .PersistMessage(false))
                    .QoS(q => q
                        .PrefetchGlobalLimit(true)
                        .PrefetchLimit(100)
                        .PrefetchSize(0))
                    .AddTopics(customerCreatedEventTopicToSubscribe));

            // Cria um Exchange Router
            var exchangeRouter = ExchangeRouter
                .Builder
                .Factory()
                .Name("MessagingExchange")
                .ServerAddress("127.0.0.1")
                .Type(ExchangeType.topic)
                .QueueChannel(QueueChannel.Amqp)
                .Reliability(isDurable: false, isAutoDelete: false, isPersistent: false)
                .AddQueue(queue)
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

            return exchangeRouter;
        }
    }
}