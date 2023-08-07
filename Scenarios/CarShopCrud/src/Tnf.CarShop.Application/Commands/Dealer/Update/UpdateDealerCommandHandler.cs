using Tnf.CarShop.Domain.Repositories;
using Tnf.CarShop.Host.Commands.Dealer;
using Tnf.Commands;

public class UpdateDealerCommandHandler : ICommandHandler<DealerCommand, DealerResult>
{
    private readonly ILogger<UpdateDealerCommandHandler> _logger;
    private readonly IDealerRepository _dealerRepository;

    public UpdateDealerCommandHandler(ILogger<UpdateDealerCommandHandler> logger, IDealerRepository dealerRepository)
    {
        _logger = logger;
        _dealerRepository = dealerRepository;
    }

    public async Task HandleAsync(ICommandContext<DealerCommand, DealerResult> context,
        CancellationToken cancellationToken = new CancellationToken())
    {
        var command = context.Command;

        var dealer = await _dealerRepository.GetAsync(command.Id, cancellationToken);

        if (dealer == null)
        {
            throw new Exception($"Dealer with id {command.Id} not found.");
        }

        dealer.UpdateName(command.Name);
        dealer.UpdateLocation(command.Location);

        var updatedDealer = await _dealerRepository.UpdateAsync(dealer, cancellationToken);

        context.Result = new DealerResult(updatedDealer.Id, true);

        return;
    }
}