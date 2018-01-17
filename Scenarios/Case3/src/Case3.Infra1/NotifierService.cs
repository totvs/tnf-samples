using Case3.Queue.Messages;
using System.Threading.Tasks;
using Tnf.Bus.Client;
using Tnf.Bus.Queue.Interfaces;
using Tnf.Notifications;

namespace Case3.Infra1.Services
{
    public class NotifierService :
        INotifierService,
        IPublish<NotificationMessage>
    {
        private readonly INotificationHandler _notificationHandler;

        public NotifierService(INotificationHandler notificationHandler)
        {
            _notificationHandler = notificationHandler;
        }

        public void Handle(NotificationMessage message)
            => message.Publish();

        public Task Notify(string message)
        {
            if (string.IsNullOrWhiteSpace(message))
            {
                _notificationHandler.DefaultBuilder
                    .AsError()
                    .WithMessage(Infra3Consts.LocalizationSourceName, LocalizationKeys.UndefinedMessage)
                    .Raise();

                return Task.CompletedTask;
            }

            Handle(new NotificationMessage(message));

            return Task.CompletedTask;
        }
    }
}
