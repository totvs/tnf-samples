using Microsoft.Extensions.Logging;
using Tnf.CarShop.Application.Commands.Dealer.Update;
using Tnf.CarShop.Application.Dtos;
using Tnf.CarShop.Domain.Repositories;
using Tnf.CarShop.Host.Commands.Dealer;
using Tnf.Commands;

public class UpdateDealerCommandHandler : ICommandHandler<UpdateDealerCommand, UpdateDealerResult>
{
    private readonly ILogger<UpdateDealerCommandHandler> _logger;
    private readonly IDealerRepository _dealerRepository;

    public UpdateDealerCommandHandler(ILogger<UpdateDealerCommandHandler> logger, IDealerRepository dealerRepository)
    {
        _logger = logger;
        _dealerRepository = dealerRepository;
    }

    public async Task HandleAsync(ICommandContext<UpdateDealerCommand, UpdateDealerResult> context,
        CancellationToken cancellationToken = new CancellationToken())
    {
        var dealerDto = context.Command.Dealer;

        var dealer = await _dealerRepository.GetAsync(dealerDto.Id, cancellationToken);

        if (dealer == null)
        {
            throw new Exception($"Dealer with id {dealerDto.Id} not found.");
        }

        dealer.UpdateName(dealerDto.Name);
        dealer.UpdateLocation(dealerDto.Location);
        
        var updatedDealer = await _dealerRepository.UpdateAsync(dealer, cancellationToken);
        
        var returnedCars = updatedDealer.Cars;
        var cars = new List<CarDto>();

        if (returnedCars != null)
            foreach (var car in returnedCars)
            {
                var carDto = new CarDto(car.Id, car.Brand, car.Model, car.Year, car.Price, car.Dealer?.Id, car.Owner?.Id);
                cars.Add(carDto);
            }

        context.Result = new UpdateDealerResult(new DealerDto(updatedDealer.Id, updatedDealer.Name, updatedDealer.Location, cars));

        return;
    }
}