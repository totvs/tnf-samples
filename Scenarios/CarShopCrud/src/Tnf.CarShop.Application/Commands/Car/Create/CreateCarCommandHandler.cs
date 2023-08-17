using Microsoft.Extensions.Logging;
using Tnf.CarShop.Domain.Repositories;
using Tnf.Commands;

namespace Tnf.CarShop.Application.Commands.Car.Create;
//use xunit
public class CreateCarCommandHandler : CommandHandler<CreateCarCommand, CreateCarResult>
{
    private readonly ICarRepository _carRepository;
    private readonly ICustomerRepository _customerRepository;
    private readonly IStoreRepository _dealerRepository;
    private readonly ILogger<CreateCarCommandHandler> _logger;


    public CreateCarCommandHandler(ILogger<CreateCarCommandHandler> logger, ICarRepository carRepository,
        IStoreRepository dealerRepository, ICustomerRepository customerRepository)
    {
        _logger = logger;
        _carRepository = carRepository;
        _dealerRepository = dealerRepository;
        _customerRepository = customerRepository;
    }

    public override async Task<CreateCarResult> ExecuteAsync(CreateCarCommand command,
        CancellationToken cancellationToken = default)
    {
        var createdCarId = await CreateCarAsync(command, cancellationToken);

        return new CreateCarResult(createdCarId, true);
    }

    private async Task<Guid> CreateCarAsync(CreateCarCommand command, CancellationToken cancellationToken)
    {
        var newCar = new Domain.Entities.Car(command.Brand, command.Model, command.Year, command.Price);

        var createdCar = await _carRepository.InsertAsync(newCar, cancellationToken);

        return createdCar.Id;
    }
}