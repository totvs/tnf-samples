using Microsoft.Extensions.Logging;
using Tnf.CarShop.Domain.Repositories;
using Tnf.Commands;

namespace Tnf.CarShop.Application.Commands.Car.Delete;

public class DeleteCarCommandHandler : CommandHandler<DeleteCarCommand, DeleteCarResult>
{
    private readonly ICarRepository _carRepository;
    private readonly ILogger<DeleteCarCommandHandler> _logger;

    public DeleteCarCommandHandler(ILogger<DeleteCarCommandHandler> logger, ICarRepository carRepository)
    {
        _logger = logger;
        _carRepository = carRepository;
    }

    public override async Task<DeleteCarResult> ExecuteAsync(DeleteCarCommand command, CancellationToken cancellationToken = default)
    {
        try
        {
            await _carRepository.DeleteAsync(command.CarId, cancellationToken);

            return new DeleteCarResult(true);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message, ex);

            return new DeleteCarResult(false);
        }        
    }
}
