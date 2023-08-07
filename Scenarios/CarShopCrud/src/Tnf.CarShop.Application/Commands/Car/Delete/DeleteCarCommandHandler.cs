namespace Tnf.CarShop.Host.Commands.Car.Create.Delete;

public class DeleteCarCommandHandler : ICommandHandler<DeleteCarCommand, CommandResult>
{
    private readonly ILogger<DeleteCarCommandHandler> _logger;
    private readonly ICarRepository _carRepository;

    public DeleteCarCommandHandler(ILogger<DeleteCarCommandHandler> logger, ICarRepository carRepository)
    {
        _logger = logger;
        _carRepository = carRepository;
    }

    public async Task HandleAsync(ICommandContext<DeleteCarCommand, CommandResult> context,
        CancellationToken cancellationToken = new CancellationToken())
    {
        var command = context.Command;

        var success = await _carRepository.DeleteAsync(command.Id, cancellationToken);

        context.Result = new CommandResult(success);

        return;
    }
}
