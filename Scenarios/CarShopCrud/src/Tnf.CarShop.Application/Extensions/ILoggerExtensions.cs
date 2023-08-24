using Microsoft.Extensions.Logging;

namespace Tnf.CarShop.Application.Extensions;
public static class ILoggerExtensions
{
    public static void MessageIsEmpty(this ILogger logger) => logger.LogDebug($"Message is empty");

    public static void DataIsEmpty(this ILogger logger) => logger.LogDebug($"Data is empty");

    public static void EntitySuccessfullyCreated(this ILogger logger, string entity, Guid id)
        => logger.LogInformation($"Entity {entity} {id} successfully created!");

    public static void EntityWasNotCreated(this ILogger logger, string entity)
        => logger.LogWarning($"Entity {entity} was not created!");

    public static void MessageSuccessfullyProcessed(this ILogger logger, string message)
        => logger.LogInformation($"Message {message} successfully processed!");

    public static void MessageWasNotProcessed(this ILogger logger, string message)
        => logger.LogWarning($"Message {message} was not processed!");
}
