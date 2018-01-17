using Tnf.Notifications;

namespace Case1.Domain
{
    public class HashService : IHashService
    {
        private readonly INotificationHandler _notificationHandler;

        public HashService(INotificationHandler notificationHandler)
        {
            _notificationHandler = notificationHandler;
        }

        public string CalculateHash(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                _notificationHandler.DefaultBuilder
                    .AsError()
                    .WithMessage(Case1Consts.LocalizationSourceName, Error.CalculateHashInvalidValue)
                    .Raise();

                return string.Empty;
            }

            var hash = Hash.CalculateMD5(value);
            return hash;
        }

        public enum Error
        {
            CalculateHashInvalidValue
        }
    }
}
