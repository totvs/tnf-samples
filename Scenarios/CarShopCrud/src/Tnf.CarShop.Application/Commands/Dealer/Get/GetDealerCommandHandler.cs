using Microsoft.Extensions.Logging;
using Tnf.CarShop.Application.Commands.Dealer.Get;
using Tnf.CarShop.Application.Dtos;
using Tnf.CarShop.Application.Factories;
using Tnf.CarShop.Domain.Repositories;
using Tnf.Commands;

public class GetDealerCommandHandler : ICommandHandler<GetDealerCommand, GetDealerResult>
{
    private readonly ILogger<GetDealerCommandHandler> _logger;
    private readonly IDealerRepository _dealerRepository;
    private readonly DealerFactory _dealerFactory;

    public GetDealerCommandHandler(ILogger<GetDealerCommandHandler> logger, IDealerRepository dealerRepository, DealerFactory dealerFactory)
    {
        _logger = logger;
        _dealerRepository = dealerRepository;
        _dealerFactory = dealerFactory;
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
        
        var dealerResult = new GetDealerResult( _dealerFactory.ToDto(dealer));

        context.Result = dealerResult;
    }
}