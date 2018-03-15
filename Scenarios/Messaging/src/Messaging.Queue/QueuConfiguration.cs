using Messaging.Queue.Messages;
using Tnf.Bus.Queue;
using Tnf.Bus.Queue.Interfaces;

namespace Messaging.Queue
{
    public static class QueuConfiguration
    {
        /// <summary>
        /// Setup Mensageria - Veja documentação TDN
        /// </summary>
        public static IExchangeRouter GetExchangeRouterConfiguration()
        {
            // Cria um Tópico da mensagem CustomerCreatedEvent
            var customerCreatedEventTopic = TopicSetup.Builder
                .New(s =>
                        s.Message<NotificationMessage>()
                        .AddKey("Notification.Message"));

            // Cria uma Fila
            var queue = QueueSetup.Builder
               .New(s => s
                    .QueueName("General")
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
                .Name("ExchangeForCase3")
                .ServerAddress("127.0.0.1")
                .Type(ExchangeType.topic)
                .QueueChannel(QueueChannel.Amqp)
                .Reliability(isDurable: false, isAutoDelete: false, isPersistent: false)
                .AddQueue(queue)
                .SetExclusive(false)
                .Build();

            return exchangeRouter;
        }
    }
}