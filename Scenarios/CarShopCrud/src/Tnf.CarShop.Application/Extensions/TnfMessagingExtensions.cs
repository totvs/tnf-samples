using Microsoft.Extensions.Logging;
using Tnf.Messaging;

namespace Tnf.CarShop.Application.Extensions;

public static class TnfMessagingExtensions
{
    public static bool TryGetMessage<TMessageType>(
        this IConsumeContext<ICloudEvent<TMessageType>> context,
        ILogger logger,
        out ICloudEvent<TMessageType> message)
    {
        message = context.Message;

        if (message != null)
            return true;

        logger.MessageIsEmpty();

        return false;
    }

    public static bool TryGetData<TMessageType>(
        this IConsumeContext<ICloudEvent<TMessageType>> context,
        ILogger logger,
        out TMessageType data)
    {
        data = default;

        if (!context.TryGetMessage(logger, out var message))
            return false;

        data = message.Data;

        if (data != null)
            return true;

        logger.DataIsEmpty();

        return false;
    }
}
