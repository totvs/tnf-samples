using Case3.Queue.Messages;
using System.Threading.Tasks;
using Tnf.Bus.Client;
using Tnf.Bus.Queue.Interfaces;

namespace Case3.Infra1.Services
{
    public class NotifierService :
        INotifierService,
        IPublish<NotificationMessage>
    {
        public void Handle(NotificationMessage message)
            => message.Publish();

        public Task Notify(string message)
        {
            Handle(new NotificationMessage(message));

            return Task.CompletedTask;
        }
    }
}
