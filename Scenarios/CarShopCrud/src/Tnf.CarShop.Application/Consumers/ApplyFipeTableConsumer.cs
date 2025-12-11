using Microsoft.Extensions.Logging;

using Tnf.CarShop.Application.Commands.Fipe;
using Tnf.CarShop.Application.Extensions;
using Tnf.CarShop.Application.Messages;
using Tnf.Commands;

using Tnf.Messaging;

namespace Tnf.CarShop.Application.Consumers;
public class ApplyFipeTableConsumer : IConsumer<CloudEvent<ApplyFipeTable>>
{
    private readonly ICommandSender _commandSender;
    private readonly ILogger<ApplyFipeTableConsumer> _logger;

    public ApplyFipeTableConsumer(ICommandSender commandSender, ILogger<ApplyFipeTableConsumer> logger)
    {
        _commandSender = commandSender;
        _logger = logger;
    }

    public async Task ConsumeAsync(IConsumeContext<CloudEvent<ApplyFipeTable>> context, CancellationToken cancellationToken = default)
    {
        if (!context.TryGetMessage(_logger, out var message))
            return;

        if (!context.TryGetData(_logger, out var data))
            return;

        var command = new ApplyFipeTableCommand
        {
            AveragePrice = data.AveragePrice,
            Brand = data.Brand,
            FipeCode = data.FipeCode,
            Model = data.Model,
            MonthYearReference = data.MonthYearReference,
            Year = data.Year
        };

        var result = await _commandSender.SendAsync(command);

        if (result)
            _logger.MessageSuccessfullyProcessed(nameof(ApplyFipeTable));
        else
            _logger.MessageWasNotProcessed(nameof(ApplyFipeTable));
    }
}
