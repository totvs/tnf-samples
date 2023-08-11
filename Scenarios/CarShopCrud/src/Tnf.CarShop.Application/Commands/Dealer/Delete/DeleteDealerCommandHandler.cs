using Microsoft.Extensions.Logging;
using Tnf.CarShop.Domain.Repositories;
using Tnf.Commands;

namespace Tnf.CarShop.Application.Commands.Dealer.Delete;

public class DeleteDealerCommandHandler : CommandHandler<DeleteDealerCommand, DeleteDealerResult>
{
    private readonly IStoreRepository _dealerRepository;
    private readonly ILogger<DeleteDealerCommandHandler> _logger;

    public DeleteDealerCommandHandler(ILogger<DeleteDealerCommandHandler> logger, IStoreRepository dealerRepository)
    {
        _logger = logger;
        _dealerRepository = dealerRepository;
    }

    public override async Task<DeleteDealerResult> ExecuteAsync(DeleteDealerCommand command,
        CancellationToken cancellationToken = default)
    {
        await _dealerRepository.DeleteAsync(command.DealerId, cancellationToken);

        return new DeleteDealerResult(true);
    }
}