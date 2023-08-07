public class GetDealerCommandHandler : ICommandHandler<GetDealerCommand, DealerResult>
{
    private readonly ILogger<GetDealerCommandHandler> _logger;
    private readonly IDealerRepository _dealerRepository;

    public GetDealerCommandHandler(ILogger<GetDealerCommandHandler> logger, IDealerRepository dealerRepository)
    {
        _logger = logger;
        _dealerRepository = dealerRepository;
    }

    public async Task HandleAsync(ICommandContext<GetDealerCommand, DealerResult> context,
        CancellationToken cancellationToken = new CancellationToken())
    {
        var command = context.Command;

        var dealer = await _dealerRepository.GetAsync(command.Id, cancellationToken);

        if (dealer == null)
        {
            throw new Exception($"Dealer with id {command.Id} not found.");
        }

        var dealerResult = new DealerResult(dealer.Id, dealer.Name, dealer.Location, true);

        context.Result = dealerResult;

        return;
    }
}