using Castle.Core.Logging;
using Tnf.Architecture.Domain.WhiteHouse;
using Tnf.Dependency;
using Tnf.Events.Bus;
using Tnf.Events.Bus.Handlers;

namespace Tnf.Architecture.Domain.Events.WhiteHouse
{
    public class PresidentCreatedHandler : IEventHandler<PresidentCreatedEvent>, ITransientDependency
    {
        private readonly ILogger _logger;

        public PresidentCreatedHandler(ILogger logger)
        {
            _logger = logger;
        }

        public void HandleEvent(PresidentCreatedEvent eventData)
        {
            _logger.Info($"President Created Event Id {eventData.President.Id} and Name {eventData.President.Name}");
        }
    }

    public class PresidentCreatedEvent : EventData
    {
        public President President { get; }

        public PresidentCreatedEvent(President president)
        {
            President = president;
        }
    }
}
