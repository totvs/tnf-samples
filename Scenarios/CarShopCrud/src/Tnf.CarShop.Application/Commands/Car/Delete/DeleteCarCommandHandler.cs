using Microsoft.Extensions.Logging;
using Tnf.CarShop.Domain.Repositories;
using Tnf.Commands;

namespace Tnf.CarShop.Application.Commands.Car.Delete;

public class DeleteCarCommandHandler : ICommandHandler<DeleteCarCommand, DeleteCarResult>
{
    private readonly ICarRepository _carRepository;
    private readonly ILogger<DeleteCarCommandHandler> _logger;

    public DeleteCarCommandHandler(ILogger<DeleteCarCommandHandler> logger, ICarRepository carRepository)
    {
        _logger = logger;
        _carRepository = carRepository;
    }

    public async Task HandleAsync(ICommandContext<DeleteCarCommand, DeleteCarResult> context,
        CancellationToken cancellationToken = new())
    {
        var command = context.Command;

        await _carRepository.DeleteAsync(command.CarId, cancellationToken);

        context.Result = new DeleteCarResult(true);
    }
}
