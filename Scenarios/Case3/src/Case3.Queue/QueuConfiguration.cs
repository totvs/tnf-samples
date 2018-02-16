using Case3.Queue.Messages;
using Tnf.Bus.Queue;
using Tnf.Bus.Queue.Interfaces;

namespace Case3.Queue
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
                .Factory()
                .Message<NotificationMessage>()
                .AddKey("Notification.Message")
                .Build();

            // Cria uma Fila
            var queue = QueueSetup.Builder
               .Factory()
               .QueueName("General")
               .QueueReliabilitySetup(r => r
                   .AutoAck(false)
                   .AutoDeleteQueue(true)
                   .MaxMessageSize(256)
                   .PersistMessage(false)
                   .Build())
                .QueueQosSetup(q => q
                    .PrefetchGlobalLimit(true)
                    .PrefetchLimit(100)
                    .PrefetchSize(0)
                    .Build())
               .AddTopics(customerCreatedEventTopic) 
               .Build();

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