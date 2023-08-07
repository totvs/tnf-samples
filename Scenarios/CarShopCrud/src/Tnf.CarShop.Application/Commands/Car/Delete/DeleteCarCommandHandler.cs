using Microsoft.Extensions.Logging;
using Tnf.CarShop.Domain.Repositories;
using Tnf.Commands;

namespace Tnf.CarShop.Application.Commands.Car.Delete;

public class DeleteCarCommandHandler : ICommandHandler<DeleteCarCommand, DeleteCarResult>
{
    private readonly ILogger<DeleteCarCommandHandler> _logger;
    private readonly ICarRepository _carRepository;

    public DeleteCarCommandHandler(ILogger<DeleteCarCommandHandler> logger, ICarRepository carRepository)
    {
        _logger = logger;
        _carRepository = carRepository;
    }

    public async Task HandleAsync(ICommandContext<DeleteCarCommand, DeleteCarResult> context,
        CancellationToken cancellationToken = new CancellationToken())
    {
        var command = context.Command;

        await _carRepository.DeleteAsync(command.CardId, cancellationToken);

        context.Result = new DeleteCarResult(true);

        return;
    }
}
