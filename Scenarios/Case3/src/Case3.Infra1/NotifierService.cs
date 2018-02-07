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

        public async Task Notify(string message)
        {
            if (string.IsNullOrWhiteSpace(message))
            {
                _notificationHandler.DefaultBuilder
                    .AsError()
                    .WithMessage(Infra3Consts.LocalizationSourceName, LocalizationKeys.UndefinedMessage)
                    .Raise();
            }

            await Handle(new NotificationMessage(message));
        }

        public async Task Handle(NotificationMessage message)
            => await message.Publish();
    }
}
