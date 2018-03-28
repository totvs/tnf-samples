using Messaging.Infra1.Messages;
using System.Threading.Tasks;
using Tnf.Bus.Client;
using Tnf.Bus.Queue.Interfaces;
using Tnf.Notifications;

namespace Messaging.Infra1.Services
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

        public Task Notify(string message)
        {
            if (string.IsNullOrWhiteSpace(message))
            {
                _notificationHandler.DefaultBuilder
                    .AsError()
                    .WithMessage(Constants.LocalizationSourceName, LocalizationKeys.UndefinedMessage)
                    .Raise();

                return Task.CompletedTask;
            }

            return Handle(new NotificationMessage(message));
        }

        public async Task Handle(NotificationMessage message)
            => await message.Publish();
    }
}
