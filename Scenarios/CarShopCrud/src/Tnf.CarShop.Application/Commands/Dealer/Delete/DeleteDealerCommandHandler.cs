using Microsoft.Extensions.Logging;
using Tnf.CarShop.Domain.Repositories;
using Tnf.Commands;

namespace Tnf.CarShop.Application.Commands.Dealer.Delete;

public class DeleteDealerCommandHandler : ICommandHandler<DeleteDealerCommand, DeleteDealerResult>
{
    private readonly IDealerRepository _dealerRepository;
    private readonly ILogger<DeleteDealerCommandHandler> _logger;

    public DeleteDealerCommandHandler(ILogger<DeleteDealerCommandHandler> logger, IDealerRepository dealerRepository)
    {
        _logger = logger;
        _dealerRepository = dealerRepository;
    }

    public async Task HandleAsync(ICommandContext<DeleteDealerCommand, DeleteDealerResult> context,
        CancellationToken cancellationToken = new())
    {
        var command = context.Command;

        await _dealerRepository.DeleteAsync(command.DealerId, cancellationToken);

        context.Result = new DeleteDealerResult(true);
    }
}