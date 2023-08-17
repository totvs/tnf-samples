using Microsoft.Extensions.Logging;
using Tnf.CarShop.Domain.Repositories;
using Tnf.Commands;

namespace Tnf.CarShop.Application.Commands.Dealer.Delete;

public class DeleteStoreCommandHandler : CommandHandler<DeleteStoreCommand, DeleteStoreResult>
{
    private readonly IStoreRepository _storeRepository;
    private readonly ILogger<DeleteStoreCommandHandler> _logger;

    public DeleteStoreCommandHandler(ILogger<DeleteStoreCommandHandler> logger, IStoreRepository dealerRepository)
    {
        _logger = logger;
        _storeRepository = dealerRepository;
    }

    public override async Task<DeleteStoreResult> ExecuteAsync(DeleteStoreCommand command,
        CancellationToken cancellationToken = default)
    {
        await _storeRepository.DeleteAsync(command.StoreId, cancellationToken);

        return new DeleteStoreResult(true);
    }
}