using Microsoft.Extensions.Logging;
using Tnf.CarShop.Application.Dtos;
using Tnf.CarShop.Application.Factories;
using Tnf.CarShop.Domain.Repositories;
using Tnf.Commands;

namespace Tnf.CarShop.Application.Commands.Car.Create;

public class CreateCarCommandHandler : CommandHandler<CreateCarCommand, CreateCarResult>
{
    private readonly CarFactory _carFactory;
    private readonly ICarRepository _carRepository;
    private readonly ICustomerRepository _customerRepository;
    private readonly IDealerRepository _dealerRepository;
    private readonly ILogger<CreateCarCommandHandler> _logger;


    public CreateCarCommandHandler(ILogger<CreateCarCommandHandler> logger, ICarRepository carRepository,
        IDealerRepository dealerRepository, ICustomerRepository customerRepository, CarFactory carFactory)
    {
        _logger = logger;
        _carRepository = carRepository;
        _dealerRepository = dealerRepository;
        _customerRepository = customerRepository;
        _carFactory = carFactory;
    }

    public override async Task<CreateCarResult> ExecuteAsync(CreateCarCommand command,
        CancellationToken cancellationToken = default)
    {
        var carDto = command.Car;

        var createdCarId = await CreateCarAsync(carDto, cancellationToken);

        return new CreateCarResult(createdCarId, true);
    }

    private async Task<Guid> CreateCarAsync(CarDto carDto, CancellationToken cancellationToken)
    {
        var newCar = _carFactory.ToEntity(carDto);

        if (carDto.Dealer != null)
        {
            var dealer = await FetchDealerAsync(carDto.Dealer.Id, cancellationToken);

            Check.NotNull(dealer, nameof(dealer));

            newCar.AssignToDealer(dealer);
        }

        if (carDto.Owner != null)
        {
            var owner = await FetchOwnerAsync(carDto.Owner.Id, cancellationToken);

            Check.NotNull(owner, "Customer");

            newCar.AssignToOwner(owner);
        }

        var createdCar = await _carRepository.InsertAsync(newCar, cancellationToken);

        return createdCar.Id;
    }

    private async Task<Domain.Entities.Dealer> FetchDealerAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _dealerRepository.GetAsync(id, cancellationToken);
    }

    private async Task<Domain.Entities.Customer> FetchOwnerAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _customerRepository.GetAsync(id, cancellationToken);
    }
}