using Microsoft.Extensions.Logging;
using Tnf.CarShop.Application.Commands.Dealer.Get;
using Tnf.CarShop.Application.Dtos;
using Tnf.CarShop.Domain.Repositories;
using Tnf.Commands;

public class GetDealerCommandHandler : ICommandHandler<GetDealerCommand, GetDealerResult>
{
    private readonly ILogger<GetDealerCommandHandler> _logger;
    private readonly IDealerRepository _dealerRepository;

    public GetDealerCommandHandler(ILogger<GetDealerCommandHandler> logger, IDealerRepository dealerRepository)
    {
        _logger = logger;
        _dealerRepository = dealerRepository;
    }

    public async Task HandleAsync(ICommandContext<GetDealerCommand, GetDealerResult> context,
        CancellationToken cancellationToken = new CancellationToken())
    {
        var dealerId = context.Command.DealerId;

        var dealer = await _dealerRepository.GetAsync(dealerId, cancellationToken);

        if (dealer == null)
        {
            throw new Exception($"Dealer with id {dealerId} not found.");
        }

        var returnedCars = dealer.Cars;
        var cars = new List<CarDto>();

        if (returnedCars != null)
            foreach (var car in returnedCars)
            {
                var carDto = new CarDto(car.Id, car.Brand, car.Model, car.Year, car.Price, car.Dealer?.Id, car.Owner?.Id);
                cars.Add(carDto);
            }

        var dealerResult = new GetDealerResult( new DealerDto(  dealer.Id, dealer.Name, dealer.Location, cars));

        context.Result = dealerResult;

        return;
    }
}