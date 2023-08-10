using Microsoft.Extensions.Logging;
using Tnf.CarShop.Application.Factories;
using Tnf.CarShop.Domain.Repositories;
using Tnf.Commands;

namespace Tnf.CarShop.Application.Commands.Dealer.Update;

public class UpdateDealerCommandHandler : CommandHandler<UpdateDealerCommand, UpdateDealerResult>
{
    private readonly DealerFactory _dealerFactory;
    private readonly IDealerRepository _dealerRepository;
    private readonly ILogger<UpdateDealerCommandHandler> _logger;

    public UpdateDealerCommandHandler(ILogger<UpdateDealerCommandHandler> logger, IDealerRepository dealerRepository,
        DealerFactory dealerFactory)
    {
        _logger = logger;
        _dealerRepository = dealerRepository;
        _dealerFactory = dealerFactory;
    }

    public override async Task<UpdateDealerResult> ExecuteAsync(UpdateDealerCommand command,
        CancellationToken cancellationToken = default)
    {
        var dealerDto = command.Dealer;

        var dealer = await _dealerRepository.GetAsync(dealerDto.Id, cancellationToken);

        if (dealer == null) throw new Exception($"Dealer with id {dealerDto.Id} not found.");

        dealer.UpdateName(dealerDto.Name);
        dealer.UpdateLocation(dealerDto.Location);

        var updatedDealer = await _dealerRepository.UpdateAsync(dealer, cancellationToken);

        return new UpdateDealerResult(_dealerFactory.ToDto(updatedDealer));
    }
}