using Microsoft.Extensions.Logging;
using Tnf.CarShop.Application.Dtos;
using Tnf.CarShop.Application.Factories;
using Tnf.CarShop.Application.Factories.Interfaces;
using Tnf.CarShop.Domain.Repositories;
using Tnf.Commands;

namespace Tnf.CarShop.Application.Commands.Dealer.Update;

public class UpdateDealerCommandHandler : CommandHandler<UpdateDealerCommand, UpdateDealerResult>
{
    private readonly ICarFactory _carFactory;
    private readonly IDealerFactory _dealerFactory;
    private readonly IStoreRepository _dealerRepository;
    private readonly ILogger<UpdateDealerCommandHandler> _logger;

    public UpdateDealerCommandHandler(ILogger<UpdateDealerCommandHandler> logger, IStoreRepository dealerRepository,
        IDealerFactory dealerFactory, ICarFactory carFactory)
    {
        _logger = logger;
        _dealerRepository = dealerRepository;
        _dealerFactory = dealerFactory;
        _carFactory = carFactory;
    }

    public override async Task<UpdateDealerResult> ExecuteAsync(UpdateDealerCommand command,
        CancellationToken cancellationToken = default)
    {
        var dealerDto = command.Dealer;

        var dealer = await _dealerRepository.GetAsync(dealerDto.Id, cancellationToken);

        if (dealer == null) throw new Exception($"Dealer with id {dealerDto.Id} not found.");

        dealer.UpdateName(dealerDto.Name);
        dealer.UpdateLocation(dealerDto.Location);

        if (dealerDto.Cars != null)
        {
            var cars = dealerDto.Cars.Select(dto => _carFactory.ToEntity(dto)).ToList();
            foreach (var carDto in dealerDto.Cars)
            {
                var carEntity = _carFactory.ToEntity(carDto);
                dealer.AddCar(carEntity);
            }
        }

        var updatedDealer = await _dealerRepository.UpdateAsync(dealer, cancellationToken);

        var updatedDealerDto = _dealerFactory.ToDto(updatedDealer);
        updatedDealerDto.Cars =
            updatedDealer.Cars?.Select(car => _carFactory.ToDto(car)).ToList() ?? new List<CarDto>();

        return new UpdateDealerResult(updatedDealerDto);
    }
}