using Microsoft.Extensions.Logging;
using Tnf.CarShop.Application.Dtos;
using Tnf.CarShop.Application.Factories;
using Tnf.CarShop.Domain.Repositories;
using Tnf.Commands;

namespace Tnf.CarShop.Application.Commands.Dealer.Create;

public class CreateDealerCommandHandler : ICommandHandler<CreateDealerCommand, CreateDealerResult>
{
    private readonly DealerFactory _dealerFactory;
    private readonly IDealerRepository _dealerRepository;
    private readonly ILogger<CreateDealerCommandHandler> _logger;

    public CreateDealerCommandHandler(ILogger<CreateDealerCommandHandler> logger, IDealerRepository dealerRepository,
        DealerFactory dealerFactory)
    {
        _logger = logger;
        _dealerRepository = dealerRepository;
        _dealerFactory = dealerFactory;
    }

    public async Task HandleAsync(ICommandContext<CreateDealerCommand, CreateDealerResult> context,
        CancellationToken cancellationToken = new())
    {
        var dealerDto = context.Command.Dealer;

        var createdDealerId = await CreateDealerAsync(dealerDto, cancellationToken);

        context.Result = new CreateDealerResult(createdDealerId);
    }

    private async Task<Guid> CreateDealerAsync(DealerDto dealerDto, CancellationToken cancellationToken)
    {
        var newDealer = _dealerFactory.ToEntity(dealerDto);

        var createdDealer = await _dealerRepository.InsertAsync(newDealer, cancellationToken);

        return createdDealer.Id;
    }
}