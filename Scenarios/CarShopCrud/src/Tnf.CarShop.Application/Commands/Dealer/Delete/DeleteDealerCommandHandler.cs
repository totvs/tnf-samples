using Microsoft.Extensions.Logging;
using Tnf.CarShop.Domain.Repositories;
using Tnf.Commands;

namespace Tnf.CarShop.Host.Commands.Dealer.Delete;

public class DeleteDealerCommandHandler : ICommandHandler<DeleteDealerCommand, DeleteDealerResult>
{
    private readonly ILogger<DeleteDealerCommandHandler> _logger;
    private readonly IDealerRepository _dealerRepository;

    public DeleteDealerCommandHandler(ILogger<DeleteDealerCommandHandler> logger, IDealerRepository dealerRepository)
    {
        _logger = logger;
        _dealerRepository = dealerRepository;
    }

    public async Task HandleAsync(ICommandContext<DeleteDealerCommand, DeleteDealerResult> context,
        CancellationToken cancellationToken = new CancellationToken())
    {
        var command = context.Command;

        var success = await _dealerRepository.DeleteAsync(command.DealerId, cancellationToken);

        context.Result = new DeleteDealerResult(success);

        return;
    }
}
