using Microsoft.Extensions.Logging;
using Tnf.CarShop.Application.Dtos;
using Tnf.CarShop.Application.Factories;
using Tnf.CarShop.Application.Factories.Interfaces;
using Tnf.CarShop.Domain.Repositories;
using Tnf.Commands;

namespace Tnf.CarShop.Application.Commands.Dealer.Get;

public class GetDealerCommandHandler : CommandHandler<GetDealerCommand, GetDealerResult>
{
    private readonly ICarFactory _carFactory;
    private readonly IDealerFactory _dealerFactory;
    private readonly IStoreRepository _dealerRepository;
    private readonly ILogger<GetDealerCommandHandler> _logger;

    public GetDealerCommandHandler(ILogger<GetDealerCommandHandler> logger, IStoreRepository dealerRepository,
        IDealerFactory dealerFactory, ICarFactory carFactory)
    {
        _logger = logger;
        _dealerRepository = dealerRepository;
        _dealerFactory = dealerFactory;
        _carFactory = carFactory;
    }

    public override async Task<GetDealerResult> ExecuteAsync(GetDealerCommand command,
        CancellationToken cancellationToken = default)
    {
        if (command.DealerId.HasValue)
        {
            var dealer = await _dealerRepository.GetAsync(command.DealerId.Value, cancellationToken);

            if (dealer is null) throw new Exception($"Dealer with id {command} not found.");

            var dealerDto = _dealerFactory.ToDto(dealer);

            dealerDto.Cars = dealer.Cars?.Select(car => _carFactory.ToDto(car)).ToList() ?? new List<CarDto>();

            return new GetDealerResult(dealerDto);
        }

        var dealers = await _dealerRepository.GetAllAsync(cancellationToken);

        var dealersDto = dealers.Select(dealer =>
        {
            var dto = _dealerFactory.ToDto(dealer);
            dto.Cars = dealer.Cars?.Select(car => _carFactory.ToDto(car)).ToList() ?? new List<CarDto>();
            return dto;
        }).ToList();

        return new GetDealerResult(dealersDto);
    }
}