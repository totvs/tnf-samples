using Microsoft.Extensions.Logging;
using Tnf.CarShop.Application.Dtos;
using Tnf.CarShop.Domain.Repositories;
using Tnf.CarShop.Host.Commands.Car.Create;
using Tnf.Commands;

namespace Tnf.CarShop.Application.Commands.Car.Create;
public class CreateCarCommandHandler : ICommandHandler<CreateCarCommand, CreateCarResult>
{
    private readonly ILogger<CreateCarCommandHandler> _logger;
    private readonly ICarRepository _carRepository;
    private readonly IDealerRepository _dealerRepository;
    private readonly ICustomerRepository _customerRepository;
    

    public CreateCarCommandHandler(ILogger<CreateCarCommandHandler> logger, ICarRepository carRepository, IDealerRepository dealerRepository, ICustomerRepository customerRepository)
    {
        _logger = logger;
        _carRepository = carRepository;
        _dealerRepository = dealerRepository;
        _customerRepository = customerRepository;
    }

    public async Task HandleAsync(ICommandContext<CreateCarCommand, CreateCarResult> context,
        CancellationToken cancellationToken = new CancellationToken())
    {

        var carDto = context.Command.Car;

        var createdCarId = await CreateCarAsync(carDto, cancellationToken);


        context.Result = new CreateCarResult(createdCarId, true);

        return;
    }

    private async Task<Guid> CreateCarAsync(CarDto carDto, CancellationToken cancellationToken)
    {
        var newCar = new Domain.Entities.Car(carDto.Brand, carDto.Model, carDto.Year, carDto.Price);

        if (carDto.DealerId.HasValue)
        {
            var dealer = await FetchDealerAsync(carDto.DealerId.Value, cancellationToken);
            CheckEntityNotNull(dealer, "Dealer", carDto.DealerId.Value);
            newCar.AssignToDealer(dealer);
        }
        
        if (carDto.OwnerId.HasValue)
        {
            var owner = await FetchOwnerAsync(carDto.OwnerId.Value, cancellationToken);
            CheckEntityNotNull(owner, "Owner", carDto.OwnerId.Value);
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

    private void CheckEntityNotNull<T>(T entity, string entityType, Guid id)
    {
        if (entity == null)
        {
            throw new Exception($"{entityType} with id {id} not found.");
        }
    }
    
}