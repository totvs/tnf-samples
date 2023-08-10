using Microsoft.Extensions.Logging;
using Tnf.CarShop.Application.Factories;
using Tnf.CarShop.Domain.Repositories;
using Tnf.Commands;

namespace Tnf.CarShop.Application.Commands.Dealer.Get;

public class GetDealerCommandHandler : CommandHandler<GetDealerCommand, GetDealerResult>
{
    private readonly DealerFactory _dealerFactory;
    private readonly IDealerRepository _dealerRepository;
    private readonly ILogger<GetDealerCommandHandler> _logger;

    public GetDealerCommandHandler(ILogger<GetDealerCommandHandler> logger, IDealerRepository dealerRepository,
        DealerFactory dealerFactory)
    {
        _logger = logger;
        _dealerRepository = dealerRepository;
        _dealerFactory = dealerFactory;
    }

    public override async Task<GetDealerResult> ExecuteAsync(GetDealerCommand command,
        CancellationToken cancellationToken = default)
    {
        if (command.DealerId.HasValue)
        {
            var dealer = await _dealerRepository.GetAsync(command.DealerId.Value, cancellationToken);

            if (dealer is null) throw new Exception($"Dealer with id {command} not found.");

            var dealerResult = new GetDealerResult(_dealerFactory.ToDto(dealer));

            return dealerResult;
        }

        var dealers = await _dealerRepository.GetAllAsync(cancellationToken);

        var dealersDto = dealers.Select(_dealerFactory.ToDto).ToList();

        return new GetDealerResult(dealersDto);
    }
}